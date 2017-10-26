using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgeRanger.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
    }
}