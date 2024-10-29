using System;

namespace RentCars_Client.Models.Output;

public class GetRentalOutput
{
    public string rental_id { get; set; }
    public DateTime rental_date { get; set; }
    public DateTime return_date { get; set; }
    public decimal total_price { get; set; }
    public bool payment_status { get; set; }
    public string customer_id { get; set; }
    public string car_id { get; set; }
}
