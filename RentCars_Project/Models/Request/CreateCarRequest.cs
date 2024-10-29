using System;
using System.ComponentModel.DataAnnotations;

namespace RentCars_Project.Models.Request;

public class CreateCarRequest
{
    // [Required]
    // public string Car_id { get; set; }
    [Required]
    public string name { get; set; }
    [Required]
    public string model { get; set; }
    [Required]
    public int year { get; set; }
    [Required]
    public string license_plate { get; set; }
    [Required]
    public int number_of_car_seats { get; set; }
    [Required]
    public string transmission { get; set; }
    [Required]
    public decimal price_per_day { get; set; }
    [Required]
    public bool status { get; set; }
}
