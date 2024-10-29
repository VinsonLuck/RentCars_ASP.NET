using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RentCars_Client.Models.Input;
using RentCars_Client.Services;

namespace RentCars_Client.Controllers
{
    public class MsCarController : Controller
    {
        private readonly ICar _carApi;
        private readonly ICustomer _customerApi;
        // GET: CarController

        public MsCarController(ICar carApi, ICustomer customerApi)
        {
            _carApi = carApi;
            _customerApi = customerApi;
        }
        public async Task<ActionResult> Home()
        {
            // var customerId = HttpContext.Session.GetString("CustomerId");
            // ViewBag.isLoggedIn = HttpContext.Session.GetString("IsLoggedIn") == "true";

            // if(ViewBag.isLoggedIn){
            //     var response= await _customerApi.FindCustomer(customerId);
            //     var customer = response.Data;
            //     ViewBag.username = customer?.name;
            // }
            if(Request.Cookies.TryGetValue("CustomerId",out var customerId)){
                ViewBag.isLoggedIn = true;
                var response= await _customerApi.FindCustomer(customerId);
                var customer = response.Data;

                ViewBag.name = customer.name;
            }else{
                ViewBag.isLoggedIn = false;
            }
           
            return View("~/Views/Home/Home.cshtml");
        }
        public async Task <IActionResult> GetCar()
        {
            var result = await _carApi.GetCar();
            return Json(result);
        }
        public async Task <IActionResult> FindCar([FromBody] string id)
        {
            var result = await _carApi.FindCar(id);
            return Json(result);
            
        }
        public async Task <IActionResult> CreateCar([FromBody] CreateCarInput request)
        {
            var result = await _carApi.CreateCar(request);
            return Json(result);
        }
        
        public async Task <IActionResult> UpdateCar(string id, [FromBody] UpdateCarInput request)
        {
            var result = await _carApi.UpdateCar(id,request);
            return Json(result);
        }
        public async Task <IActionResult> DeleteCar(string id)
        {
            var result = await _carApi.DeleteCar(id);
            return Json(result);
        }

    }
}
