using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using GymSystem.DAL.Entities;
using GymSystemDAL.Data.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;


namespace GymSystemDAL.Data.Seeding
{
    public static class GymDbContextDataSeeding 
    {
        public static async Task<bool> SeedData(AppDbContext Dbcontext,IWebHostEnvironment environment)
        {
            try
            {
                var hasPlans = Dbcontext.Plans.Any();
                var hasCats = Dbcontext.Categories.Any();
                if (!hasPlans)
                {
                    var plans = LoadDataFromJsonFile<Plan>("plans.json", environment);
                    if(plans.Any()) await Dbcontext.AddRangeAsync(plans);
                    
                }
                if (!hasCats)
                {
                    var cats = LoadDataFromJsonFile<Category>("categories.json", environment);
                    if(cats.Any()) await Dbcontext.AddRangeAsync(cats);
                }

                return await Dbcontext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FAILD: Seeding data : {ex}");
                return false;
            }

        }


        private static List<T> LoadDataFromJsonFile<T>(string filename , IWebHostEnvironment environment)
        {
            var filePath = Path.Combine(environment.WebRootPath,@$"wwwroot\Files\{filename}");
            if(!File.Exists(filePath)) throw new FileNotFoundException();

            string data = File.ReadAllText(filePath) ?? throw new Exception($"Cannot read data from file named{filename}");


            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            var SerializedData = JsonSerializer.Deserialize<List<T>>(data,options) ?? throw new Exception("Cannot seralized the data");
            return SerializedData;
        }
    }
}