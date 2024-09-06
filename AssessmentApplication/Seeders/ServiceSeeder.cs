using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssessmentApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace AssessmentApplication.Seeders
{
    public class ServiceSeeder
    {
        public static void SeedServices(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceModel>().HasData(new List<ServiceModel>
            {
                new ServiceModel { Id = 1, Name = "Service A" },
                new ServiceModel { Id = 2, Name = "Service B" },
                new ServiceModel { Id = 3, Name = "Service C" }
            });
        }
    }
}