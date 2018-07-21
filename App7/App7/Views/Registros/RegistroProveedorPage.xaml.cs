using App7.Interfaces;
using App7.ServiceReference1;
using App7.ViewModel;
using App7.Views.VistasNavegacion;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using Xamarin.Forms;
using XLabs.Platform.Services.Geolocation;

namespace App7.Views
{
    public partial class RegistroProveedorPage : TabbedPage
    {
        RegistroProveedoresViewModel _vm;
        Service1Client mds = new Service1Client();
        readonly IDataStore dataWCF;
        public event EventHandler close;
        public RegistroProveedorPage(App7.ServiceReference1.Proveedor p)
        {
            try
            {
                InitializeComponent();

                dataWCF = DependencyService.Get<IDataStore>();
                BindingContext = _vm = new RegistroProveedoresViewModel(this,p);
                paisPi.SelectedItem = p.Pais;
                cuidadPi.SelectedItem = p.Ciudad;
                _vm.LoadTipos();
                tiposPi.ItemsSource = _vm.Items;
                //MyAutoComplete.TextChanged += MyAutoComplete_TextChanged;
                if(!String.IsNullOrEmpty(p.Tipo))
                    tiposPi.SelectedItem = p.Tipo;
                latLb.Text = "" + p.Latitude;
                lonLb.Text = "" + p.Longitude;

                stackL.HeightRequest = _vm.Prov.L ? 40:0;
                stackM.HeightRequest = _vm.Prov.M ? 40 : 0;
                stackMi.HeightRequest = _vm.Prov.Mi ? 40 : 0;
                stackJ.HeightRequest = _vm.Prov.J ? 40 : 0;
                stackV.HeightRequest = _vm.Prov.V ? 40 : 0;
                stackS.HeightRequest = _vm.Prov.S ? 40 : 0;
                stackD.HeightRequest = _vm.Prov.D ? 40 : 0;

            }
            catch (Exception ex)
            {

            }

        }
        //void MyAutoComplete_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    _vm.LoadProducts(e.NewTextValue);
        //}
        public void Toggled_Click(object sender, ToggledEventArgs e)
        {
            try
            {
                if (e.Value)
                {
                    MapaRegistro mpr = new MapaRegistro("r");
                    mpr.close += Mpr_close;
                    this.Navigation.PushModalAsync(mpr);
                }
            }
            catch(Exception ex)
            {

            }
        }

        public void ToggledDiasL_Click(object sender, ToggledEventArgs e)
        {
            try
            {
                if (e.Value)
                {
                    stackL.HeightRequest = 40;
                }
                else
                {
                    stackL.HeightRequest = 0;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void ToggledDiasM_Click(object sender, ToggledEventArgs e)
        {
            try
            {
                if (e.Value)
                {
                    stackM.HeightRequest = 40;
                }
                else
                {
                    stackM.HeightRequest = 0;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void ToggledDiasMi_Click(object sender, ToggledEventArgs e)
        {
            try
            {
                if (e.Value)
                {
                    stackMi.HeightRequest = 40;
                }
                else
                {
                    stackMi.HeightRequest = 0;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void ToggledDiasJ_Click(object sender, ToggledEventArgs e)
        {
            try
            {
                if (e.Value)
                {
                    stackJ.HeightRequest = 40;
                }
                else
                {
                    stackJ.HeightRequest = 0;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void ToggledDiasV_Click(object sender, ToggledEventArgs e)
        {
            try
            {
                if (e.Value)
                {
                    stackV.HeightRequest = 40;
                }
                else
                {
                    stackV.HeightRequest = 0;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void ToggledDiasS_Click(object sender, ToggledEventArgs e)
        {
            try
            {
                if (e.Value)
                {
                    stackS.HeightRequest = 40;
                }
                else
                {
                    stackS.HeightRequest = 0;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void ToggledDiasD_Click(object sender, ToggledEventArgs e)
        {
            try
            {
                if (e.Value)
                {
                    stackD.HeightRequest = 40;
                }
                else
                {
                    stackD.HeightRequest = 0;
                }
            }
            catch (Exception ex)
            {

            }
        }


        private void Mpr_close(object sender, EventArgs e)
        {
            Position po = sender as Position;
            _vm.Prov.Latitude = po.Latitude;
            _vm.Prov.Longitude = po.Longitude;

            latLb.Text =""+ po.Latitude;
            lonLb.Text =""+ po.Longitude;
            
        }

        public async void Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                GuardarBt.IsEnabled = false;
                loading.IsRunning = true;
                bool GuardaImagen = false;

                if (String.IsNullOrEmpty(_vm.Prov.Nombre) || String.IsNullOrEmpty(_vm.Prov.Descripcion) || String.IsNullOrEmpty(_vm.Prov.Correo))
                {
                    await this.DisplayAlert("Completar Nombre, Descripción, Correo", "Complete los campos", "OK");
                    loading.IsRunning = false;
                    return;
                }

                if (_vm.ImagenArray != null && _vm.ImagenArray.Length > 5)
                {
                    if (_vm.Prov.Imagen == _vm.ImagenArray.Length.ToString())
                        GuardaImagen = false;
                    else
                    {
                        GuardaImagen = true;
                        _vm.Prov.Imagen = _vm.ImagenArray.Length.ToString();
                    }
                }

                

                try
                {
                    _vm.Prov.Tipo = tiposPi.SelectedItem.ToString();
                    _vm.Prov.Pais = paisPi.SelectedItem.ToString();
                    _vm.Prov.Ciudad = cuidadPi.SelectedItem.ToString();
                }
                catch
                {
                    await this.DisplayAlert("Error", "Complete los datos: Tipo de Comida, Pais, Ciudad", "OK");
                    loading.IsRunning = false;
                    GuardarBt.IsEnabled = true;
                    return;
                }
                await dataWCF.SetProveedor(_vm.Prov);

                Settings.Provee = await dataWCF.GetProveedorPorUsuario(Settings.User._id);
                await dataWCF.SetUsuarioProv(Settings.User._id, "P", Settings.Provee._id);
                Settings.User.IdProv = Settings.Provee._id;
                Settings.User.Tipo = "P";
                Settings.UserTipo = "P";

                if (GuardaImagen)
                {
                     mds.SetFotoCompleted += Mds_SetFotoCompleted;
                    string pa = "\\proveedores\\" + Settings.Provee._id.ToString() + ".jpg";
                    mds.SetFotoAsync(_vm.ImagenArray, pa);
                }
                else
                {
                    GuardarBt.IsEnabled = true;
                    loading.IsRunning = false;
                    await this.DisplayAlert("Confirmación", "Registro Correcto", "OK");
                    close(null, null);
                    await this.Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Error", ex.Message, "OK");
                loading.IsRunning = false;
                GuardarBt.IsEnabled = true;
            }
        }

        private void Mds_SetFotoCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            try
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    loading.IsRunning = false;
                    await this.DisplayAlert("Confirmación", "Registro Correcto", "OK");
                    GuardarBt.IsEnabled = true;
                    close(null, null);
                    await this.Navigation.PopAsync();
                });
            }
            catch (Exception ex)
            {

            }
        }

      
        public  void Legal_Completed(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(_vm.Prov.Representante)
                && String.IsNullOrEmpty(_vm.Prov.Ruc)
                && String.IsNullOrEmpty(_vm.Prov.Registro))
            {
                //No lleno nada, proveedor nuevo
                catProveLb.Text = "Eres un proveedor nuevo:";
                catImg.Source = "Egg.png";
                
            }
            else
            {
                //lleno algo, proveedor en proceso
                catProveLb.Text = "Eres un proveedor registrado:";
                catImg.Source = "settings.png";
                _vm.Prov.Categoria = "P";
                if (!String.IsNullOrEmpty(_vm.Prov.Representante)
                   && !String.IsNullOrEmpty(_vm.Prov.Ruc)
                   && !String.IsNullOrEmpty(_vm.Prov.Registro))
                {
                    //lleno todo, proveedor calificado
                    catProveLb.Text = "Eres un proveedor calificado:";
                    catImg.Source = "check.png";
                    _vm.Prov.Categoria = "C";
                }
            }
        }

   
    }
}
