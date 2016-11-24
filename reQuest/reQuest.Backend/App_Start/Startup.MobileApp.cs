using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using reQuest.Backend.DataObjects;
using reQuest.Backend.Models;
using Owin;
using Newtonsoft.Json;

namespace reQuest.Backend
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            config.MapHttpAttributeRoutes();

            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new MobileServiceInitializer());

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    // This middleware is intended to be used locally for debugging. By default, HostName will
                    // only have a value when running in an App Service application.
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }

            app.UseWebApi(config);
        }
    }

    public class MobileServiceInitializer : DropCreateDatabaseIfModelChanges<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {
            List<Topic> topics = new List<Topic>()
            {
                new Topic { Id = "44b11725-5225-43a1-b96a-167a8ce4ee16", ShortName = "MM-501", LongName = "Elearning and Games", Description = "This course introduces students to the theory concerning E-learning and gamification as well as the fundamentals of designing games with an emphasis on E-learning. An in-depth analysis of essential game concepts such as goals, rules, uncertainty, social play and meaningful interaction provides some of the theoretical framework for evaluating existing games and creating new ones. A series of design challenges encourage students to both utilize established solutions and to think out of the box when combining media elements into good play and E-learning experiences. Students will collaborate to develop their own e-learning prototypes. During the course the student will learn how to choose when an E-learning game is the right solution, for enhancing teaching, and when it is not. Students will also be challenged to create resources based on specific scenarios presented.", Locked = true },
                new Topic { Id = "d0215911-ce09-47b3-b2ef-80e829384082", ShortName = "MM-503", LongName = "Multimedia Project", Description = "The course is based upon on-going research at the Faculty, or relevant needs of the University of Agder or national industry, and provides advanced project oriented knowledge within selected topics. Individually or in small groups, students specialize in tasks approved by members of the academic staff.", Locked = true },
                new Topic { Id = "9c36b1b7-0677-4de8-8bdd-af6141370533", ShortName = "MM-403", LongName = "Interaction Design", Description = "Interaction Design is the process of designing usable and user-centred interactive systems. This course draws from different research areas including human-computer interaction, user-centred design, interface design, psychology and computer science. Students learn the theory and practical techniques to support development of usable interactive systems. The course includes an introduction to interaction design; concept development with regards to the context of use and user requirements; a variety of required tools and methods for designing and prototyping as well as user testing and evaluation of interactive systems. Students learn how to design interactive systems considering users, the usability of the system in a specific context of use and the user experience arising through the use of the system. The course provides the competence to discuss design results and alternatives.", Locked = true },
            };


            context.Set<Topic>().AddRange(topics);

            List<Player> players = new List<Player>()
            {
                new Player {
                    Id  = "963407bf-c9e0-40d3-b84b-370a690e74d2", Longitude = 8.578026, Latitude = 58.333859, ExternalId = "sigurdkb",
                    Competencies = JsonConvert.SerializeObject(new List<string>() { "44b11725-5225-43a1-b96a-167a8ce4ee16", "d0215911-ce09-47b3-b2ef-80e829384082" }),
                     Score = 60
                },
                new Player {
                    Id  = "7326733c-6029-4582-8eee-82abe9da7f5c", Longitude = 8.577447, Latitude = 58.334698, ExternalId = "josteinn",
                    Competencies = JsonConvert.SerializeObject(new List<string>() { "d0215911-ce09-47b3-b2ef-80e829384082", "44b11725-5225-43a1-b96a-167a8ce4ee16" }),
                     Score = 66
                },
                new Player {
                    Id  = "4703ca5c-d855-41d0-8814-4c1814597234", Longitude = 8.576447, Latitude = 58.335698, ExternalId = "mariub06",
                    Competencies = JsonConvert.SerializeObject(new List<string>() { "9c36b1b7-0677-4de8-8bdd-af6141370533" }),
                     Score = 80
                },

            };

            context.Set<Player>().AddRange(players);

            List<Quest> quests = new List<Quest>()
            {
                new Quest { Id = "35af0d83-6614-4b72-8c59-e070dd93f1a7", Title = "Explain usability testing", TimeLimit = new TimeSpan(0,15,0), OwnerId = "963407bf-c9e0-40d3-b84b-370a690e74d2", TopicId = "9c36b1b7-0677-4de8-8bdd-af6141370533" },
                new Quest { Id = "3de9b625-1073-454d-81ba-8df1c38394cb", Title = "Need help with report outline", TimeLimit = new TimeSpan(0,30,0), OwnerId = "7326733c-6029-4582-8eee-82abe9da7f5c", TopicId = "44b11725-5225-43a1-b96a-167a8ce4ee16" },
                new Quest { Id = "351d3a9a-910c-4fab-86e5-b35413d8120e", Title = "Help with xamarin forms", TimeLimit = new TimeSpan(0,5,0), OwnerId = "963407bf-c9e0-40d3-b84b-370a690e74d2", TopicId = "d0215911-ce09-47b3-b2ef-80e829384082" }
            };

            context.Set<Quest>().AddRange(quests);


            base.Seed(context);
        }
    }
}

