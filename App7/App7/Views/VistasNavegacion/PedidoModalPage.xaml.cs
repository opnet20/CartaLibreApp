using App7.ServiceReference1;
using App7.Views.VistasNavegacion;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace App7.Views
{
    public partial class PedidoModalPage : ContentPage
    {
        string _modo;
        Geocoder geo;
        public PedidoModalPage(FoodLine p, string modo)
        {
            try
            {
                InitializeComponent();
                if (ViewModelLocator.PedidoUsuarioViewModel == null)
                    ViewModelLocator.PedidoUsuarioViewModel = new ViewModel.PedidoViewModel(null);
                ViewModelLocator.PedidoUsuarioViewModel.PlatoPedidoModal = p;
                ViewModelLocator.PedidoUsuarioViewModel.Cantidad = 1;
                ViewModelLocator.PedidoUsuarioViewModel.DetallePedido = "";
                BindingContext = ViewModelLocator.PedidoUsuarioViewModel;
                var hora = DateTime.Now;
                var horavar = hora.AddMinutes(30);
                TimeSpan ts = new TimeSpan(horavar.Hour, horavar.Minute, 0);
                timepicker.Time = ts;

                _modo = modo;
                geo = new Geocoder();
                if (modo == "Reserva")
                {
                    direcTB.IsEnabled = false;
                    direcST.HeightRequest = 0;
                    fechaLb.Text = "Fecha y Hora de Consumo";
                }
                else
                    direcTB.IsEnabled = true;
            }
            catch (Exception ex)
            {

            }
        }


        async void confirmar_Click(object sender, EventArgs e)
        {
            string men = "Los pedidos pueden incurrir en costos de envío o costos adicionales por los detalles agregados, recibirás una respuesta por parte del proveedor con el detalle antes de culminar tu pedido, ¿Deseas continuar?";
            if (_modo == "Reserva")
                men = "La reserva pueden incurrir en costos adicionales por los detalles agregados, recibirás una respuesta por parte del proveedor con el detalle antes de culminar tu pedido, ¿Deseas continuar?";
            PedidoModalConfirm pmp = new PedidoModalConfirm(men);
            pmp.confirmEvent += Pmp_confirmEvent;
            await this.Navigation.PushPopupAsync(pmp);
            //this.Navigation.PushAsync(new PedidoPage());
        }

        private void Pmp_confirmEvent(object sender, EventArgs e)
        {
            var fechaent = new DateTime(datepicker.Date.Year,
              datepicker.Date.Month, datepicker.Date.Day, timepicker.Time.Hours, timepicker.Time.Minutes, 0);
            ViewModelLocator.PedidoUsuarioViewModel.Fechaentrega = fechaent.AddHours(-7);
            if (_modo == "Reserva")
                ViewModelLocator.PedidoUsuarioViewModel.DetalleDireccion = "Reserva";
            ViewModelLocator.PedidoUsuarioViewModel.AddPedido();
            this.Navigation.PopModalAsync();
        }

        void mas_Click(object sender, EventArgs e)
        {
            ViewModelLocator.PedidoUsuarioViewModel.Cantidad++;
        }
         void menos_Click(object sender, EventArgs e)
        {
            if (ViewModelLocator.PedidoUsuarioViewModel.Cantidad <= 1)
                return;
            else
                ViewModelLocator.PedidoUsuarioViewModel.Cantidad--;
        }
        async void cancelar_Click(object sender, EventArgs e)
        {
            await this.Navigation.PopModalAsync();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        void gps_Click(object sender, EventArgs e)
        {
            MapaRegistro mpr = new MapaRegistro("r");
            mpr.close += Mpr_close; ;
            this.Navigation.PushModalAsync(mpr);
        }

        private async void Mpr_close(object sender, EventArgs e)
        {
            try
            {
                XLabs.Platform.Services.Geolocation.Position po = sender as XLabs.Platform.Services.Geolocation.Position;
                if (po != null)
                {
                    gpsLb.Text = po.Latitude + "," + po.Longitude;
                    Position pos = new Position(po.Latitude, po.Longitude);
                    ViewModelLocator.PedidoUsuarioViewModel.DetalleDireccion = (await geo.GetAddressesForPositionAsync(pos)).FirstOrDefault();
                    var _coordinates = new double[2] { po.Latitude, po.Longitude };
                    ViewModelLocator.PedidoUsuarioViewModel.punto = new ServiceReference1.Point { type = "Point", coordinates = new System.Collections.ObjectModel.ObservableCollection<double>(_coordinates)};
                }
            }
            catch (Exception ex)
            {

            }
        }

        // Method for animation child in PopupPage
        // Invoced after custom animation end
        protected virtual Task OnAppearingAnimationEnd()
        {
            return Content.FadeTo(0.5);
        }

        // Method for animation child in PopupPage
        // Invoked before custom animation begin
        protected virtual Task OnDisappearingAnimationBegin()
        {
            return Content.FadeTo(1); ;
        }

        //protected override bool OnBackButtonPressed()
        //{
        //    // Prevent hide popup
        //    //return base.OnBackButtonPressed();
        //    return true;
        //}

        //// Invoced when background is clicked
        //protected override bool OnBackgroundClicked()
        //{
        //    // Return default value - CloseWhenBackgroundIsClicked
        //    return base.OnBackgroundClicked();
        //}

    }
}
