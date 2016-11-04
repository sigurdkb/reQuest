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
    public class QuestController : TableController<Quest>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Quest>(context, Request);
        }

        // GET tables/Quest
        public IQueryable<Quest> GetAllQuest()
        {
            return Query(); 
        }

        // GET tables/Quest/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Quest> GetQuest(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Quest/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Quest> PatchQuest(string id, Delta<Quest> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Quest
        public async Task<IHttpActionResult> PostQuest(Quest item)
        {
            Quest current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Quest/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteQuest(string id)
        {
             return DeleteAsync(id);
        }
    }
}
