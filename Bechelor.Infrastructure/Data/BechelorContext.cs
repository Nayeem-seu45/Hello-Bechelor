using Bechelor.Core.Common;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Bechelor.Core.Domin.Core;
using Bechelor.Core.Domin.Expenses;
using Bechelor.Core.Domin.Bazar;

namespace Bechelor.Infrastructure.Data
{
    public partial class BechelorContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public BechelorContext(DbContextOptions<BechelorContext> options) : base(options)
        {

        }
        public virtual DbSet<MemberInformation> MemberInformations { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public virtual DbSet<MediaFile> MediaFiles { get; set; }
        public virtual DbSet<BazarDetail> BazarDetails { get; set; }
        public virtual DbSet<Tanent> Tanents { get; set; }
        public virtual DbSet<BazarItem> BazarItems { get; set; }
        //public virtual DbSet<UserWiseBill> UserWiseBills { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }

}
