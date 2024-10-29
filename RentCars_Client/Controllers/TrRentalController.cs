using System;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using RentCars_Client.Services;

namespace RentCars_Client.Controllers{
    public class TrRentalController : Controller
    {
        private readonly ICustomer _customerApi;
        private readonly IRental _rentalApi;
        public TrRentalController (ICustomer customerApi,IRental rentalApi)
        {
            _rentalApi = rentalApi;
            _customerApi = customerApi;
        }
        public async Task<ActionResult> RentalHistory(){
            if(Request.Cookies.TryGetValue("CustomerId",out var customerId)){
                ViewBag.isLoggedIn = true;
                var response= await _customerApi.FindCustomer(customerId);
                var customer = response.Data;
                ViewBag.customerId = customerId;
                ViewBag.name = customer.name;
            }else{
                ViewBag.isLoggedIn = false;
                return View("~/Views/Authentication/Login.cshtml");
            }
             return View("~/Views/Rental/RentalHistory.cshtml");
        }
         public async Task<IActionResult> FindRentalByCustomerId(string id)
        {
            var result = await _rentalApi.FindRentalByCustomerId(id);
            return Json(result);
        }
    }

}

