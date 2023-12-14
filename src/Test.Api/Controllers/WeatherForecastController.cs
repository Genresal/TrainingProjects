using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using Azure.Storage;
using Microsoft.AspNetCore.Mvc;

namespace Test.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly TableClient _tableClient;
    private readonly TableServiceClient _tableServiceClient;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;

        //https://dev.to/alikolahdoozan/insert-update-delete-and-read-operations-in-azure-table-storage-by-cnet--ggd
        var tableName = "TestTable";
        var storageAccountKey = "ibE8d6jPZrsUT64gYkcqKSVXEfxVplCdP4srBMYTyO98gxb+2rWbO+uEWKGN6mchd44Ykw60rXAv+AStfQ1kfQ=="; // Add your Azure Storage Account Key to your configuration
        var storageAccountName = "storage4466";
        var tableServiceUri = new Uri($"https://{storageAccountName}.table.core.windows.net");

        var creds = new StorageSharedKeyCredential(storageAccountName, storageAccountKey);

        _tableServiceClient = new TableServiceClient(
            tableServiceUri,
            new TableSharedKeyCredential(storageAccountName, storageAccountKey));
    }

    [HttpGet(Name = "GetAzureTable")]
    public IActionResult GetTable()
    {
        Pageable<TableItem> queryTableResults = _tableServiceClient.Query(filter: $"TableName eq 'TestTable'");

        Console.WriteLine("The following are the names of the tables in the query results:");

        // Iterate the <see cref="Pageable"> in order to access queried tables.

        var obj = new Entity()
        {
            PartitionKey = Guid.NewGuid().ToString(), // Must be unique
            RowKey = Guid.NewGuid().ToString(), // Must be unique
            Name = "ValueA",
            Age = 14,
        };

        //AddEntity(obj.RowKey, obj.PartitionKey, obj.Age);

        QueryEntity("4f607a7a-7ece-4486-b878-3cb9fc168900");

        return Ok();
    }

    void AddEntity(string orderID, string category, int quantity)
    {
        TableClient tableClient = new TableClient("DefaultEndpointsProtocol=https;AccountName=storage4466;AccountKey=ibE8d6jPZrsUT64gYkcqKSVXEfxVplCdP4srBMYTyO98gxb+2rWbO+uEWKGN6mchd44Ykw60rXAv+AStfQ1kfQ==;EndpointSuffix=core.windows.net", "TestTable");

        TableEntity tableEntity = new TableEntity(category, orderID)
        {
            {"quantity",quantity}
        };

        tableClient.AddEntity(tableEntity);
        //
        Console.WriteLine("Added Entity with order ID {0}", orderID);
    }

    void QueryEntity(string category)
    {
        TableClient tableClient = new TableClient("DefaultEndpointsProtocol=https;AccountName=storage4466;AccountKey=ibE8d6jPZrsUT64gYkcqKSVXEfxVplCdP4srBMYTyO98gxb+2rWbO+uEWKGN6mchd44Ykw60rXAv+AStfQ1kfQ==;EndpointSuffix=core.windows.net", "TestTable");

        Pageable<TableEntity> results = tableClient.Query<TableEntity>(entity => entity.PartitionKey == category);

        foreach (TableEntity tableEntity in results)
        {
            Console.WriteLine("Order Id {0}", tableEntity.RowKey);
            Console.WriteLine("Quantity is {0}", tableEntity.GetInt32("quantity"));

        }
    }

    /*
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }*/
}
