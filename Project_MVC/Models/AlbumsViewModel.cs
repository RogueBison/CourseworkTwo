using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using ChinookEntities;
using ChinookContext;

namespace Project_MVC.Models
{
    public class AlbumsViewModel
    {
        public string Heading { get; set; }
        public string orderedBy{ get; set; }
        public string searchString{ get; set; }
        public List<Album> Albums { get; set; } 
        public List<Artist> Artists { get; set; }
        public Album Album { get; set; }

        /* Tracks */

        public string trackHeading { get; set; }
        public List<Track> Tracks { get; set; }
        public Track Track { get; set; }
        
    }
}