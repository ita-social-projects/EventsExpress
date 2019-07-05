using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EventsExpress.Db.DbInitialize
{
    public static class LocationParser
    {                           

        public static List<Country> GetCountries()
        {
            List<Country> countries = new List<Country>();

            using (StreamReader r = new StreamReader($"ClientApp/Json/Locations.json"))
            {
                string json = r.ReadToEnd();                  
                
                foreach (var item in JsonConvert.DeserializeObject<dynamic>(json))
                {
                    List<City> cities = new List<City>();       
                    foreach(var city in item.Value.ToString().Split("|"))
                    {
                        if(city != "")
                            cities.Add(new City() { Name = city });
                    }
                    countries.Add(new Country() { Name = item.Name, Cities = cities });
                };
            }                            
            return countries;
        }


    }
}
