using System;

namespace RentCars_Client.Models.Output;

public class ApiResponse<T>
{
    public string StatusCode { get; set; }
    public string RequestMethod{ get; set; }
    public T Data { get; set; }

}
