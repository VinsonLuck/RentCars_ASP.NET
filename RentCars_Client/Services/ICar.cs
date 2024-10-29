using System;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using RentCars_Client.Models.Input;
using RentCars_Client.Models.Output;

namespace RentCars_Client.Services;

public interface ICar
{
    Task<ApiResponse<IEnumerable<GetCarOutput>>> GetCar();
    Task<ApiResponse<GetCarOutput>> FindCar(string id);
    Task<ApiResponse<string>> CreateCar(CreateCarInput request);
    Task<ApiResponse<string>> UpdateCar(string id, UpdateCarInput request);
    Task<ApiResponse<string>> DeleteCar(string id);
}
