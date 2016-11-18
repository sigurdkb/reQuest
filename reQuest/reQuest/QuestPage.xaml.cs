using reQuest.Models;
using reQuest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using reQuest.ViewModels;

namespace reQuest
{
    public partial class QuestPage : ContentPage
    {
        private reQuestService service;
        public QuestViewModel Quest { get; set; }
        public QuestPage(QuestViewModel quest, reQuestService service)
        {
            InitializeComponent();
            this.Title = quest.Title;
            this.service = service;
			this.Quest = quest;

			this.BindingContext = Quest;
        }
    }
}
