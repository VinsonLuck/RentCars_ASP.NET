using System;
using System.ComponentModel.DataAnnotations;

namespace RentCars_Project.Models.Request;

public class LoginRequest
{
    [Required]
    public string name { get; set; }
    public string password { get; set; }
}
