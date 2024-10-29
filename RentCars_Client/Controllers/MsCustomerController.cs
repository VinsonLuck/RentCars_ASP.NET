using System;
using Microsoft.AspNetCore.Mvc;
using RentCars_Client.Models.Input;
using RentCars_Client.Services;

namespace RentCars_Client.Controllers{
    public class MsCustomerController : Controller
    {
        private readonly ICustomer _customerApi;

        public MsCustomerController(ICustomer customerApi){
            _customerApi = customerApi;
        }

        public ActionResult Registrasi()
        {
            return View("~/Views/Authentication/Registrasi.cshtml");
        }
        
        public ActionResult Login(){
            return View("~/Views/Authentication/Login.cshtml");
        }

        public async Task<IActionResult> Logout(){
            var isLoggedOut = await _customerApi.Logout();
            var cookiescustomer = Request.Cookies["CustomerId"];
            if (isLoggedOut.Data)
            {   
                return RedirectToAction("Login", "MsCustomer");
    
            }
            return BadRequest(new { success = false, message = "Logout failed." });
        }

        public async Task<IActionResult> GetCustomer(){
            var result = await _customerApi.GetCustomer();
            return Json(result);
        }

        public async Task<IActionResult> FindCustomer([FromBody] string id)
        {
            var result = await _customerApi.FindCustomer(id);
            return Json(result);
        }

        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerInput request)
        {
            var result = await _customerApi.CreateCustomer(request);
            return Json(result);
        }


    }

}

