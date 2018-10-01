using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Play2Money.Models;

namespace Play2Money.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Query<OrderTableViewModel>();
        }

        public  IQueryable<OrderTableViewModel> GetOrderTableList()
        {
             
            var sql = "SELECT o.Id, o.Created, o.IsDone, o.PlayerId, o.Reference, p.Id, p.GlobalUid PlayerGlobalUid, p.Points"
                    + " FROM Orders AS o"
                    + " INNER JOIN Players AS p ON o.PlayerId = p.Id";

            var orders = this.Query<OrderTableViewModel>().FromSql(sql);
            return orders;
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
