using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCars_Project.Data;
using RentCars_Project.Models;
using RentCars_Project.Models.Request;
using RentCars_Project.Models.Result;

namespace RentCars_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MsCarController : ControllerBase
    {
        private readonly AppDbContext _context;
        public MsCarController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCarResult>>> Get()
        {
            var listCar = await _context.MsCar
            .Select(x => new GetCarResult{
                Car_id = x.Car_id,
                name = x.name,
                model = x.model,
                year = x.year,
                license_plate = x.license_plate,
                number_of_car_seats = x.number_of_car_seats,
                transmission = x.transmission,
                price_per_day = x.price_per_day,
                status = x.status,
            }).ToListAsync();

            var response = new ApiResponse<IEnumerable<GetCarResult>>{
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = listCar
            };

            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCarRequest request)
        {

            var topCarId = await _context.MsCar.OrderByDescending(x=> x.Car_id)
            .Select(x=> x.Car_id).FirstOrDefaultAsync();

            var substringCarId = topCarId?.Substring(3);
            var currentCarId = int.Parse(substringCarId);

            try{
                // var isCarExist = await _context.MsCar.Where(x => x.Car_id == request.Car_id).AnyAsync();
                // if(isCarExist){
                //     throw new ArgumentException("Car is already exist");
                // }

                currentCarId += 1;
                var newCarId = currentCarId.ToString("D3");

                var carData = new MsCar{
                    Car_id = $"CAR{newCarId}",
                    name = request.name,
                    model = request.model,
                    year = request.year,
                    license_plate=request.license_plate,
                    number_of_car_seats = request.number_of_car_seats,
                    transmission = request.transmission,
                    price_per_day = request.price_per_day,
                    status = request.status,
                };
                
                _context.MsCar.Add(carData);
                await _context.SaveChangesAsync();

                var response = new ApiResponse<string>{
                    StatusCode = StatusCodes.Status201Created,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "Car data saved succesfully"
                };
                return Ok(response);

            }catch(Exception ex)
            {
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "No inner exception available";
                var response = new ApiResponse<string>{
                    StatusCode = StatusCodes.Status400BadRequest,
                    RequestMethod = HttpContext.Request.Method,
                    Data = $"{ex.Message} - {innerExceptionMessage}"
                };
                return BadRequest(response);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCarResult>> Get(string id){
            var carData = await _context.MsCar.Where(x => x.Car_id == id)
            .Select(x => new GetCarResult{
                Car_id = x.Car_id,
                name = x.name,
                model = x.model,
                year = x.year,
                license_plate = x.license_plate,
                number_of_car_seats = x.number_of_car_seats,
                transmission  = x.transmission,
                price_per_day = x.price_per_day,
                status = x.status,
            }).FirstOrDefaultAsync();
             
            var response = new ApiResponse<GetCarResult>{
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = carData
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id,[FromBody] UpdateCarRequest request)
        {
            try{
                var carData = await _context.MsCar.Where(x => x.Car_id== id ).FirstOrDefaultAsync();

                if(carData == null){
                    throw new KeyNotFoundException("Car data not found");
                }
                
                carData.name = request.name;
                carData.model = request.model;
                carData.year = request.year;
                carData.license_plate = request.license_plate;
                carData.number_of_car_seats = request.number_of_car_seats;
                carData.transmission = request.transmission;
                carData.price_per_day = request.price_per_day;
                carData.status = request.status;

                _context.MsCar.Update(carData);
                await _context.SaveChangesAsync();

                var response = new ApiResponse<string>{
                    StatusCode = StatusCodes.Status200OK,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "Car is updated"
                };
                return Ok(response);
            }
            catch(Exception ex){
                var response = new ApiResponse<string>{
                    StatusCode = StatusCodes.Status400BadRequest,
                    RequestMethod = HttpContext.Request.Method,
                    Data =ex.Message
                };
                return BadRequest(response);
            }
            
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id){
            try{
                var carData = await _context.MsCar.Where(x=> x.Car_id == id).FirstOrDefaultAsync();

                if(carData == null){
                    throw new KeyNotFoundException("Car Data not found");
                }

                _context.MsCar.Remove(carData);
                await _context.SaveChangesAsync();

                var response = new ApiResponse<string>{
                    StatusCode = StatusCodes.Status200OK,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "Car is deleted"
                };
                return Ok(response);

            }catch(Exception ex){
                 var response = new ApiResponse<string>{
                    StatusCode = StatusCodes.Status400BadRequest,
                    RequestMethod = HttpContext.Request.Method,
                    Data =ex.Message
                };
                return BadRequest(response);
            }
        }
    }
}
