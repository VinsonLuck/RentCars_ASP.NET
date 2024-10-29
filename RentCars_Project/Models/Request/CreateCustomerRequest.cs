using System;
using System.ComponentModel.DataAnnotations;

namespace RentCars_Project.Models.Request;

public class CreateCustomerRequest
{
    [Required]
    public string email { get; set; }
    [Required]
    public string password { get; set; }
    [Required]
    public string name { get; set; }
    [Required]
    public string phone_number { get; set; }
    [Required]
    public string address { get; set; }
    

}
