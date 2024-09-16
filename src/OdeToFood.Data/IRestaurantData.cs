using OdeToFood.Core;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace OdeToFood.Data
{
    // Getting ready for SQL Server installation
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantByName(string name);
        Restaurant GetById(int id);
        Restaurant Add(Restaurant newRestraunt);
        Restaurant Update(Restaurant updatedRestaurant);
        Restaurant Delete(int id);
        int GetRestaurantCount();
        int Commit(); // SaveChanges();

    }
}
