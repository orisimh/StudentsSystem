using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class DemoDB :DbContext
    {
        public DemoDB(DbContextOptions<DemoDB> options) : base(options)
        {

        }

        public DbSet<Students> Students { get;  set; }
        // public DbSet<tblDeltaStudent> DeltaStudents { get; set; }
        public DbSet<UserCred> UserCred { get; set; }

       public DbSet<Question> Question { get; set; }
       public DbSet<Answer> Answer { get; set; }


        // public DbSet<RolesByUsers> Roles { get; set; }
        //public DbSet<tblStudent> gettblOUSR { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            base.OnModelCreating(modelBuilder);

        }
    }
}
