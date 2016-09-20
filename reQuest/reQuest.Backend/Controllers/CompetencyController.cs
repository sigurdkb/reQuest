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
    public class CompetencyController : TableController<Competency>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Competency>(context, Request);
        }

        // GET tables/Competency
        public IQueryable<Competency> GetAllCompetency()
        {
            return Query(); 
        }

        // GET tables/Competency/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Competency> GetCompetency(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Competency/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Competency> PatchCompetency(string id, Delta<Competency> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Competency
        public async Task<IHttpActionResult> PostCompetency(Competency item)
        {
            Competency current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Competency/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteCompetency(string id)
        {
             return DeleteAsync(id);
        }
    }
}
