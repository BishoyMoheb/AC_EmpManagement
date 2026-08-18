using Microsoft.EntityFrameworkCore;//To use ModelBuilder
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AC_EmpManagement.Models
{
    public static class MBuilderExtensions
    {
        public static void Seed(this ModelBuilder MBuilder)
        {
            MBuilder.Entity<MEmployee>().HasData(
                                        new MEmployee
                                        {
                                            Id = 1,
                                            Name = "Pishooo",
                                            Email = "pishooo@parthy.com",
                                            Department = EnumDept.IT
                                        },
                                        new MEmployee
                                        {
                                            Id = 2,
                                            Name = "Qalby",
                                            Email = "qalby@parthy.com",
                                            Department = EnumDept.PayRoll
                                        });
        }
    }
}
