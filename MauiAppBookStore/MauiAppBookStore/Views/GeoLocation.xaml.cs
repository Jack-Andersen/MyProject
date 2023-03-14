using Azure.Core;
using MauiAppBookStore.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;
using Method = RestSharp.Method;

namespace MauiAppBookStore.Views;

public partial class GeoLocation : ContentPage
{

    public GeoLocation()
    {
        InitializeComponent();
    }

    private async void OnGeoLocation(object sender, EventArgs e)
    {
        try
        {
            var location = await GetLocationAsync();
            if (location != null)
            {
                // Use the location
                LatitudeLabel.Text = $"Latitude: {location.Latitude}";
                LongitudeLabel.Text = $"Longitude: {location.Longitude}";
            }
            else
            {
                // Location not available
                await DisplayAlert("Warning", "Location not found", "Ok");
            }
        }
        catch (Exception ex)
        {
            // Handle error
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task<Location> GetLocationAsync()
    {
        try
        {
            // Obtain the public IP address of the device
            var client = new RestClient("https://api.ipgeolocation.io");
            var request = new RestRequest($"/ipgeo?apiKey=0030ae9b89f44a3d83d490243e511505", Method.Get);
            var response = await client.ExecuteAsync(request);
            var content = response.Content;
            var ipInfo = JsonConvert.DeserializeObject<IpInfo>(content);

            // Convert the location data to a Location object
            var location = new Location(ipInfo.Latitude, ipInfo.Longitude);

            return location;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }
}