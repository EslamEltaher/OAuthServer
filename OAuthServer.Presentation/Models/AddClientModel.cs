using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAuthServer.Presentation.Models
{
    public class AddClientModel
    {
        public string Client_Name { get; set; }
        public string Redirect_Uri { get; set; }
    }
}
