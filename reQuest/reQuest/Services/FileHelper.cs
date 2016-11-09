using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PCLStorage;
using Xamarin.Forms;
using reQuest.Interfaces;

namespace reQuest.Services
{
    public class FileHelper
    {
        public static async Task<string> CopyQuestFileAsync(string itemId, string filePath)
        {
            IFolder localStorage = FileSystem.Current.LocalStorage;

            string fileName = Path.GetFileName(filePath);
            string targetPath = await GetLocalFilePathAsync(itemId, fileName);

            var sourceFile = await localStorage.GetFileAsync(filePath);
            var sourceStream = await sourceFile.OpenAsync(FileAccess.Read);

            var targetFile = await localStorage.CreateFileAsync(targetPath, CreationCollisionOption.ReplaceExisting);

            using (var targetStream = await targetFile.OpenAsync(FileAccess.ReadAndWrite))
            {
                await sourceStream.CopyToAsync(targetStream);
            }

            return targetPath;
        }

        public static async Task<string> GetLocalFilePathAsync(string itemId, string fileName)
        {
            IPlatform platform = DependencyService.Get<IPlatform>();

            string recordFilesPath = Path.Combine(await platform.GetQuestFilesPathAsync(), itemId);

            var checkExists = await FileSystem.Current.LocalStorage.CheckExistsAsync(recordFilesPath);
            if (checkExists == ExistenceCheckResult.NotFound)
            {
                await FileSystem.Current.LocalStorage.CreateFolderAsync(recordFilesPath, CreationCollisionOption.ReplaceExisting);
            }

            return Path.Combine(recordFilesPath, fileName);
        }

        public static async Task DeleteLocalFileAsync(Microsoft.WindowsAzure.MobileServices.Files.MobileServiceFile fileName)
        {
            string localPath = await GetLocalFilePathAsync(fileName.ParentId, fileName.Name);
            var checkExists = await FileSystem.Current.LocalStorage.CheckExistsAsync(localPath);

            if (checkExists == ExistenceCheckResult.FileExists)
            {
                var file = await FileSystem.Current.LocalStorage.GetFileAsync(localPath);
                await file.DeleteAsync();
            }
        }
    }
}
