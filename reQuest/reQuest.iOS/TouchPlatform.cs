using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.Files;
using Microsoft.WindowsAzure.MobileServices.Files.Metadata;
using Microsoft.WindowsAzure.MobileServices.Files.Sync;
using Microsoft.WindowsAzure.MobileServices.Sync;
using reQuest.Interfaces;
using reQuest.Services;

[assembly: Xamarin.Forms.Dependency(typeof(reQuest.iOS.TouchPlatform))]
namespace reQuest.iOS
{
    class TouchPlatform : IPlatform
    {
        public async Task DownloadFileAsync<T>(IMobileServiceSyncTable<T> table, MobileServiceFile file, string filename)
        {
            var path = await FileHelper.GetLocalFilePathAsync(file.ParentId, file.Name);
            await table.DownloadFileAsync(file, path);
        }

        public async Task<IMobileServiceFileDataSource> GetFileDataSource(MobileServiceFileMetadata metadata)
        {
            var filePath = await FileHelper.GetLocalFilePathAsync(metadata.ParentDataItemId, metadata.FileName);
            return new PathMobileServiceFileDataSource(filePath);
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

        public Task<string> GetQuestFilesPathAsync()
        {
            string filesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "QuestFiles");

            if (!Directory.Exists(filesPath))
            {
                Directory.CreateDirectory(filesPath);
            }

            return Task.FromResult(filesPath);
        }

    }
}
