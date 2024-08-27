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
    public class AlbumsController : Controller
    {
        public IActionResult Index()
        {
            AlbumsViewModel model = new AlbumsViewModel();
            ChinookDatabase db = new ChinookDatabase();
            model.Heading = "Chinook Album Information";
            model.Albums = db.Albums.Include(ar => ar.Artist).ToList();
            return View(model);
        }
        public IActionResult InsertPage()
        {
            AlbumsViewModel model = new AlbumsViewModel();
            ChinookDatabase db = new ChinookDatabase();
            model.Heading = "Insert a New Album";
            model.Artists = db.Artists.ToList();
            return View(model);
        }
        public IActionResult Insert(string Title, short ArtistId, List<string> Tracks)
        {
            ChinookDatabase db = new ChinookDatabase();
            Album newAlbum = new Album
            {
                Title = Title,
                ArtistId = ArtistId
            };
            db.Albums.Add(newAlbum);
            db.SaveChanges();

            if (Tracks != null)
            {
                foreach (string trackName in Tracks)
                {
                    if (!string.IsNullOrEmpty(trackName))
                    {
                        Track newTrack = new Track
                        {
                            AlbumId = newAlbum.AlbumId,
                            Name = trackName,
                            MediaTypeId = 1,
                            GenreId = 1,
                            Composer = "TBD",
                            Milliseconds = 1111,
                            UnitPrice = 0.99
                        };
                        db.Tracks.Add(newTrack);
                    }
                }
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult updateForm(string id)
        {
            AlbumsViewModel model = new AlbumsViewModel();
            ChinookDatabase db = new ChinookDatabase();
            var albumTitle = db.Albums.Where(al => al.AlbumId == Int32.Parse(id)).Select(al => al.Title).FirstOrDefault();
            model.Heading = $"Update | '{albumTitle}'";
            model.Album = db.Albums.Include(ar => ar.Artist).FirstOrDefault(al => al.AlbumId == Int32.Parse(id));
            if (@model.Album == null)
            {
                return RedirectToAction("Index");
            }
            model.Tracks = db.Tracks.Where(a => a.AlbumId == Int32.Parse(id)).ToList();

            return View(model);
        }
        public IActionResult Update(string id, string Title, string ArtistName, string[] TrackNames, int[] TrackIds, string NewTrackName)
        {
            ChinookDatabase db = new ChinookDatabase();
            AlbumsViewModel model = new AlbumsViewModel();
            model.Album = db.Albums.Include(ar => ar.Artist).FirstOrDefault(al => al.AlbumId == Int32.Parse(id));
            model.Tracks = db.Tracks.Where(t => t.AlbumId == Int32.Parse(id)).ToList();
            if (@model.Album == null)
            {
                return RedirectToAction("Index");
            }

            // Update album
            if (!string.IsNullOrEmpty(Title) && model.Album.Title != Title)
            {
                model.Album.Title = Title;
            }
            if (!string.IsNullOrEmpty(ArtistName) && model.Album.Artist.Name != ArtistName)
            {
                model.Album.Artist.Name = ArtistName;
            }

            // Update album tracks
            for (int i = 0; i < model.Tracks.Count; i++)
            {
                if (TrackIds != null && i < TrackIds.Length && TrackIds[i] != 0 && !string.IsNullOrEmpty(TrackNames[i]))
                {
                    int trackId = TrackIds[i];
                    Track track = db.Tracks.FirstOrDefault(t => t.TrackId == trackId);
                    if (track != null && track.Name != TrackNames[i])
                    {
                        track.Name = TrackNames[i];
                    }
                }
            }

            // Add new track to album
            if (!string.IsNullOrEmpty(NewTrackName))
            {
                Track newTrack = new Track
                {
                    AlbumId = model.Album.AlbumId,
                    Name = NewTrackName,
                    MediaTypeId = 1,
                    GenreId = 1,
                    Composer = "TBD",
                    Milliseconds = 1111,
                    UnitPrice = 0.99
                };
                db.Tracks.Add(newTrack);
            }
            

            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult DeleteSingleTrack(int id)
        {
            ChinookDatabase db = new ChinookDatabase();
            Track track = db.Tracks.FirstOrDefault(t => t.TrackId == id);
            var playlistTracksToDelete = db.Playlist_Tracks.Where(pt => pt.TrackId == track.TrackId).ToList();
            db.Playlist_Tracks.RemoveRange(playlistTracksToDelete);

            var invoiceItemsToDelete = db.invoice_Items.Where(ii => ii.TrackId == track.TrackId).ToList();
            db.invoice_Items.RemoveRange(invoiceItemsToDelete);

            if (track != null)
            {
                db.Tracks.Remove(track);
                db.SaveChanges();
            }
            return RedirectToAction("UpdateForm", new { id = track.AlbumId });
        }
        public IActionResult deletePage(string id)
        {
            AlbumsViewModel model = new AlbumsViewModel();
            ChinookDatabase db = new ChinookDatabase();
            var albumTitle = db.Albums.Where(al => al.AlbumId == Int32.Parse(id)).Select(al => al.Title).FirstOrDefault();
            var albumArtist = db.Albums.Where(al => al.AlbumId == Int32.Parse(id)).Select(al => al.Artist.Name).FirstOrDefault();
            model.trackHeading = $"'{albumTitle}' ({albumArtist})";
            model.Tracks = db.Tracks.Where(a => a.AlbumId == Int32.Parse(id)).ToList();
            model.Track = db.Tracks.FirstOrDefault(a => a.AlbumId == Int32.Parse(id));

            return View(model);
        }
        public IActionResult Delete(string id)
        {
            ChinookDatabase db = new ChinookDatabase();
            var tracksToDelete = db.Tracks.Where(t => t.AlbumId == Int32.Parse(id)).ToList();
            foreach (var track in tracksToDelete)
            {
                var playlistTracksToDelete = db.Playlist_Tracks.Where(pt => pt.TrackId == track.TrackId).ToList();
                db.Playlist_Tracks.RemoveRange(playlistTracksToDelete);
            }

            foreach (var track in tracksToDelete)
            {
                var invoiceItemsToDelete = db.invoice_Items.Where(ii => ii.TrackId == track.TrackId).ToList();
                db.invoice_Items.RemoveRange(invoiceItemsToDelete);
            }
          
            db.Tracks.RemoveRange(tracksToDelete);

            Album delAlbum = db.Albums.Single(al => al.AlbumId == Int32.Parse(id));
            db.Albums.Remove(delAlbum);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Search(string searchText)
        {
            ChinookDatabase db = new ChinookDatabase();
            AlbumsViewModel model = new AlbumsViewModel();
            if (string.IsNullOrEmpty(searchText))
            {
                model.searchString = "No results";
                model.Albums = db.Albums.Include(ar => ar.Artist).ToList();
            }
            else
            {
                model.searchString = $"Search results for '{searchText}'";
                model.Albums = db.Albums.Where(a => a.Title.Contains(searchText)
                || a.Artist.Name.Contains(searchText)).Include(ar => ar.Artist).ToList();
            }

            return View(model);
        }
        public IActionResult Order()
        {
            AlbumsViewModel model = new AlbumsViewModel();
            ChinookDatabase db = new ChinookDatabase();
            var sortType = Request.Form["orderBy"];
            if (sortType == "T_ASC")
            {
                model.Albums = db.Albums.Include(ar => ar.Artist).OrderBy(t => t.Title).ToList();
                model.orderedBy = "Albums Ascending Order [A-Z]";
            }
            else if (sortType == "T_DESC")
            {
                model.Albums = db.Albums.Include(ar => ar.Artist).OrderByDescending(t => t.Title).ToList();
                model.orderedBy = "Albums Descending Order [Z-A]";
            }
            else if (sortType == "A_ASC")
            {
                model.Albums = db.Albums.Include(ar => ar.Artist).OrderBy(an => an.Artist.Name).ToList();
                model.orderedBy = "Artists Ascending Order [Z-A]";
            }
            else if (sortType == "A_DESC")
            {
                model.Albums = db.Albums.Include(ar => ar.Artist).OrderByDescending(an => an.Artist.Name).ToList();
                model.orderedBy = "Artists Descending Order [Z-A]";
            }
            return View(model);
        }
    }
}