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
    public class TracksViewModel
    {
        public string trackHeading { get; set; }
        public List<Track> Tracks { get; set; }
        public Track Track { get; set; } 
    }
}