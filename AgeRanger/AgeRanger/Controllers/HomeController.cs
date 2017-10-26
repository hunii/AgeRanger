using AgeRanger.Infrastructure.Private;
using AgeRanger.Infrastructure.Services;
using AgeRanger.Models;
using AgeRanger.ViewModels;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgeRanger.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string search)
        {
            if(search == null){search = "";}
            ViewBag.SearchTerm = search;
            using (var conn = new SQLiteConnection(Config.Current.ConnectionString))
            {
                var profileList =  PersonService.Search(conn, search);
                return View(profileList);
            }
        }


        [HttpPost]
        public ActionResult Create(ProfileCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var conn = new SQLiteConnection(Config.Current.ConnectionString))
                {
                    PersonService.Create(conn, model);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Modify(int? id)
        {
            if (id.HasValue)
            {
                using (var conn = new SQLiteConnection(Config.Current.ConnectionString))
                {
                    var profile = PersonService.GetProfile(conn, id.Value);
                    if (profile != null)
                    {
                        var viewModel = new ProfileModifyViewModel()
                        {
                            Id = profile.Id,
                            FirstName = profile.FirstName,
                            LastName = profile.LastName,
                            Age = profile.Age
                        };
                        return View(viewModel);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Modify(ProfileModifyViewModel model)
        {
            using (var conn = new SQLiteConnection(Config.Current.ConnectionString))
            {
                PersonService.ModifyProfile(conn, model);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                using (var conn = new SQLiteConnection(Config.Current.ConnectionString))
                {
                    PersonService.DeleteProfile(conn, id.Value);
                }
            }
            return RedirectToAction("Index", "Home");
        }

    }
}