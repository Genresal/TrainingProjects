using BlazorServerTest.Data;

namespace BlazorServerTest.Services.Interfaces
{
    public interface IBackgroundService
    {
        Task GetAndSaveBackgroundAsync();
        Task AddJobs();
        Task RemoveJobs();
        Task CheckAndMarkNewData();
    }
}
