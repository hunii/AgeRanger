using AgeRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgeRanger.ViewModels
{
    public class ProfileListViewModel
    {
        public IEnumerable<Profile> Profiles { get; set; }
        public int Count { get; set; }
    }
}