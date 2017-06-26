
using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Threading.Tasks;
using XamarinAllianceApp.Helpers;
using XamarinAllianceApp.Services;

namespace XamarinAllianceApp.Droid
{
    [Activity (Label = "Xamarin Alliance",
		Icon = "@drawable/icon",
		MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
		Theme = "@android:style/Theme.Holo.Light")]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity, IAuthenticate
	{
        private MobileServiceUser user;
        public async Task<bool> Authenticate()
        {
            // throw new NotImplementedException();
            var success = false;
            var message = string.Empty;
            try
            {
                var client = new MobileServiceClient(Constants.MobileServiceAuthentication);
                //conexion con el servidor con facebook
                user = await client.LoginAsync(this, MobileServiceAuthenticationProvider.Facebook);
                if (user!=null)
                {
                    //estas logeado en facebook
                    message = $"you are now signed-in as {user.UserId}.";
                    success = true;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            //desplegar mensaje en caso de exito o falla
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetMessage(message);
            builder.SetTitle("Sign in result");
            builder.Create().Show();
            return success;
          
        }   
		protected override void OnCreate (Bundle bundle)
		{

            base.OnCreate (bundle);

			// Initialize Xamarin Forms
			global::Xamarin.Forms.Forms.Init (this, bundle);

            CurrentPlatform.Init();
            App.Init(this);
			// Load the main application
			LoadApplication (new App ());
		}
	}
}

