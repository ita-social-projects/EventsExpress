using System.Collections.Generic;
using System.IO;
using EventsExpress.Db.Entities;
using Newtonsoft.Json;

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
                    foreach (var city in item.Value.ToString().Split("|"))
                    {
                        if (city != string.Empty)
                        {
                            cities.Add(new City() { Name = city });
                        }
                    }

                    countries.Add(new Country() { Name = item.Name, Cities = cities });
                }
            }

            return countries;
        }
    }
}
