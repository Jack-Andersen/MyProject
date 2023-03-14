using MauiAppBookStore.Models;
using MauiAppBookStore.Services;
using Microsoft.Data.SqlClient;

namespace MauiAppBookStore;

public partial class LoginPage : ContentPage
{
    readonly ILoginRepository _loginRepository = new Loginservice();
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {

        string userName = txtUserName.Text;
        string password = txtPassword.Text;
        if(userName == null || password == null)
        {
            await DisplayAlert("Warning", "Please Input UserName & Password", "Ok");
            return;
        }
        try { 
            CustomerInfo customerInfo = await _loginRepository.Login(userName, password);
            if(customerInfo != null)
            {
                CustomerInfo.Current = customerInfo;
                await Navigation.PushAsync(new MainPage());
                var idk = customerInfo.CustomerId.ToString();
            }
        } catch
        {
            await DisplayAlert("Warning", "Username or Password is incorrect", "Ok");
        }
    }
}