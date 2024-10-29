using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCars_Project.Models;

public class TrPayment
{
    [Key]
    public string Payment_id { get; set; }
    public DateTime payment_date { get; set; }
    public decimal amount { get; set; }
    public string payment_method { get; set; }
    [ForeignKey("rental_id")]
    public string rental_id { get; set; }
    public TrRental trRental{ get; set; }
}
