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
            //List<Topic> topics = new List<Topic>
            //{
            //    new Topic { Id = new Guid("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa").ToString(), ShortName = "MM-501", LongName = "Elearning and Games", Description = "This course introduces students to the theory concerning E-learning and gamification as well as the fundamentals of designing games with an emphasis on E-learning. An in-depth analysis of essential game concepts such as goals, rules, uncertainty, social play and meaningful interaction provides some of the theoretical framework for evaluating existing games and creating new ones. A series of design challenges encourage students to both utilize established solutions and to think out of the box when combining media elements into good play and E-learning experiences. Students will collaborate to develop their own e-learning prototypes. During the course the student will learn how to choose when an E-learning game is the right solution, for enhancing teaching, and when it is not. Students will also be challenged to create resources based on specific scenarios presented.", Locked = true },
            //    new Topic { Id = new Guid("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb").ToString(), ShortName = "MM-503", LongName = "Multimedia Project", Description = "The course is based upon on-going research at the Faculty, or relevant needs of the University of Agder or national industry, and provides advanced project oriented knowledge within selected topics. Individually or in small groups, students specialize in tasks approved by members of the academic staff.", Locked = true },
            //    new Topic { Id = new Guid("cccccccccccccccccccccccccccccccc").ToString(), ShortName = "MM-403", LongName = "Interaction Design", Description = "Interaction Design is the process of designing usable and user-centred interactive systems. This course draws from different research areas including human-computer interaction, user-centred design, interface design, psychology and computer science. Students learn the theory and practical techniques to support development of usable interactive systems. The course includes an introduction to interaction design; concept development with regards to the context of use and user requirements; a variety of required tools and methods for designing and prototyping as well as user testing and evaluation of interactive systems. Students learn how to design interactive systems considering users, the usability of the system in a specific context of use and the user experience arising through the use of the system. The course provides the competence to discuss design results and alternatives.", Locked = true },
            //    new Topic { Id = new Guid("dddddddddddddddddddddddddddddddd").ToString(), ShortName = "", LongName = "Adobe Photoshop", Description = "Basic Adobe Photoshop skills", Locked = false }
            //};


            //context.Set<Topic>().AddRange(topics);

            //List<Player> players = new List<Player>()
            //{
            //    new Player {
            //        Id  = new Guid("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa11").ToString(), Longitude = 8.578026, Latitude = 58.333859, ExternalId = "A3100",
            //        Competencies = new List<Competency>()
            //        {
            //            new Competency { Id = new Guid("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa1").ToString(), Topic = topics.Find(t => t.ShortName == "MM-501"), Score = 10 },
            //            new Competency { Id = new Guid("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa2").ToString(), Topic = topics.Find(t => t.ShortName == "MM-503"), Score = 20 },
            //            new Competency { Id = new Guid("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa3").ToString(), Topic = topics.Find(t => t.ShortName == "MM-403"), Score = 30 },
            //        },
            //         Score = 60
            //    },
            //    new Player {
            //        Id  = new Guid("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbb11").ToString(), Longitude = 8.577447, Latitude = 58.334698, ExternalId = "Bluebox",
            //        Competencies = new List<Competency>()
            //        {
            //            new Competency { Id = new Guid("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb1").ToString(), Topic = topics.Find(t => t.ShortName == "MM-501"), Score = 11 },
            //            new Competency { Id = new Guid("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb2").ToString(), Topic = topics.Find(t => t.ShortName == "MM-503"), Score = 22 },
            //            new Competency { Id = new Guid("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb3").ToString(), Topic = topics.Find(t => t.ShortName == "MM-403"), Score = 33 }
            //        },
            //         Score = 66
            //    },

            //};

            //context.Set<Player>().AddRange(players);


            base.Seed(context);
        }
    }
}

