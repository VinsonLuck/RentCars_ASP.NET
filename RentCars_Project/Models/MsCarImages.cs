using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCars_Project.Models;

public class MsCarImages
{
    [Key]
    public string image_car_id { get; set; }
    
    [ForeignKey("Car_id")]
    public string Car_id { get; set; }
    public string image_link { get; set; }

    public MsCar MsCar { get; set; }

}
