using flow.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace flow.Context
{
    public class FlowDbContext : DbContext
    {
        public FlowDbContext()
            : base("name=MySQLDbContext")
        {
        }

        public virtual DbSet<MainFlow> MainFlow { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<FlowValidation> FlowValidation { get; set; }
    }
}