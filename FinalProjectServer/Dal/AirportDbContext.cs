using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Dal
{
    public class AirportDbContext : DbContext
    {
        public AirportDbContext(DbContextOptions<AirportDbContext> options) : base(options)
        {
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<PlaneStationHistory> PlaneStationHistory { get; set; }
        public DbSet<ControlTower> ControlTower { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StationRelation>(srBuilder =>
            {
                srBuilder.HasKey(sr => new { sr.Direction, sr.StationFromId, sr.StationToId });
                srBuilder.HasOne(sr => sr.StationFrom).WithMany(s => s.ChildrenStations).HasForeignKey(sr => sr.StationFromId);
                srBuilder.HasOne(sr => sr.StationTo).WithMany(s => s.ParentStations).HasForeignKey(sr => sr.StationToId);
            });

            modelBuilder.Entity<ControlTowerStationRelation>(srBuilder =>
            {
                srBuilder.HasKey(sr => new { sr.Direction, sr.ControlTowerId, sr.StationToId });
                srBuilder.HasOne(sr => sr.ControlTower).WithMany(s => s.StartupStations).HasForeignKey(sr => sr.ControlTowerId);
                srBuilder.HasOne(sr => sr.StationTo).WithOne(s => s.ControlTowerStationRelation).HasForeignKey<ControlTowerStationRelation>(sr => sr.StationToId);
            });
            modelBuilder.Entity<Station>(st=> st.HasOne(st => st.CurrentFlight).WithOne(f => f.Station).HasForeignKey<Station>(st=> st.CurrentFlightId));

            modelBuilder.Entity<Station>().HasData(DummyData.Stations);
            modelBuilder.Entity<ControlTower>().HasData(DummyData.ControlTowers);
            modelBuilder.Entity<StationRelation>().HasData(DummyData.StationRelations);
            modelBuilder.Entity<ControlTowerStationRelation>().HasData(DummyData.ControlTowerStationRelations);
        }
    }
}
