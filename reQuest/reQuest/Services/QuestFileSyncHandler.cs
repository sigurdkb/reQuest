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
using PCLStorage;
using System.Diagnostics;

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
				IFolder rootFolder = FileSystem.Current.LocalStorage;
				IFolder reQuestFolder = await rootFolder.CreateFolderAsync("reQuest", CreationCollisionOption.OpenIfExists);
				IFile imageFile = await reQuestFolder.GetFileAsync(file.ParentId + ".jpg");
				Debug.WriteLine($"QuestFileSyncHandler:ProcessFileSynchronizationAction: {imageFile.Path}");

				await imageFile.DeleteAsync();
            }
            else
            { // Create or update. We're aggressively downloading all files.
                await this.reQuestService.DownloadFileAsync(file);
            }
        }
    }
}
