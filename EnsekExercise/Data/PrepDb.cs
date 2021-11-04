using CsvHelper;
using CsvHelper.Configuration;
using EnsekExercise.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EnsekExercise.Data
{
    public class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {           
            if (!context.Accounts.Any())
            {
                Console.WriteLine("Seeding data");

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HeaderValidated = null,
                    MissingFieldFound = null
                };

                using (var reader = new StreamReader("SeedData/Test_Accounts.csv"))
                using (var csvReader = new CsvReader(reader, config))
                {
                    var accounts = csvReader.GetRecords<Account>().ToArray();

                    context.Accounts.AddRange(accounts);
                }

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("We already have data");
            }
        }
    }


}
