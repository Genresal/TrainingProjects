using LaikableDogsAPI.DataAccess.Interfaces;
using LaikableDogsAPI.Services.Interfaces;
using LaikalableDogsAPI.Models;

namespace LaikableDogsAPI.Services
{
    public class AuxService : IAuxService
    {
        private readonly IAuxProvider auxProvider;

        public AuxService(IAuxProvider auxProvider)
        {
            this.auxProvider = auxProvider;
        }

        public async Task CreateDefaultData()
        {
            await auxProvider.CreateDefaultData();
        }

        public async Task DropCollections()
        {
            await auxProvider.DropCollections();
        }
    }
}
