using System;

namespace RentCars_Client.Models.Input;

public class CreateCustomerInput
{
    public string email { get; set;}
    public string password { get; set;}
    public string name { get; set;}
    public string phone_number { get; set;}
    public string address { get; set;}

}
