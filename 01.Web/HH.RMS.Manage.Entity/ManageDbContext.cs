using HH.RMS.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.RMS.Manage.Repository
{
    public class ManageDbContext:ApplicationDbContext
    {
        public ManageDbContext()
            : base()
        {
            
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
 	         base.OnModelCreating(modelBuilder);
        }

    }
}
