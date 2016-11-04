using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.Azure.Mobile.Server.Authentication;
using System.Threading.Tasks;

namespace reQuest.Backend.Controllers
{
    // Use the MobileAppController attribute for each ApiController you want to use  
    // from your mobile clients 
    [MobileAppController]
    public class ValuesController : ApiController
    {
        // GET api/values
        public async Task<string> Get()
        {
            // Get the SID of the current user.
            //var claimsPrincipal = this.User as ClaimsPrincipal;
            //string sid = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;
            //return sid;

            // Get the credentials for the logged-in user.
            var credentials =
                await this.User
                .GetAppServiceIdentityAsync<MicrosoftAccountCredentials>(this.Request);

            //if (credentials.Provider == "MicrosoftAccount")
            //{
                // Create a query string with the Facebook access token.
                var maRequestUrl = "https://apis.live.net/v5.0/me/?method=GET&access_token="
                    + credentials.AccessToken;

                // Create an HttpClient request.
                var client = new System.Net.Http.HttpClient();

                // Request the current user info from Facebook.
                var resp = await client.GetAsync(maRequestUrl);
                resp.EnsureSuccessStatusCode();

                // Do something here with the Facebook user information.
                var maInfo = await resp.Content.ReadAsStringAsync();

            //}
            return maInfo; 
        }

        // POST api/values
        public string Post()
        {
            return "Hello World!";
        }
    }
}
