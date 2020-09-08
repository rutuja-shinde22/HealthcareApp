using HealthcareApp.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HealthcareApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
  
            MainPage = new NavigationPage(new ProfilePage());
            //MainPage = new MasterDetailPageNav();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        private static ICommonInterface _HealthSoapService;
        public static ICommonInterface HealthSoapService
        {
            get
            {
                if (_HealthSoapService == null)
                {
                    _HealthSoapService = DependencyService.Get<ICommonInterface>();
                }

                return _HealthSoapService;
            }
        }
    }
}
