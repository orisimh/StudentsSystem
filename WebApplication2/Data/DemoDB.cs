using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Data
{
    public class DemoDB :DbContext
    {
        public DemoDB(DbContextOptions<DemoDB> options) : base(options)
        {

        }

        public DbSet<Students> Students { get;  set; }
        public DbSet<tblDeltaStudent> DeltaStudents { get; set; }

        //public DbSet<tblStudent> gettblOUSR { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<tblStudent>()
            //.Property(d => d.Id)
            //.ValueGeneratedOnAdd();

            //modelBuilder.Entity<tblStudent>()
            //.HasKey(d => d.Id);


            modelBuilder.Entity<Students>(entity =>
            {
                //entity.Property(t => t.Id).HasColumnName("Id").(DatabaseGeneratedOption.Identity);

                //entity.ToTable("Students");
                entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedOnAdd(); // <-- This fixed it for me
            });

        }
    }
}
