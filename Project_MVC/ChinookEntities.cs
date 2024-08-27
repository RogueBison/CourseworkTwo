using System;
using System.Collections.Generic;

namespace ChinookEntities
{
    public class Album
    {
        public Int16 AlbumId { get; set; }
        public String Title { get; set; }
        public Int16 ArtistId { get; set; }
        public Artist Artist { get; set; }
        public ICollection<Track> Tracks { get; set; }
    }
    public class Artist
    {
        public Int16 ArtistId { get; set; }
        public String Name { get; set; }
        public ICollection<Album> Albums { get; set; }
    }
    public class Genre
    {
        public Int16 GenreId { get; set; }
        public String Name { get; set; }
    }
    public class Invoice_Item
    {
        public Int16 InvoiceLineId { get; set; }
        public Int16 InvoiceId { get; set; }
        public Int16 TrackId { get; set; }
        public Double UnitPrice { get; set; }
        public Int16 Quantity { get; set; }
        public Track Track { get; set; }
    }
    public class Invoice
    {
        public Int16 InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public Int16 CustomerId { get; set; }
        public String BillingAddress { get; set; }
        public String BillingCity { get; set; }
        public String BillingPostalCode { get; set; }
        public String BillingCountry { get; set; }
        public String BillingState { get; set; }
        public Double Total { get; set; }
    }
    public class Media_Type
    {
        public Int16 MediaTypeId { get; set; }
        public String Name { get; set; }
    }
    public class Playlist_Track
    {
        public Int16 PlaylistId { get; set; }
        public Int16 TrackId { get; set; }
        public Track Tracks { get; set; }
        public Playlist Playlist { get; set; }
    }
    public class Playlist
    {
        public Int16 PlaylistId { get; set; }
        public String Name { get; set; }
        public ICollection<Playlist_Track> playlist_Tracks { get; set; }
    }
    public class Track
    {
        public Int16 TrackId { get; set; }
        public String Name { get; set; }
        public Int16 AlbumId { get; set; }
        public Int16 MediaTypeId { get; set; }
        public Int16 GenreId { get; set; }
        public String Composer { get; set; }
        public Int32 Milliseconds { get; set; }
        public Int32 Bytes { get; set; }
        public Double UnitPrice { get; set; }
        public Album Album { get; set; }
        public ICollection<Invoice_Item> invoice_Items { get; set; }
        public ICollection<Playlist_Track> playlist_Tracks { get; set; }
    }
}
