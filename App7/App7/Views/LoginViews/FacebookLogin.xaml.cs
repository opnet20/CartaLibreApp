using App7.Interfaces;
using App7.ServiceReference1;
using FacebookLogin.Models;
using FacebookLogin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App7.Views
{
    public partial class FacebookLogin : ContentPage
    {
        private string ClientId = "291652144632898";
        public event EventHandler close;
        readonly IDataStore dataWCF;
        Usuario user;
        Service1Client mds = new Service1Client();

        public FacebookLogin()
        {
            try
            {
                InitializeComponent();
                dataWCF = DependencyService.Get<IDataStore>();
                BindingContext = new FacebookViewModel();
                var apiRequest =
                   "https://www.facebook.com/dialog/oauth?client_id="
                   + ClientId
                   + "&display=popup&response_type=token&redirect_uri=http://www.facebook.com/connect/login_success.html";

                var webView = new WebView
                {
                    Source = apiRequest,
                    HeightRequest = 1
                };

                webView.Navigated += WebViewOnNavigated;

                Content = webView;
            }
            catch(Exception ex)
            {

            }
        }

        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {
            try
            {
                var accessToken = ExtractAccessTokenFromUrl(e.Url);
                this.Content.HeightRequest = 0 ;
                if (accessToken != "")
                {
                    var vm = BindingContext as FacebookViewModel;

                    await vm.SetFacebookUserProfileAsync(accessToken);

                    user = new Usuario();
                    user.Apellidos = vm.FacebookProfile.LastName;
                    user.Nombres = vm.FacebookProfile.FirstName;
                    user.Imagen = vm.FacebookProfile.Picture.Data.Url;
                    user.Nick = vm.FacebookProfile.Name;
                    user.FacebookId = vm.FacebookProfile.Id;
                    user.Tipo = "U";
                    var usert = await dataWCF.GetUsuario(
                          user.Nick,
                           String.IsNullOrEmpty(user.FacebookId) ? null : user.FacebookId,
                           String.IsNullOrEmpty(user.Password) ? null : user.Password
                           );

                    if (usert != null)
                    {
                        Settings.UserName = usert.Nick;
                        Settings.UserPass = usert.Password;
                        Settings.FaceId = usert.FacebookId;
                        Settings.User = usert;
                        Settings.UserTipo = usert.Tipo;

                        close(true, null);
                        await Navigation.PopModalAsync();
                    }
                    else
                    {
                        mds.SetUsuarioCompleted += Mds_SetUsuarioCompleted; ;
                        mds.SetUsuarioAsync(user);
                    }
                  
                    //SetPageContent(vm.FacebookProfile);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void Mds_SetUsuarioCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            try
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    try
                    {
                        var usert = await dataWCF.GetUsuario(
                            user.Nick,
                             String.IsNullOrEmpty(user.FacebookId) ? null: user.FacebookId, 
                             String.IsNullOrEmpty(user.Password) ? null : user.Password
                             );

                        if (usert != null)
                        {
                            Settings.UserName = usert.Nick;
                            Settings.UserPass = usert.Password;
                            Settings.FaceId = usert.FacebookId;
                            Settings.User = usert;
                            close(true, null);
                            await Navigation.PopModalAsync();
                        }
                        else
                        {
                            close(false, null);
                            await Navigation.PopModalAsync();
                        }
                       
                       
                    }
                    catch (Exception ex)
                    {

                    }
                });
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void SetPageContent(FacebookProfile facebookProfile)
        {
            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(8, 30),
                Children =
                {
                    new Label
                    {
                        Text = facebookProfile.Name,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = facebookProfile.Id,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = facebookProfile.IsVerified.ToString(),
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = facebookProfile.Devices.FirstOrDefault().Os,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = facebookProfile.Gender,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = facebookProfile.AgeRange.Min.ToString(),
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = facebookProfile.Picture.Data.Url,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = facebookProfile.Cover.Source,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                }
            };
        }

        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");
                
                var accessToken = at.Remove(at.IndexOf("&expires_in="));

                return accessToken;
            }

            return string.Empty;
        }
    }   
}
