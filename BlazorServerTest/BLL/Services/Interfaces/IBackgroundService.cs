namespace BlazorServerTest.BLL.Services.Interfaces
{
    public interface IBackgroundService
    {
        Task GetAndSaveBackgroundAsync();
        Task AddJobs();
        Task RemoveJobs();
        Task CheckAndMarkNewData();
        Task JobImitation();
    }
}
