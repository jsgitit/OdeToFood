﻿using OdeToFood.Core;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDBContext db;

        public SqlRestaurantData(OdeToFoodDBContext db)
        {
            this.db = db;
        }
        public Restaurant Add(Restaurant newRestaurant)
        {
            db.Restaurants.Add(newRestaurant);
            return newRestaurant;

        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetById(id);
            if (restaurant != null)
            {
                db.Restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public Restaurant GetById(int id)
        {
            return db.Restaurants.Find(id);
        }

        public IEnumerable<Restaurant> GetRestaurantByName(string name)
        {
            var query = from r in db.Restaurants
                        where string.IsNullOrEmpty(name) || r.Name.ToLower().Contains(name.ToLower()) // Use ToLower for case-insensitive comparison
                        orderby r.Name
                        select r;
            return query;
        }

        public int GetRestaurantCount()
        {
            return db.Restaurants.Count();
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var entity = db.Restaurants.Attach(updatedRestaurant);  // tells ef to start tracking changes on this entity
                                                                                      // all fields in the table will be updated to match the entity
            entity.State = EntityState.Modified;
            return updatedRestaurant;

        }
    }
}
