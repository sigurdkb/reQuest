﻿using Microsoft.WindowsAzure.MobileServices;
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
		const string applicationURL = @"https://requestbackend.azurewebsites.net";
		const string localDbStore = "localrequeststore.db";

        private static reQuestService instance = new reQuestService();
        private MobileServiceClient client;

		private Player player;

		private IMobileServiceSyncTable<Topic> topics;
		private IMobileServiceSyncTable<Competency> comptencies;
		private IMobileServiceSyncTable<Player> players;
		private IMobileServiceSyncTable<Quest> quests;

		private static Object currentDownloadTaskLock = new Object();
		private static Task currentDownloadTask = Task.FromResult(0);


        public static reQuestService Instance
        {
			get { return instance; }
			set { instance = value; }
        }
        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }

		public Player CurrentPlayer
		{
			get { return player; }
		}

		public List<Topic> Topics { get; private set; }
		public List<Competency> Competencies { get; private set; }
		public List<Player> Players { get; private set; }
		public List<Quest> Quests { get; private set; }




		public reQuestService()
        {
            client = new MobileServiceClient(applicationURL);
            var reQuestStore = new MobileServiceSQLiteStore(localDbStore);

			reQuestStore.DefineTable<Topic>();
			reQuestStore.DefineTable<Competency>();
			reQuestStore.DefineTable<Player>();
			reQuestStore.DefineTable<Quest>();

            client.InitializeFileSyncContext(new FileSyncHandler(this), reQuestStore);
            client.SyncContext.InitializeAsync(reQuestStore, StoreTrackingOptions.NotifyLocalAndServerOperations);

			topics = client.GetSyncTable<Topic>();
			comptencies = client.GetSyncTable<Competency>();
			players= client.GetSyncTable<Player>();
			quests = client.GetSyncTable<Quest>();

			//RefreshDataAsync();
    }

		public async Task<string> GetUserSid()
		{
			var result = await client.InvokeApiAsync<string>("Values",System.Net.Http.HttpMethod.Get, null);
			return result;
		}

		public bool SetCurrentPlayer(string externalId)
		{
			player = Players.FirstOrDefault(p => p.ExternalId == externalId);

			return player != null;
		}
		public async void SetCurrentPosition(double latitude, double longitude) 
		{
			player.Latitude = latitude;
			player.Longitude = longitude;
			await SavePlayerAsync(player);
			Debug.WriteLine($"Position: {latitude},{longitude}");

		}

		//public async Task<Player> UpdatePlayer(Player player)
		//{
		//	IMobileServiceTable<Player> table = client.GetTable<Player>();
		//	await table.UpdateAsync(player);
		//	return await table.LookupAsync(player.Id);
		//}

		public async Task RefreshDataAsync(bool sync = false)
		{
			if (sync)
			{
				await SyncAsync();
			}

			//IMobileServiceTableQuery<Player> playerQuery = players.CreateQuery();
			//var playerParameters = new Dictionary<string, string>() { { "$expand", "Competencies" } };
			//IMobileServiceTableQuery<Quest> questQuery = quests.CreateQuery();
			//var questParameters = new Dictionary<string, string>() { { "$expand", "Topic,Owner" } };

			try
			{
				Topics = await topics.ToListAsync();
				Competencies = await comptencies.ToListAsync();
				Players = await players.ToListAsync();
				Quests = await quests.ToListAsync();
				//IEnumerable<Player> expandedPlayers = await players.ReadAsync<Player>(playerQuery.WithParameters(playerParameters));
				//Players = expandedPlayers.ToList();
				//IEnumerable<Quest> expandedQuests = await quests.ReadAsync<Quest>(questQuery.WithParameters(questParameters));
				//Quests = expandedQuests.ToList();
			}

			catch (MobileServiceInvalidOperationException ex)
			{
				Debug.WriteLine($"Invalid sync operation: {ex.Message}");
			}
			catch (Exception ex)
			{ 
				Debug.WriteLine($"Refresh error: {ex.Message}");
				
			}
		}



    //    public async Task<ObservableCollection<Quest>> GetQuestsAsync(bool syncItems = false)
    //    {
    //        try
    //        {
    //            if (syncItems)
    //            {
    //                await this.SyncAsync();
    //            }

				//IMobileServiceTableQuery<Quest> questQuery = quests.CreateQuery();
				//var questParameters = new Dictionary<string, string>() { { "$expand", "Topic,Owner" } };
				//IEnumerable<Quest> expandedQuests = await quests.ReadAsync<Quest>(questQuery.WithParameters(questParameters));

				//return new ObservableCollection<Quest>(expandedQuests);
				////IEnumerable<Quest> items = await questTable.ToEnumerableAsync();
				////return new ObservableCollection<Quest>(items);
    //        }
    //        catch (MobileServiceInvalidOperationException msioe)
    //        {
    //            Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
    //        }
    //        catch (Exception e)
    //        {
    //            Debug.WriteLine(@"Sync error: {0}", e.Message);
    //        }
    //        return null;
    //    }

        public async Task SaveQuestAsync(Quest quest)
		{
			if (quest.Id == null)
			{
				await quests.InsertAsync(quest);
			}
			else
			{
				await quests.UpdateAsync(quest);
			}
		}

		public async Task SavePlayerAsync(Player player)
		{
			if (player.Id == null)
			{
				await players.InsertAsync(player);
			}
			else
			{
				await players.UpdateAsync(player);
			}
		}

		//public async Task SaveGameAsync(Game game)
		//{
		//	if (game.Id == null)
		//	{
		//		await games.InsertAsync(game);
		//	}
		//	else
		//	{
		//		await games.UpdateAsync(game);
		//	}
		//}


		public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

			try
			{
				await quests.PushFileChangesAsync();
				await client.SyncContext.PushAsync();

				await topics.PullAsync("allTopics", topics.CreateQuery());
				await comptencies.PullAsync("allCompetencies", comptencies.CreateQuery());
				await players.PullAsync("allPlayers", players.CreateQuery());
				await quests.PullAsync("allQuests", quests.CreateQuery());

			}
			catch (MobileServicePushFailedException exc)
			{
				if (exc.PushResult != null)
				{
					syncErrors = exc.PushResult.Errors;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
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

		internal Task DownloadFileAsync(MobileServiceFile file)
		{
			lock (currentDownloadTaskLock)
			{
				return currentDownloadTask =
					currentDownloadTask.ContinueWith(x => DoDownloadFileAsync(file)).Unwrap();
			}
		}


		internal async Task DoDownloadFileAsync(MobileServiceFile file)
        {
			IPlatform platform = DependencyService.Get<IPlatform>();

			await platform.DownloadFileAsync(this.quests, file);
        }



        internal async Task<MobileServiceFile> AddImage(Quest quest, string imagePath)
        {
			Debug.WriteLine($"reQuestService:AddImage: {imagePath}");
			return await this.quests.AddFileAsync(quest, System.IO.Path.GetFileName(imagePath));
		}

        internal async Task DeleteImage(Quest quest, MobileServiceFile file)
        {
			await this.quests.DeleteFileAsync(file);
        }

        internal async Task<IEnumerable<MobileServiceFile>> GetImageFilesAsync(Quest quest)
        {
			return await this.quests.GetFilesAsync(quest);
        }

	}
}
