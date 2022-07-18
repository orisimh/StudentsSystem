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

        public DbSet<tblStudent> Students { get;  set; }
        public DbSet<tblDeltaStudent> DeltaStudents { get; set; }

        //public DbSet<tblStudent> gettblOUSR { get; set; }

    }
}
