using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reQuest.Interfaces
{
	public interface IAuthenticateData
	{
		string UserID { get; set; }
		string UserToken { get; set; }
	}
 
	public interface IAuthenticate
    {
        Task<bool> Authenticate();

		event EventHandler<IAuthenticateData> userAuthenticated;

    }

}
