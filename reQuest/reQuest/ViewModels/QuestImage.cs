using Microsoft.WindowsAzure.MobileServices.Files;
using reQuest.Models;
using reQuest.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reQuest.ViewModels
{
    public class QuestImage : INotifyPropertyChanged
    {
        private string name;
        private string uri;

        public MobileServiceFile File { get; private set; }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Uri
        {
            get { return uri; }
            set
            {
                uri = value;
                OnPropertyChanged(nameof(Uri));
            }
        }

        public QuestImage(MobileServiceFile file, Quest todoItem)
        {
            Name = file.Name;
            File = file;

            FileHelper.GetLocalFilePathAsync(todoItem.Id, file.Name).ContinueWith(x => this.Uri = x.Result);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
