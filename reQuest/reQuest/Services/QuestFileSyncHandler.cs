using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.Files.Sync;
using Microsoft.WindowsAzure.MobileServices.Files;
using Microsoft.WindowsAzure.MobileServices.Files.Metadata;
using Xamarin.Forms;
using reQuest.Interfaces;

namespace reQuest.Services
{
    class QuestFileSyncHandler : IFileSyncHandler
    {
        private readonly reQuestService reQuestService;

        public QuestFileSyncHandler(reQuestService service)
        {
            this.reQuestService = service;
        }
        public Task<IMobileServiceFileDataSource> GetDataSource(MobileServiceFileMetadata metadata)
        {
            IPlatform platform = DependencyService.Get<IPlatform>();
            return platform.GetFileDataSource(metadata);
        }

        public async Task ProcessFileSynchronizationAction(MobileServiceFile file, FileSynchronizationAction action)
        {
            if (action == FileSynchronizationAction.Delete)
            {
                await FileHelper.DeleteLocalFileAsync(file);
            }
            else
            { // Create or update. We're aggressively downloading all files.
                await this.reQuestService.DownloadFileAsync(file);
            }
        }
    }
}
