using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RentCars_Project.Data;
using RentCars_Project.Models;
using RentCars_Project.Models.Request;
using RentCars_Project.Models.Result;

namespace RentCars_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MsCustomerController : ControllerBase
    {
        private readonly AppDbContext _context;
        public MsCustomerController (AppDbContext context)
        {
            _context = context;   
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCustomerResult>>> Get()
        {
            var listCustomer = await _context.MsCustomer
            .Select(x => new GetCustomerResult{ 
                CustomerId = x.Customer_id,
                email = x.email,
                name = x.name,
                phone_number = x.phone_number,
                address = x.address,
                driver_license_number = x.driver_license_number,                    
            }).ToListAsync();

            var response = new ApiResponse<IEnumerable<GetCustomerResult>>(){
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = listCustomer
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCustomerRequest request)
        {
            var topCustomerId = await _context.MsCustomer.OrderByDescending(x=> x.Customer_id)
            .Select(x=>x.Customer_id).FirstOrDefaultAsync();

            var substringCustomerId = topCustomerId?.Substring(3);
            var currentCustomerId = int.Parse(substringCustomerId);

            var driverLicenses = await _context.MsCustomer.Select(x => x.driver_license_number).ToListAsync();
            var topDriverId = driverLicenses.OrderByDescending(x=> int.Parse(x.Substring(3))).FirstOrDefault();
            var substringDriverId = topDriverId?.Substring(3);
            var currentDriverId = substringDriverId != null ? int.Parse(substringDriverId) : 0; 
            
            try{
                currentCustomerId += 1;
                var newCustomerId = currentCustomerId.ToString("D3");

                currentDriverId += 1;
                var newDriverId = currentDriverId.ToString();

                var customerData = new MsCustomer{
                    Customer_id = $"CUS{newCustomerId}",
                    email =  request.email,
                    name= request.name,
                    password = request.password,
                    phone_number = request.phone_number,
                    address = request.address,
                    driver_license_number = $"DLN{newDriverId}",
                };
                _context.MsCustomer.Add(customerData);
                await _context.SaveChangesAsync();

                var response = new ApiResponse<string>{
                    StatusCode = StatusCodes.Status201Created,
                    RequestMethod = HttpContext.Request.Method,
                    Data = $"Customer data saved succesfully "
                };
                return Ok(response);
            
            }catch(Exception ex)
            {
                var response = new ApiResponse<string>{
                    StatusCode = StatusCodes.Status400BadRequest,
                    RequestMethod = HttpContext.Request.Method,
                    Data = ex.Message
                };
                return BadRequest(response);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCustomerResult>> Get(string id){
            var customerData = await _context.MsCustomer.Where(x=> x.Customer_id == id)
            .Select(x => new GetCustomerResult{
                CustomerId = x.Customer_id,
                email = x.email,
                name = x.name,
                password = x.password,
                phone_number = x.phone_number,
                address = x.address,
                driver_license_number = x.driver_license_number,
            }).FirstOrDefaultAsync();

            var response = new ApiResponse<GetCustomerResult>{
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = customerData
            };
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var customer = await _context.MsCustomer.FirstOrDefaultAsync(x => x.name == request.name && x.password == request.password);
            
            if(customer != null){
                
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTimeOffset.Now.AddMinutes(30)
                };
                
                Response.Cookies.Append("CustomerId",customer.Customer_id,cookieOptions);
                return Ok(new {success=true,message = "Login Succesful"});
            }
            return BadRequest(new{success = false,message= "invalid username or password"});
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(-1) 
             };
            var cookies = Request.Cookies;
            Response.Cookies.Append("CustomerId", "", cookieOptions); 
            Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            HttpContext.Session.Clear();
            var response = new ApiResponse<bool>{
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data =  true
            };
            return Ok(response);
        }


    }
}

