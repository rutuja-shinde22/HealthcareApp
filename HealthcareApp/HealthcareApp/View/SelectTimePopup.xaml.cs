using Rg.Plugins.Popup.Services;
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
	public partial class SelectTimePopup 
	{
        bool buttonClicked = false;
		public SelectTimePopup ()
		{
			InitializeComponent ();
		}
        public void defoult()
        {
            string currentHour = DateTime.Now.Hour.ToString();
            string currentMinute = DateTime.Now.Minute.ToString();


            buttonClicked = false;
            Button1.BackgroundColor = Color.FromHex("#3498DB");
            Button1.TextColor = Color.White;
            Button2.BackgroundColor = Color.FromHex("#3498DB");
            Button2.TextColor = Color.White;
            Button3.BackgroundColor = Color.FromHex("#3498DB");
            Button3.TextColor = Color.White;
            Button4.BackgroundColor = Color.FromHex("#3498DB");
            Button4.TextColor = Color.White;
            Button5.BackgroundColor = Color.FromHex("#3498DB");
            Button5.TextColor = Color.White;
            Button6.BackgroundColor = Color.FromHex("#3498DB");
            Button6.TextColor = Color.White;
            Button7.BackgroundColor = Color.FromHex("#3498DB");
            Button7.TextColor = Color.White;
            Button8.BackgroundColor = Color.FromHex("#3498DB");
            Button8.TextColor = Color.White;

            Button9.BackgroundColor = Color.FromHex("#3498DB");
            Button9.TextColor = Color.White;
            Button10.BackgroundColor = Color.FromHex("#3498DB");
            Button10.TextColor = Color.White;
            Button10.BackgroundColor = Color.FromHex("#3498DB");
            Button11.TextColor = Color.White;
            Button11.BackgroundColor = Color.FromHex("#3498DB");
            Button12.TextColor = Color.White;
            Button12.BackgroundColor = Color.FromHex("#3498DB");
            Button13.TextColor = Color.White;
            Button13.BackgroundColor = Color.FromHex("#3498DB");
            Button14.TextColor = Color.White;
            Button14.BackgroundColor = Color.FromHex("#3498DB");
            Button15.TextColor = Color.White;
            Button15.BackgroundColor = Color.FromHex("#3498DB");
            Button16.TextColor = Color.White;

            Button17.BackgroundColor = Color.FromHex("#3498DB");
            Button17.TextColor = Color.White;
            Button18.BackgroundColor = Color.FromHex("#3498DB");
            Button18.TextColor = Color.White;
            Button19.BackgroundColor = Color.FromHex("#3498DB");
            Button19.TextColor = Color.White;
            Button20.BackgroundColor = Color.FromHex("#3498DB");
            Button20.TextColor = Color.White;
            Button21.BackgroundColor = Color.FromHex("#3498DB");
            Button21.TextColor = Color.White;
            Button22.BackgroundColor = Color.FromHex("#3498DB");
            Button22.TextColor = Color.White;
            Button23.BackgroundColor = Color.FromHex("#3498DB");
            Button23.TextColor = Color.White;
            Button24.BackgroundColor = Color.FromHex("#3498DB");
            Button24.TextColor = Color.White;

            Button25.BackgroundColor = Color.FromHex("#3498DB");
            Button25.TextColor = Color.White;
            Button26.BackgroundColor = Color.FromHex("#3498DB");
            Button26.TextColor = Color.White;
            Button27.BackgroundColor = Color.FromHex("#3498DB");
            Button27.TextColor = Color.White;
            Button28.BackgroundColor = Color.FromHex("#3498DB");
            Button28.TextColor = Color.White;
            Button29.BackgroundColor = Color.FromHex("#3498DB");
            Button29.TextColor = Color.White;
            Button30.BackgroundColor = Color.FromHex("#3498DB");
            Button30.TextColor = Color.White;
            Button31.BackgroundColor = Color.FromHex("#3498DB");
            Button31.TextColor = Color.White;
            Button32.BackgroundColor = Color.FromHex("#3498DB");
            Button32.TextColor = Color.White;

            Button33.BackgroundColor = Color.FromHex("#3498DB");
            Button33.TextColor = Color.White;
            Button34.BackgroundColor = Color.FromHex("#3498DB");
            Button34.TextColor = Color.White;
            Button35.BackgroundColor = Color.FromHex("#3498DB");
            Button35.TextColor = Color.White;
            Button36.BackgroundColor = Color.FromHex("#3498DB");
            Button36.TextColor = Color.White;
            Button37.BackgroundColor = Color.FromHex("#3498DB");
            Button37.TextColor = Color.White;
            Button38.BackgroundColor = Color.FromHex("#3498DB");
            Button38.TextColor = Color.White;
            Button39.BackgroundColor = Color.FromHex("#3498DB");
            Button39.TextColor = Color.White;
            Button40.BackgroundColor = Color.Blue;
            Button40.TextColor = Color.White;
        }

        private void Button1_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button1.BackgroundColor = Color.White;
            Button1.TextColor = Color.Blue;

        }

        private void Button2_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button2.BackgroundColor = Color.White;
            Button2.TextColor = Color.Blue;
        }

        private void Button3_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button3.BackgroundColor = Color.White;
            Button3.TextColor = Color.Blue;

        }

        private void Button4_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button4.BackgroundColor = Color.White;
            Button4.TextColor = Color.Blue;
        }

        private void Button5_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button5.BackgroundColor = Color.White;
            Button5.TextColor = Color.Blue;
        }

        private void Button6_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button6.BackgroundColor = Color.White;
            Button6.TextColor = Color.Blue;
        }

        private void Button7_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button7.BackgroundColor = Color.White;
            Button7.TextColor = Color.Blue;
        }

        private void Button8_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button8.BackgroundColor = Color.White;
            Button8.TextColor = Color.Blue;
            buttonClicked = true;
        }

        private void Button9_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button9.BackgroundColor = Color.White;
            Button9.TextColor = Color.Blue;
        }

        private void Button10_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button10.BackgroundColor = Color.White;
            Button10.TextColor = Color.Blue;
        }

        private void Button11_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button11.BackgroundColor = Color.White;
            Button11.TextColor = Color.Blue;
        }

        private void Button12_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button12.BackgroundColor = Color.White;
            Button12.TextColor = Color.Blue;
        }

        private void Button13_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button13.BackgroundColor = Color.White;
            Button13.TextColor = Color.Blue;
        }

        private void Button14_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button14.BackgroundColor = Color.White;
            Button14.TextColor = Color.Blue;
        }

        private void Button15_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button15.BackgroundColor = Color.White;
            Button15.TextColor = Color.Blue;
        }

        private void Button16_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button16.BackgroundColor = Color.White;
            Button16.TextColor = Color.Blue;
        }

        private void Button17_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button17.BackgroundColor = Color.White;
            Button17.TextColor = Color.Blue;
        }

        private void Button18_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button18.BackgroundColor = Color.White;
            Button18.TextColor = Color.Blue;
        }

        private void Button19_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button19.BackgroundColor = Color.White;
            Button19.TextColor = Color.Blue;
        }

        private void Button20_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button20.BackgroundColor = Color.White;
            Button20.TextColor = Color.Blue;
        }

        private void Button21_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button21.BackgroundColor = Color.White;
            Button21.TextColor = Color.Blue;
        }

        private void Button22_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button22.BackgroundColor = Color.White;
            Button22.TextColor = Color.Blue;
        }

        private void Button23_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button23.BackgroundColor = Color.White;
            Button23.TextColor = Color.Blue;
        }

        private void Button24_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button24.BackgroundColor = Color.White;
            Button24.TextColor = Color.Blue;
        }

        private void Button25_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button25.BackgroundColor = Color.White;
            Button25.TextColor = Color.Blue;
        }

        private void Button26_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button26.BackgroundColor = Color.White;
            Button26.TextColor = Color.FromHex("");
        }

        private void Button27_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button27.BackgroundColor = Color.White;
            Button27.TextColor = Color.Blue;
        }

        private void Button28_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button28.BackgroundColor = Color.White;
            Button28.TextColor = Color.Blue;
        }

        private void Button29_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button29.BackgroundColor = Color.White;
            Button29.TextColor = Color.Blue;
        }

        private void Button30_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button30.BackgroundColor = Color.White;
            Button30.TextColor = Color.Blue;
        }

        private void Button31_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button31.BackgroundColor = Color.White;
            Button31.TextColor = Color.Blue;
        }

        private void Button32_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button32.BackgroundColor = Color.White;
            Button32.TextColor = Color.Blue;
        }

        private void Button33_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button33.BackgroundColor = Color.White;
            Button33.TextColor = Color.Blue;
        }

        private void Button34_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button34.BackgroundColor = Color.White;
            Button34.TextColor = Color.Blue;
        }

        private void Button35_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button35.BackgroundColor = Color.White;
            Button35.TextColor = Color.Blue;
        }

        private void Button36_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button36.BackgroundColor = Color.White;
            Button36.TextColor = Color.Blue;
        }

        private void Button37_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button37.BackgroundColor = Color.White;
            Button37.TextColor = Color.Blue;
        }

        private void Button38_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button38.BackgroundColor = Color.White;
            Button38.TextColor = Color.Blue;
        }

        private void Button39_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button39.BackgroundColor = Color.White;

            Button39.TextColor = Color.Blue;
        }

        private void Button40_Clicked(object sender, EventArgs e)
        {
            defoult();
            Button40.BackgroundColor = Color.White;
            Button40.TextColor = Color.Blue;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }
    }
}