using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCars_Project.Models;

public class TrMaintenance
{
    [Key]
    public string Maintenance_id { get; set; }
    public DateTime maintenance_date { get; set; }
    public string description { get; set; }
    public decimal cost { get; set; }
    [ForeignKey("car_id")]
    public string car_id { get; set; }
    [ForeignKey("employee_id")]
    public string employee_id { get; set; }

    public MsCar msCar { get; set; }
    public MsEmployee msEmployee { get; set; }


}
