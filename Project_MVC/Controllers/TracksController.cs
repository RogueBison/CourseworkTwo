using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Project_MVC.Models;

using ChinookEntities;
using ChinookContext;

namespace Project_MVC.Controllers
{
    public class TracksController : Controller
    {
        public IActionResult Index(string id)
        {
            TracksViewModel model = new TracksViewModel();
            ChinookDatabase db = new ChinookDatabase();
            // if no album id is passed to action, select all the tracks in the database
            if (id == null)
            {
                model.trackHeading = "Chinook Track Information";
                model.Tracks = db.Tracks.ToList();
                return View(model);
            }
            // if the id is present, use the value to select only the tracks associated with the album id
            else
            {
                var albumTitle = db.Albums.Where(al => al.AlbumId == Int32.Parse(id)).Select(al => al.Title).FirstOrDefault();
                var albumArtist = db.Albums.Where(al => al.AlbumId == Int32.Parse(id)).Select(al => al.Artist.Name).FirstOrDefault();
                model.trackHeading = $"All Tracks | '{albumTitle}' ({albumArtist})";
                model.Tracks = db.Tracks.Where(a => a.AlbumId == Int32.Parse(id)).ToList();
                return View(model);
            }
        }
    }
}