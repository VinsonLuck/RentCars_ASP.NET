using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCars_Project.Models;

public class TrRental
{
    [Key]
    public string Rental_id { get; set; }
    public DateTime rental_date { get; set; }
    public DateTime return_date { get; set; }
    public decimal total_price { get; set; }
    public bool payment_status { get; set; }
    [ForeignKey("customer_id")]
    public string Customer_id { get; set; }
    [ForeignKey("car_id")]
    public string car_id { get; set; }  

}
