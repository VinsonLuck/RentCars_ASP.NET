using System;
using Newtonsoft.Json;
using RentCars_Client.Models.Input;
using RentCars_Client.Models.Output;
using RentCars_Client.Services;

namespace RentCars_Client.Handler;

public class CarHandler : ICar
{
    private readonly IConfiguration _configuration;
    private readonly string baseUrl = "";
    private HttpClient httpClient = new HttpClient();

    public CarHandler(IConfiguration configuration){
        _configuration = configuration;
        baseUrl = _configuration["apiEndpoint"];
    }

    public async Task<ApiResponse<string>> CreateCar(CreateCarInput request)
    {
        if(request==null){
            return new ApiResponse<string>
            {
                StatusCode = "400",
                RequestMethod = "Post",
                Data = "Bad Request"
            };
        }

        string endpoint = baseUrl + "MsCar";
        var response = await httpClient.PostAsJsonAsync(endpoint,request);
        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();

        return apiResponse;
    }

    public async Task<ApiResponse<string>> DeleteCar(string id)
    {
        if(id==null){
            return new ApiResponse<string>
            {
                StatusCode = "400",
                RequestMethod = "Post",
                Data = "Bad Request"
            };
        }

        string endpoint = baseUrl + "MsCar/" + id;
        var response = await httpClient.DeleteAsync(endpoint);
        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();

        return apiResponse;   
    }

    public async Task<ApiResponse<GetCarOutput>> FindCar(string id)
    {
        string endpoint = baseUrl + "MsCar/"+ id;

        var carOutput = new ApiResponse<GetCarOutput>();

        var response = await httpClient.GetAsync(endpoint);
        string apiResponse = await response.Content.ReadAsStringAsync();

        if(!string.IsNullOrEmpty(apiResponse)){
            carOutput = JsonConvert.DeserializeObject<ApiResponse<GetCarOutput>>(apiResponse);
        }
        return carOutput;
    }

    public async Task<ApiResponse<IEnumerable<GetCarOutput>>> GetCar(){
        string endpoint = baseUrl + "MsCar";

        var carOutput = new ApiResponse<IEnumerable<GetCarOutput>>();

        var response = await httpClient.GetAsync(endpoint);
        string apiResponse = await response.Content.ReadAsStringAsync();

        if(!string.IsNullOrEmpty(apiResponse)){
            carOutput = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<GetCarOutput>>>(apiResponse);
        }
        return carOutput;
    }

    public async Task<ApiResponse<string>> UpdateCar(string id, UpdateCarInput request)  
    {
       if(request==null || id==null){
            return new ApiResponse<string>
            {
                StatusCode = "400",
                RequestMethod = "Post",
                Data = "Bad Request"
            };
        }

        string endpoint = baseUrl + "MsCar/" + id;
        var response = await httpClient.PutAsJsonAsync(endpoint,request);
        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();

        return apiResponse;
    }
}
