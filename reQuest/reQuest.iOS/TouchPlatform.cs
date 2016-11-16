using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.Files;
using Microsoft.WindowsAzure.MobileServices.Files.Metadata;
using Microsoft.WindowsAzure.MobileServices.Files.Sync;
using Microsoft.WindowsAzure.MobileServices.Sync;
using PCLStorage;
using reQuest.Interfaces;
using reQuest.Services;

[assembly: Xamarin.Forms.Dependency(typeof(reQuest.iOS.TouchPlatform))]
namespace reQuest.iOS
{
    class TouchPlatform : IPlatform
    {
        public async Task DownloadFileAsync<T>(IMobileServiceSyncTable<T> table, MobileServiceFile file)
        {
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			IFolder reQuestFolder = await rootFolder.CreateFolderAsync("reQuest", CreationCollisionOption.OpenIfExists);
			string path = System.IO.Path.Combine(reQuestFolder.Path, file.ParentId + ".jpg");
			Debug.WriteLine($"TouchPlatform:DownloadFileAsync: {path}");

			await table.DownloadFileAsync(file, path);
        }

        public async Task<IMobileServiceFileDataSource> GetFileDataSource(MobileServiceFileMetadata metadata)
        {
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			IFolder reQuestFolder = await rootFolder.CreateFolderAsync("reQuest", CreationCollisionOption.OpenIfExists);
			string path = System.IO.Path.Combine(reQuestFolder.Path, metadata.ParentDataItemId + ".jpg");
			Debug.WriteLine($"TouchPlatform:GetFileDataSource: {path}");

            return new PathMobileServiceFileDataSource(path);
        }

        //public async Task<string> TakePhotoAsync(object context)
        //{
        //    try
        //    {
        //        var mediaPicker = new MediaPicker();
        //        var mediaFile = await mediaPicker.PickPhotoAsync();
        //        return mediaFile.Path;
        //    }
        //    catch (TaskCanceledException)
        //    {
        //        return null;
        //    }
        //}

        //public Task<string> GetQuestFilesPathAsync()
        //{
        //    string filesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "reQuest");

        //    if (!Directory.Exists(filesPath))
        //    {
        //        Directory.CreateDirectory(filesPath);
        //    }

        //    return Task.FromResult(filesPath);
        //}

    }
}
