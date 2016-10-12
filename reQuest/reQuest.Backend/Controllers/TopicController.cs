using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using reQuest.Backend.DataObjects;
using reQuest.Backend.Models;

namespace reQuest.Backend.Controllers
{
    public class TopicController : TableController<Topic>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Topic>(context, Request);
        }

        // GET tables/Topic
        public IQueryable<Topic> GetAllTopic()
        {
            return Query(); 
        }

        // GET tables/Topic/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Topic> GetTopic(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Topic/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Topic> PatchTopic(string id, Delta<Topic> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Topic
        public async Task<IHttpActionResult> PostTopic(Topic item)
        {
            Topic current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Topic/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTopic(string id)
        {
             return DeleteAsync(id);
        }
    }
}
