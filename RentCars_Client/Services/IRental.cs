using System;
using RentCars_Client.Models.Output;

namespace RentCars_Client.Services;

public interface IRental
{
    Task<ApiResponse<IEnumerable<GetRentalOutput>>> FindRentalByCustomerId(string id);
}
