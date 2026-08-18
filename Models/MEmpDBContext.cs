using Microsoft.AspNetCore.Identity.EntityFrameworkCore;//To use IdentityDbContext
using Microsoft.EntityFrameworkCore;//To use DbContext
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AC_EmpManagement.Models
{
    //public class MEmpDBContext : DbContext
    //{
    //    public MEmpDBContext(DbContextOptions<MEmpDBContext> DBConOptions)
    //        :base(DBConOptions)
    //    {

    //    }

    //    public DbSet<MEmployee> DbS_Emps { get; set; }

    //    //// Seeding the table with initial values
    //    //protected override void OnModelCreating(ModelBuilder MBuilder)
    //    //{
    //    //    MBuilder.Entity<MEmployee>().HasData(
    //    //                                new MEmployee
    //    //                                {
    //    //                                    Id = 1,
    //    //                                    Name = "Pishooo",
    //    //                                    Email = "pishooo@parthy.com",
    //    //                                    Department = EnumDept.IT
    //    //                                },
    //    //                                new MEmployee
    //    //                                {
    //    //                                    Id = 2,
    //    //                                    Name = "Qalby",
    //    //                                    Email = "qalby@parthy.com",
    //    //                                    Department = EnumDept.PayRoll
    //    //                                });
    //    //}

    //    // Using Extension method
    //    protected override void OnModelCreating(ModelBuilder MBuilder)
    //    {
    //        MBuilder.Seed();
    //    }
    //}

    //// 1st Step inherits from IdentityDbContext
    //public class MEmpDBContext : IdentityDbContext
    //{
    //    public MEmpDBContext(DbContextOptions<MEmpDBContext> DBConOptions)
    //        : base(DBConOptions)
    //    {

    //    }

    //    public DbSet<MEmployee> DbS_Emps { get; set; }

    //    // Using Extension method
    //    protected override void OnModelCreating(ModelBuilder MBuilder)
    //    {
    //        base.OnModelCreating(MBuilder);
    //        MBuilder.Seed();
    //    }
    //}

    // To migrate the new column City we must use IdUserExtension
    // as generic parameter
    public class MEmpDBContext : IdentityDbContext<IdUserExtension>
    {
        public MEmpDBContext(DbContextOptions<MEmpDBContext> DBConOptions)
            : base(DBConOptions)
        {

        }

        public DbSet<MEmployee> DbS_Emps { get; set; }

        //// Using Extension method
        //protected override void OnModelCreating(ModelBuilder MBuilder)
        //{
        //    base.OnModelCreating(MBuilder);
        //    MBuilder.Seed();
        //}

        // Removing "On Delete" Cascade
        protected override void OnModelCreating(ModelBuilder MBuilder)
        {
            base.OnModelCreating(MBuilder);
            MBuilder.Seed();
            foreach(var FKey in MBuilder.Model.GetEntityTypes()
                                        .SelectMany(e => e.GetForeignKeys()))
            {
                FKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
