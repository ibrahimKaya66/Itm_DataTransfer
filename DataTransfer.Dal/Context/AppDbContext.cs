﻿using DataTransfer.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataTransfer.Dal.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Tek tek map dosyalarını config etmek için uygulanan : 
            //builder.ApplyConfiguration(new BreakConfig());...
            //Bütün config dosyalarını uygulamak için : 
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Factory> Factories { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupCode> GroupCodes { get; set; }
        public DbSet<Job> Jobs{ get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<OperationPerformance> OperationPerformances { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<Style_Operation> Style_Operations { get; set; }
    }
}
