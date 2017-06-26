using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinAllianceApp.Services;

namespace XamarinAllianceApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        bool Authenticated;
        public LoginPage()
        {
            InitializeComponent();
        }
        public async void btnLogin_Clicked(object sender, EventArgs e)
        {
            if (App.Authenticator != null)
            {
                Authenticated = await App.Authenticator.Authenticate();
            }

            if (Authenticated)
            {
                await Navigation.PushAsync(new CharacterListPage());
            }
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await GetImage();
            await GetGUID();
        }
        async Task GetImage()
        {
            var stream = await ImageService.GetImage();
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            stream.Position = 0;
            ImgLogo.Source = ImageSource.FromStream(() => stream);
        }
        async Task GetGUID()
        {
            var guid = await ImageService.GetGUID();
            guidLabel.Text = guid;
        }
    }
}