using System;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCars_Project.Data;
using RentCars_Project.Models.Result;

namespace RentCars_Project.Controllers{
    
    [Route("api/[controller]")]
    public class TrRentalController: ControllerBase
    {
        private readonly AppDbContext _context;
        public TrRentalController(AppDbContext context ){
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<GetRentalResult>>> Get(string id){
            var rentalData = await _context.TrRental.Where(x=> x.Customer_id == id)
            .Select(x=> new GetRentalResult{
                rental_id = x.Rental_id,
                rental_date = x.rental_date,
                return_date = x.return_date,
                total_price = x.total_price,
                payment_status = x.payment_status,
                customer_id = x.Customer_id,
                car_id = x.car_id,
            }).ToListAsync();

            var response = new ApiResponse<IEnumerable<GetRentalResult>>{
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = rentalData
            };
            return Ok(response);
        }

        



    }

}

