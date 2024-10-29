using System;
using Newtonsoft.Json;
using RentCars_Client.Models.Output;
using RentCars_Client.Services;

namespace RentCars_Client.Handler;

public class RentalHandler : IRental
{
    private readonly IConfiguration _configuration;
    private readonly string baseUrl = "";
    private HttpClient httpclient = new HttpClient();
    public RentalHandler(IConfiguration configuration)
    {
        _configuration = configuration;
        baseUrl = _configuration["apiEndpoint"];
    }

    public async Task<ApiResponse<IEnumerable<GetRentalOutput>>> FindRentalByCustomerId(string id)
    {
        string endpoint = baseUrl + "TrRental/" + id;

        var rentalOutput = new ApiResponse <IEnumerable<GetRentalOutput>>();

        var response = await httpclient.GetAsync(endpoint);

        string apiResponse = await response.Content.ReadAsStringAsync();

        if(!string.IsNullOrEmpty(apiResponse)){
            rentalOutput = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<GetRentalOutput>>>(apiResponse);
        }
        return rentalOutput;
    }


}
