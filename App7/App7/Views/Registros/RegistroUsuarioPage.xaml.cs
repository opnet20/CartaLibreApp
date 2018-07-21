using App7.Interfaces;
using App7.ServiceReference1;
using App7.Services;
using App7.ViewModel;
using Com.OneSignal;
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using XLabs.Forms.Validation;

namespace App7.Views
{
    public partial class RegistroUsuarioPage : ContentPage
    {
        RegistroUsuarioViewModel _vm;
        readonly IDataStore dataWCF;
        public event EventHandler close;
        Service1Client mds = new Service1Client();
        bool inicio = true;
        private static readonly Regex EmailAddress =
            new Regex(
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))"
                + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");


        public RegistroUsuarioPage(Usuario u)
        {
            try
            {
                InitializeComponent();
                dataWCF = DependencyService.Get<IDataStore>();
                BindingContext = _vm = new RegistroUsuarioViewModel(this);
                _vm.User = u;
                _vm.Setup();
                //imagenCir.Aspect = Aspect.AspectFit
                if (String.IsNullOrEmpty(u.Nick))
                {// al Inicio
                    CompFormSL.IsVisible = false;
                    msjLB.IsVisible = true;
                    inicio = true;
                }
                else
                { //Ya registrado
                    CompFormRE.IsVisible = false;
                    msjLB.IsVisible = false;
                    msjLB.HeightRequest = 0;
                    guardarBt.Text = "Guardar";
                    Ciudad2.SelectedItem = u.Ciudad;
                    inicio = false;
                    guardarBt2.IsVisible = false;
                    guardarBt2.HeightRequest = 0;
                }
            }
            catch (Exception ex)
            {

            }

        }

        private static bool IsEmailAddress(string value)
        {
            try
            {
                return EmailAddress.IsMatch(value);
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
                throw;
            }
        }


        public async void Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                //if(inicio)
                //    _vm.User.Ciudad = Ciudad1.SelectedItem.ToString();

                
                if (String.IsNullOrEmpty(_vm.User.Nick) || String.IsNullOrEmpty(_vm.User.Password) || String.IsNullOrEmpty(_vm.User.Correo))
                {
                    await this.DisplayAlert("Completar", "Complete los campos", "OK");
                    return;
                }

                if (!IsEmailAddress(_vm.User.Correo))
                {
                    await this.DisplayAlert("Error", "Formato de correo", "OK");
                    return;
                }


                ActualizarIDPlayer();
                if (!inicio)
                    _vm.User.Ciudad = Ciudad2.SelectedItem.ToString();
                _vm.User.Nombres = _vm.User.Nick;
                guardarBt.IsEnabled = false;
                
                loading.IsRunning = true;

                mds.SetUsuarioCompleted += Mds_SetUsuarioCompleted;
                if (_vm.ImagenArray != null && _vm.ImagenArray.Length > 5)
                    _vm.User.Imagen = "si";
                mds.SetUsuarioAsync(_vm.User);
                
                //_vm.ReadToEnd(foto);

            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Error", ex.Message, "OK");
            }
        }


        void ActualizarIDPlayer()
        {
            OneSignal.Current.IdsAvailable(IdsAvailable);
        }

        private void IdsAvailable(string userID, string pushToken)
        {
            _vm.User.IdPlayer = userID;
        }


        private async void Mds_SetUsuarioCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            try
            {
                Device.BeginInvokeOnMainThread(async () => 
                {
                    try
                    {
                        var ee = e.Error;
                        if (ee != null)
                        {
                            throw new Exception(ee.Message);
                        }
                        var user = await dataWCF.GetUsuario(_vm.User.Nick,null, String.IsNullOrEmpty(_vm.User.Password) ? null : _vm.User.Password);

                        if (user != null)
                        {
                            Settings.UserName = user.Nick;
                            Settings.UserPass = user.Password;

                            if (_vm.ImagenArray != null)
                            {
                                mds.SetFotoCompleted += Mds_SetFotoCompleted;
                                mds.SetFotoAsync(_vm.ImagenArray, "\\users\\" + user._id.ToString() + ".jpg");
                            }
                            else
                            {
                                await this.DisplayAlert("Registro", "Correcto", "OK");
                                close(true, null);
                                loading.IsRunning = false;
                            }
                        }
                        else
                        {
                            await this.DisplayAlert("Error", "Ocurrio un error, vuelva a intentarlo", "OK");
                            loading.IsRunning = false;
                            guardarBt.IsEnabled = true;
                        }

                        loading.IsRunning = false;
                    }
                    catch (Exception ex)
                    {
                        await this.DisplayAlert("Error", ex.Message, "OK");
                        guardarBt.IsEnabled = true;
                        loading.IsRunning = false;
                    }
                });
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Error", ex.Message, "OK");
                guardarBt.IsEnabled = true;
            }
        }

        //Cuando la foto este completa
        private void Mds_SetFotoCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Device.BeginInvokeOnMainThread( async () =>
            {
                if (e.Error == null)
                {
                    loading.IsRunning = false;
                    guardarBt.IsEnabled = true;
                    if (close != null)
                        close(true, null);
                    else
                        await this.DisplayAlert("Registro", "Correcto", "OK");
                }
                else
                {
                    string aviso = e.Error.Message;
                    await this.DisplayAlert("Error", aviso , "OK");
                }
            });
        }

        //private void Proxy_SetFotoCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        //{
        //    try
        //    { }
        //    catch (Exception ex)
        //    {

        //    }
        //}
    }
}
