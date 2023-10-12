
namespace LaikableDogsAPI.Services.Interfaces
{
    public interface IAuxService
    {
        Task CreateDefaultData();
        Task DropCollections();
    }
}
