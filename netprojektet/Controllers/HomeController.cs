﻿using Microsoft.AspNetCore.Mvc;
using Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using System.ComponentModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using DataAccessLayer;

namespace netprojektet.Controllers
{
    public class HomeController : Controller
    {
        private LinkedoutDbContext linkedoutDbContext;


        public HomeController(LinkedoutDbContext DbContext)
        {
            linkedoutDbContext = DbContext;
        }

        
        public IActionResult Index()
        {
            var model = new ProfileProjectViewModel();
            
            List<Profile> profileListFull = linkedoutDbContext.Profiles.ToList();
            List<Profile> profileListLimited = linkedoutDbContext.Profiles.Where(e => e.Private == false).ToList();
            Project projekt = linkedoutDbContext.Projects.OrderByDescending(e => e.Id).FirstOrDefault();
            
            model.senasteProject = projekt;
            if (User.Identity.IsAuthenticated)
            {
                model.profiles = profileListFull;
            }
            else { 
            model.profiles = profileListLimited;
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Search()
        {
            string searchQuery = Request.Form["Query"].ToString();
            var searchQuerys = searchQuery.Split(" ");
            List<Profile> results = new List<Profile>();
            List<Profile> profiles = new List<Profile>();

            if(searchQuery == null)
            {
                return View();
            }
            if(searchQuerys.Length == 1) {
                
                profiles = linkedoutDbContext.Profiles.Where(p => p.FirstName.Contains(searchQuery) || p.LastName.Contains(searchQuery)).ToList();
            
            }
            if(searchQuerys.Length == 2)
            {

                foreach (var query in searchQuerys)
                {

                    List<Profile> queryResults = linkedoutDbContext.Profiles.Where(p => p.FirstName.Contains(searchQuerys[0]) && p.LastName.Contains(searchQuerys[1])
                                                    || p.LastName.Contains(searchQuerys[0]) && p.FirstName.Contains(searchQuerys[1])).ToList();

                    foreach(Profile queryProfile in queryResults)
                    {
                        profiles.Add(queryProfile);
                    }

                }

            }
            else
            {
                foreach (var query in searchQuerys)
                {


                    List<Profile> queryResults = linkedoutDbContext.Profiles
                             .Where(p => p.FirstName.Contains(query) || p.LastName.Contains(query)).ToList();

                    foreach (Profile queryProfile in queryResults)
                    {
                        profiles.Add(queryProfile);
                    }

                }

            }
            foreach (Profile profile in profiles)
            {
                if (results.Contains(profile))
                {

                }
                else
                {
                    results.Add(profile);
                }
            }
            return View(results);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Projects()
        {
            List<Project> projectlist = linkedoutDbContext.Projects.ToList();
            return View(projectlist);
        }
    }
}