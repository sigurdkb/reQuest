using Microsoft.WindowsAzure.MobileServices;
using reQuest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reQuest.Services
{
    public class reQuestService
    {
        private static reQuestService defaultInstance = new reQuestService();
        private MobileServiceClient client;

        public static reQuestService DefaultManager
        {
            get { return defaultInstance; }
            set { defaultInstance = value; }
        }
        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }

        public reQuestService()
        {
            client = new MobileServiceClient("https://requestbackend.azurewebsites.net");
        }

        public async Task<IEnumerable<Competency>> GetCompetencies()
        {
            var table = client.GetTable<Competency>();
            return await table.ReadAsync();
        }


    }
}
