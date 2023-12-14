using Azure;
using Azure.Data.Tables;

namespace Test.Api;

public class Entity : ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }

    public string Name { get; set; }
    public int Age { get; set; }
}
