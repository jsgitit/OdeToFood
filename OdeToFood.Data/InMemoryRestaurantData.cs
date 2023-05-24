using OdeToFood.Core;
using System.Linq;
using System.Collections.Generic;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        private readonly List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant { Id = 1, Name = "Scott's Pizza", Location = "Maryland", Cuisine = CuisineType.Italian},
                new Restaurant { Id = 2, Name = "Cinnamon Club", Location = "London", Cuisine = CuisineType.None },
                new Restaurant { Id = 3, Name = "La Costa", Location = "California", Cuisine = CuisineType.Mexican},
                new Restaurant { Id = 4, Name = "Acapulco", Location = "Oregon", Cuisine = CuisineType.Mexican},
                new Restaurant { Id = 5, Name = "El Sol", Location = "Oregon", Cuisine = CuisineType.Mexican},
                new Restaurant { Id = 6, Name = "Abby's Pizza", Location = "Oregon", Cuisine = CuisineType.Italian},
                new Restaurant { Id = 7, Name = "Namaste", Location = "Oregon", Cuisine = CuisineType.Indian},
                new Restaurant { Id = 8, Name = "McDonald's", Location = "Oregon", Cuisine = CuisineType.None},
                new Restaurant { Id = 9, Name = "Outback Steakhouse", Location = "Oregon", Cuisine = CuisineType.None},
                new Restaurant { Id = 10, Name = "AppleBee's", Location = "Oregon", Cuisine = CuisineType.None},
                new Restaurant { Id = 11, Name = "Corona Free", Location = "Oregon", Cuisine = CuisineType.None }
            };
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }

        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public Restaurant Update(Restaurant updatedRestraunt)
        {
            var restraunt = restaurants.SingleOrDefault(r => r.Id == updatedRestraunt.Id);
            if (restraunt != null)
            {
                restraunt.Name = updatedRestraunt.Name;
                restraunt.Location= updatedRestraunt.Location;
                restraunt.Cuisine= updatedRestraunt.Cuisine;

            }
            return restraunt;
        }

        public int Commit()
        {
            return 0;
        }
        public IEnumerable<Restaurant> GetRestaurantByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.Contains(name, System.StringComparison.InvariantCultureIgnoreCase)
                   orderby r.Name
                   select r;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant != null)
            {
                restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public int GetRestaurantCount()
        {
            return restaurants.Count();
        }
    }
}
