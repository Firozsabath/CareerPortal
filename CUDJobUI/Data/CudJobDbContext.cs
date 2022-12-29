using CudJobUI.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CudJobUI.Data
{
    public class CUDJobDbContext : DbContext
    {
        public DbSet<StudentEducation> StudentEducation { get; set; }
        //public DbSet<StudentDegree> StudentDegree { get; set; }
        public DbSet<StudentExperienceModel> StudentExperience { get; set; }
        public DbSet<StudentModel> Student { get; set; }
        public DbSet<StudentProfile> StudentProfile { get; set; }
        public DbSet<CountryCode> Country { get; set; }

        public CUDJobDbContext(DbContextOptions<CUDJobDbContext> options)
            : base(options)
        {
        }
    }
}
