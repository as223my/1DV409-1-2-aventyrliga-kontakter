using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdventurousContacts.Models
{
    [MetadataType(typeof(Contact_Metadata))]
    public partial class Contact
    {

        public class Contact_Metadata
        {
            [DataType(DataType.EmailAddress, ErrorMessage = "The E-mail address field is not a valid e-mail address.")]
            [StringLength(50, ErrorMessage = "Email address can contain a maximum of 50 characters!")]
            [Required(ErrorMessage = "The E-mail address field is required!")]
            public string EmailAddress { get; set; }

            [StringLength(50, ErrorMessage = "First name can contain a maximum of 50 characters!")]
            [Required(ErrorMessage = "The First name field is required!")]
            public string FirstName { get; set; }

            [StringLength(50, ErrorMessage = "Last name can contain a maximum of 50 characters!")]
            [Required(ErrorMessage = "The Last name field is required!")]
            public string LastName { get; set; }
        }
    }
}