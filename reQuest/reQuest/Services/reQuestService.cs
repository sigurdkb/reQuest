using Microsoft.WindowsAzure.MobileServices;
using reQuest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Microsoft.WindowsAzure.MobileServices.Files;
using Microsoft.WindowsAzure.MobileServices.Files.Sync;
using Microsoft.WindowsAzure.MobileServices.Eventing;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Collections.ObjectModel;
using System.Diagnostics;
using reQuest.Interfaces;

namespace reQuest.Services
{
    public class reQuestService
    {
        private static reQuestService defaultInstance = new reQuestService();
        private MobileServiceClient client;

        private IMobileServiceSyncTable<Quest> questTable;

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
            var questStore = new MobileServiceSQLiteStore("localqueststore.db");
            questStore.DefineTable<Quest>();

            client.InitializeFileSyncContext(new QuestFileSyncHandler(this), questStore);
            client.SyncContext.InitializeAsync(questStore, StoreTrackingOptions.NotifyLocalAndServerOperations);

            this.questTable = client.GetSyncTable<Quest>();
        }

		public async Task<string> GetUserSid()
		{
			var result = await client.InvokeApiAsync<string>("Values",System.Net.Http.HttpMethod.Get, null);
			return result;
		}

		public async Task<IEnumerable<Player>> GetPlayers()
		{
			IMobileServiceTable<Player> table = client.GetTable<Player>();
			var players = await table.ReadAsync();
			return players;
		}

		public async Task<Player> UpdatePlayer(Player player)
		{
			IMobileServiceTable<Player> table = client.GetTable<Player>();
			await table.UpdateAsync(player);
			return await table.LookupAsync(player.Id);
		}

        public async Task<ObservableCollection<Quest>> GetQuestsAsync(bool syncItems = false)
        {
            try
            {
                if (syncItems)
                {
                    await this.SyncAsync();
                }
                IEnumerable<Quest> items = await questTable.ToEnumerableAsync();

                return new ObservableCollection<Quest>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

        public async Task SaveQuestAsync(Quest item)
        {
            if (item.Id == null)
            {
                await questTable.InsertAsync(item);
            }
            else
            {
                await questTable.UpdateAsync(item);
            }
        }

        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await this.client.SyncContext.PushAsync();

                await this.questTable.PushFileChangesAsync();

                await this.questTable.PullAsync(
                    //The first parameter is a query name that is used internally by the client SDK to implement incremental sync.
                    //Use a different query name for each unique query in your program
                    "allQuestItems",
                    this.questTable.CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            // Simple error/conflict handling. A real application would handle the various errors like network conditions,
            // server conflicts and others via the IMobileServiceSyncHandler.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }
        }

        internal async Task DownloadFileAsync(MobileServiceFile file)
        {
            var todoItem = await questTable.LookupAsync(file.ParentId);
            IPlatform platform = DependencyService.Get<IPlatform>();

            string filePath = await FileHelper.GetLocalFilePathAsync(file.ParentId, file.Name);
            await platform.DownloadFileAsync(this.questTable, file, filePath);
        }

        internal async Task<MobileServiceFile> AddImage(Quest quest, string imagePath)
        {
            string targetPath = await FileHelper.CopyQuestFileAsync(quest.Id, imagePath);
            return await this.questTable.AddFileAsync(quest, Path.GetFileName(targetPath));
        }

        internal async Task DeleteImage(Quest quest, MobileServiceFile file)
        {
            await this.questTable.DeleteFileAsync(file);
        }

        internal async Task<IEnumerable<MobileServiceFile>> GetImageFilesAsync(Quest quest)
        {
            return await this.questTable.GetFilesAsync(quest);
        }
    }
}
