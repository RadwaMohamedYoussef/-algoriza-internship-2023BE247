using API.Models;
using Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Specialization>()
                .HasData(new Specialization { Id =1 ,Name= "Cardiology" });
             modelBuilder.Entity<Specialization>()
                .HasData(new Specialization { Id =2 ,Name= "Dermatology" });
             modelBuilder.Entity<Specialization>()
                .HasData(new Specialization { Id =3 ,Name= "Orthopedics" });
             modelBuilder.Entity<Specialization>()
                .HasData(new Specialization { Id =4 ,Name= "Neurology" });

        }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentTime> AppointmentTimes { get; set; }
        public DbSet<DoctorDetails> DoctorDetails { get; set; }
        public DbSet<PatientDetails> PatientDetails { get; set; }
        public DbSet<Discount> Discounts { get; set; }


    }
}
