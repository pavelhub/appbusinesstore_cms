using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AppB2.Custom;

namespace AppB2.Models
{
    public class NewOwner
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Phone { get; set; }

        [FranchizeExist(ErrorMessageResourceType = typeof(Localization.Account.Resource), ErrorMessageResourceName = "Franchize_Not_Exist")]
        public int FranchizerId { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}