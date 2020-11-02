using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly IRestaurantData restaurantData;

        public string Message { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }

        [BindProperty(SupportsGet = true)] // allows searchTerm to be preserved on the form.
        public string SearchTerm { get; set; }
        public ListModel(IConfiguration config, IRestaurantData restaurantData)

        {
            this.config = config;
            this.restaurantData = restaurantData;
        }
        public void OnGet(string searchTerm) // "searchTerm" matches name in Request from search form.
                                                // then it model binds to the Request.searchTerm value
                                                // but page refreshes will pass in a null, which is ok for string
        {
            

            Message = config["Message"];
            Restaurants = restaurantData.GetRestaurantByName(SearchTerm);
        }
    }
}
