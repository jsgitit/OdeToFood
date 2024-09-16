using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;
using System.Collections;
using System.Collections.Generic;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines{ get; set; }

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper; // for Cuisine option values.
        }
        public IActionResult OnGet(int? restaurantId) // nullable due to Add New scenario
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
            if(restaurantId.HasValue) 
            {
                Restaurant = restaurantData.GetById(restaurantId.Value);
            } else
            {
                Restaurant = new Restaurant();
                // could set some defaults here if needed.
            }
                
            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // asp.net core is stateless, so rebuild of Cuisines is required when posting
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }

            if(Restaurant.Id > 0)
            {
                // assumes the id is the same that's on the form.
                restaurantData.Update(Restaurant);

            }
            else
            {
                restaurantData.Add(Restaurant);
            }
            
            restaurantData.Commit();
            TempData["Message"] = "Restrauant saved.";
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });

        }
    }
}
