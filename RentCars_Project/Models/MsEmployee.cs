using System;
using System.ComponentModel.DataAnnotations;

namespace RentCars_Project.Models;

public class MsEmployee
{
    [Key]
    public string Employee_id { get; set; }
    public string name { get; set; }
    public string position { get; set; }
    public string email { get; set; }
    public string phone_number { get; set; }

}
