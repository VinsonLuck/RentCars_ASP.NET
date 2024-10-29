using System;
using Microsoft.EntityFrameworkCore;
using RentCars_Project.Models;

namespace RentCars_Project.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
     
    }
    public DbSet<MsCar> MsCar { get; set; }
    public DbSet<MsCarImages> MsCarImage { get; set; }
    public DbSet<MsCustomer> MsCustomer{ get; set; }
    public DbSet<MsEmployee> MsEmployee { get; set; }
    public DbSet<TrMaintenance> TrMaintenance { get; set; }
    public DbSet<TrPayment> TrPayment { get; set; }
    public DbSet<TrRental> TrRental { get; set; }
}
