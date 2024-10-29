using System;

namespace RentCars_Project.Models.Result;

public class ApiResponse<T>
{
    public int StatusCode { get; set; }
    public string RequestMethod { get; set; }
    public T Data{ get; set; }
}
