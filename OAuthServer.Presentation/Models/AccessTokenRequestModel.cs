using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OAuthServer.Presentation.Models
{
    public class AccessTokenRequestModel
    {
        [Required]
        public string Grant_Type { get; set; }
        [Required]
        public string Code { get; set; }
        
        [Required]
        public string Client_Id { get; set; }
        public string Client_Secret { get; set; }

        public string Redirect_Uri { get; set; }
    }
}
