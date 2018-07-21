using App7.Interfaces;
using App7.ServiceReference1;
using Com.OneSignal;
using FacebookLogin.Models;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App7.Views
{
    public partial class Login : ContentPage
    {
        public event EventHandler loginEvent;
        readonly IDataStore dataWCF;
        public Login()
        {
            InitializeComponent();
            dataWCF = DependencyService.Get<IDataStore>();
        }
        //async void Login_Click(object sender, EventArgs e)
        //{
        //    var user =await dataWCF.GetUsuario(usuarioTB.Text, passTB.Text);
        //    if (user == null)
        //        await this.DisplayAlert("Error", "Verifique sus datos", "OK");
        //    else
        //    {
        //        PGlobal.MyUser = user;

        //        if (PGlobal.MyUser.Tipo == "P" && !String.IsNullOrEmpty(PGlobal.MyUser.IdProv))
        //        {
        //            PGlobal.MyProv = await dataWCF.GetProveedorPorUsuario(PGlobal.MyUser._id);
        //        }
        //        loginEvent(null, null);
        //    }
        //}
        
        async void Registro_Click(object sender, EventArgs e)
        {
           
            var registro = new RegistroUsuarioPage(new Usuario { Tipo = "U" });
            registro.close += Page_close;
            await this.Navigation.PushModalAsync(registro);
        }

        public  async void Facebook_Click(object sender, EventArgs e)
        {
            var face = new FacebookLogin();
            face.close += Page_close;
            await Navigation.PushModalAsync(face);
        }

        public async void Login_Click(object sender, EventArgs e)
        {
            try
            {
                var face = new LoginPass();
                face.close += Page_close;
                await Navigation.PushPopupAsync(face);
            }
            catch(Exception ex)
            {

            }
        }

        private void Page_close(object sender, EventArgs e)
        {
            bool se = (bool)sender;
            if (se)
            {
                loginEvent(null, null);
            }
        }

        void ActualizarIDPlayer()
        {
            OneSignal.Current.IdsAvailable(IdsAvailable);
        }

        private void IdsAvailable(string userID, string pushToken)
        {
            Settings.User.IdPlayer = userID;
            Service1Client mds = new Service1Client();
            mds.SetUsuarioCompleted += Mds_SetUsuarioCompleted; ;
            mds.SetUsuarioAsync(Settings.User);
        }

        private void Mds_SetUsuarioCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {

        }

        public async void Guardar_Click(object sender, EventArgs e)
        {
            loading.IsRunning = true;
            loginButton.IsEnabled = false;
            var usert = await dataWCF.GetUsuario(nombreTB.Text, "", passTB.Text);

            if (usert != null)
            {
                Settings.UserName = usert.Nick;
                Settings.UserPass = usert.Password;
                Settings.FaceId = usert.FacebookId;
                Settings.UserTipo = usert.Tipo;
                Settings.User = usert;
                ActualizarIDPlayer();
                //await Navigation.PopModalAsync();
                loginEvent(null, null);
            }
            else
            {
                await this.DisplayAlert("Login", "No se encontro el usuario", "OK");
            }
            loading.IsRunning = false;
            loginButton.IsEnabled = true;
        }
        //private void Registro_close(object sender, EventArgs e)
        //{
        //    bool se = (bool)sender;
        //    if (se)
        //    {
        //        loginEvent(null, null);
        //    }
        //}
    }
}
