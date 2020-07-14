using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthcareApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignUpPage : ContentPage
	{
		public SignUpPage ()
		{
			InitializeComponent ();
		}

        private void SignInTapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            if(Password.Text==CenfirmPassword.Text)
            {
                DisplayAlert("Success", "Login Success", "Ok");
            }
            else
            {
                DisplayAlert("Error", "Password and confirm password not same,Plese Check and try again!", "Ok");
            }
        }
    }
}