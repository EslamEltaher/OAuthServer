using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OAuthServer.Presentation.Models
{
    public class AuthorizeModel
    {
        [Required]
        public string client_id { get; set; }
        [Required]
        public string response_type { get; set; }
        [Required]
        public string redirect_uri { get; set; }
        [Required]
        public string scope { get; set; }
        public string state { get; set; }
    }
}
