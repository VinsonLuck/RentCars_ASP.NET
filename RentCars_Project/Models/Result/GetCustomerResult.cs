using System;

namespace RentCars_Project.Models.Result;

public class GetCustomerResult
{
    public string CustomerId { get; set;}
    public string email { get; set;}
    public string password { get; set;}
    public string name { get; set;}
    public string phone_number { get; set;}
    public string address { get; set;}
    public string driver_license_number { get; set;}

    public static implicit operator GetCustomerResult(bool v)
    {
        throw new NotImplementedException();
    }
}
