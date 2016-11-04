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
    public class GameController : TableController<Game>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Game>(context, Request);
        }

        // GET tables/Game
        public IQueryable<Game> GetAllGame()
        {
            return Query(); 
        }

        // GET tables/Game/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Game> GetGame(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Game/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Game> PatchGame(string id, Delta<Game> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Game
        public async Task<IHttpActionResult> PostGame(Game item)
        {
            Game current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Game/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteGame(string id)
        {
             return DeleteAsync(id);
        }
    }
}
