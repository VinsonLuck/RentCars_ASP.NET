using System;
using System.ComponentModel.DataAnnotations;

namespace RentCars_Project.Models;

public class MsCustomer
{
    [Key]
    public string Customer_id { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public string name { get; set; }
    public string phone_number { get; set; }
    public string address { get; set; }
    public string driver_license_number { get; set; }
    
}
