using reQuest.Models;
using reQuest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace reQuest
{
    public partial class QuestPage : ContentPage
    {
        private reQuestService service;
        public Quest Quest { get; set; }
        public QuestPage(Quest quest, reQuestService service)
        {
            InitializeComponent();
            this.Title = quest.Title;
            this.Quest = quest;
            this.service = service;
            this.BindingContext = this;
        }
    }
}
