using System;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentCars_Client.Models.Input;
using RentCars_Client.Models.Output;
using RentCars_Client.Services;

namespace RentCars_Client.Handler;

public class CustomerHandler : ICustomer
{
    private readonly IConfiguration _configuration;
    private readonly string baseUrl = "";
    private HttpClient httpClient = new HttpClient();

    public CustomerHandler (IConfiguration configuration){
        _configuration = configuration;
        baseUrl = _configuration["apiEndpoint"];
    }

    public async Task<ApiResponse<string>> CreateCustomer(CreateCustomerInput request)
    {
        if(request==null){
            return new ApiResponse<string>
            {
                StatusCode = "400",
                RequestMethod = "Post",
                Data = "Bad Request"
            };
        }

        string endpoint = baseUrl + "MsCustomer";
        var response = await httpClient.PostAsJsonAsync(endpoint,request);
        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
        return apiResponse;
        
    }
    
    public async Task<ApiResponse<GetCustomerOutput>> FindCustomer (string id)
    {
        string endpoint = baseUrl + "MsCustomer/"  + id;
        var customerOutput = new ApiResponse<GetCustomerOutput>();
        var response = await httpClient.GetAsync(endpoint);
        string apiResponse = await response.Content.ReadAsStringAsync();   

        if(!string.IsNullOrEmpty (apiResponse))
        {
            customerOutput = JsonConvert.DeserializeObject<ApiResponse<GetCustomerOutput>>(apiResponse);
        }
        return customerOutput;
    }

    public async Task<ApiResponse<IEnumerable<GetCustomerOutput>>> GetCustomer(){
        string endpoint = baseUrl + "MsCustomer";

        var customerOutput = new ApiResponse<IEnumerable<GetCustomerOutput>>(); 
        var response = await httpClient.GetAsync(endpoint);
        string apiResponse = await response.Content.ReadAsStringAsync();

        if(!string.IsNullOrEmpty(apiResponse)){
            customerOutput = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<GetCustomerOutput>>>(apiResponse);
        }
        return customerOutput;
    }

    public async Task<ApiResponse<bool>> Logout()
    {
        string endpoint = "http://localhost:5168/api/MsCustomer/Logout";
        var response = await httpClient.PostAsync(endpoint,null);
      
        if (response.IsSuccessStatusCode)
        {
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<bool>>(await response.Content.ReadAsStringAsync());
            return apiResponse;
        }
         var errorContent = await response.Content.ReadAsStringAsync();
        
       
        return new ApiResponse<bool>
        {
            StatusCode = response.StatusCode.ToString(),
            RequestMethod = "POST",
            Data = false
        };
    }
}
