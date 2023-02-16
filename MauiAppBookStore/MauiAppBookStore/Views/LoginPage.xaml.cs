using Microsoft.Data.SqlClient;

namespace MauiAppBookStore;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new MainPage());

        //string sqlconn = "Data Source=https://10.161.168.246:7004;Database=BookStoreV2.Data;Trusted_Connection=True;MultipleActiveResultSets=true";
        //SqlConnection sqlConnection = new SqlConnection(sqlconn);
        //sqlConnection.Open();
    }
}