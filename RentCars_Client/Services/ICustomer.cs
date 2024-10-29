using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using RentCars_Client.Models.Input;
using RentCars_Client.Models.Output;

namespace RentCars_Client.Services;

public interface ICustomer
{
    Task<ApiResponse<IEnumerable<GetCustomerOutput>>> GetCustomer();
    Task<ApiResponse<string>> CreateCustomer(CreateCustomerInput request);
    Task<ApiResponse<GetCustomerOutput>> FindCustomer(string id);
    Task<ApiResponse<bool>> Logout();
}
