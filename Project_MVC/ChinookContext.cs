using System;
using Microsoft.EntityFrameworkCore;
using ChinookEntities;

namespace ChinookContext
{
    public class ChinookDatabase : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Invoice_Item> invoice_Items { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Media_Type> Media_Types { get; set; }
        public DbSet<Playlist_Track> Playlist_Tracks { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Track> Tracks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=chinook.db");
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Playlist_Track>().ToTable("playlist_track");

            modelBuilder.Entity<Invoice_Item>()
                .HasKey(pk => pk.InvoiceLineId);

            modelBuilder.Entity<Media_Type>()
                .HasKey(pk => pk.MediaTypeId);

            modelBuilder.Entity<Playlist_Track>()
                .HasKey(pt => new { pt.PlaylistId, pt.TrackId });

            modelBuilder.Entity<Album>()
                .HasOne(al => al.Artist)
                .WithMany(ar => ar.Albums)
                .HasForeignKey(al => al.ArtistId);

            modelBuilder.Entity<Track>()
                .HasOne(tr => tr.Album)
                .WithMany(al => al.Tracks)
                .HasForeignKey(tr => tr.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Track>()
                .HasMany(ti => ti.invoice_Items)
                .WithOne(ii => ii.Track)
                .HasForeignKey(ii => ii.TrackId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Playlist_Track>()
                .HasOne(pt => pt.Tracks)
                .WithMany(t => t.playlist_Tracks)
                .HasForeignKey(pt => pt.TrackId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Playlist_Track>()
                .HasOne(pt => pt.Playlist)
                .WithMany(p => p.playlist_Tracks)
                .HasForeignKey(pt => pt.PlaylistId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}