using App7.ViewModel;
using App7.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using App7.Views.VistasNavegacion;
using Rg.Plugins.Popup.Extensions;

namespace App7.Views
{
    public partial class PedidoPage : TabbedPage
    {
        //public PedidoViewModel _vm;
        public PedidoPage(int page)
        {
            InitializeComponent();
            if(ViewModelLocator.PedidoUsuarioViewModel == null)
                ViewModelLocator.PedidoUsuarioViewModel = new PedidoViewModel(this);
            BindingContext = ViewModelLocator.PedidoUsuarioViewModel;
            this.CurrentPage = this.Children[page];
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await ViewModelLocator.PedidoUsuarioViewModel.ExecuteGetPedidosCommand();
           
            //this.EsperaList.ItemsSource = null;
            //this.EsperaList.ItemsSource = ViewModelLocator.PedidoUsuarioViewModel.Espera;

            //var position = new Position(viewModel.Proveedor.Latitude, viewModel.Proveedor.Longitude); // Latitude, Longitude
            //var pin = new Pin
            //{
            //    Type = PinType.Place,
            //    Position = position,
            //    Label = viewModel.Proveedor.Nombre,
            //    Address = viewModel.Proveedor.Direccion
            //};
            //MyMap.Pins.Add(pin);

            //MyMap.MoveToRegion(
            //    MapSpan.FromCenterAndRadius(
            //        position, Distance.FromMiles(.2)));
        }

        void SelecChange(object sender, ToggledEventArgs e)
        {
            
        }

        async void aceptar_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (!String.IsNullOrEmpty(bt.CommandParameter.ToString()))
            {
                var id = bt.CommandParameter.ToString();
                var plato = ViewModelLocator.PedidoUsuarioViewModel.Espera.FirstOrDefault(p => p._id == id);
                plato.Estado = "Aceptado";
                await ViewModelLocator.PedidoUsuarioViewModel.UpdatePedido(plato);
                ViewModelLocator.PedidoUsuarioViewModel.Espera.Remove(plato);
                ViewModelLocator.PedidoUsuarioViewModel.Pedidos.Add(plato);
                //Convertir en confirmado directamente
            }
        }

        async void detalle_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (!string.IsNullOrEmpty(bt.CommandParameter.ToString()))
            {
                var id = bt.CommandParameter.ToString();
                var plato = ViewModelLocator.PedidoUsuarioViewModel.Pedidos.FirstOrDefault(p => p._id == id);
                PedidoPageConfirm pg = new PedidoPageConfirm(plato);
                pg.confirmEvent += Pg_confirmEvent;
                await this.Navigation.PushAsync(pg);
            }
        }

        private async void Pg_confirmEvent(object sender, EventArgs e)
        {
            Pedido sen = sender as Pedido;
            await ViewModelLocator.PedidoUsuarioViewModel.UpdatePedido(sen);
            await ViewModelLocator.PedidoUsuarioViewModel.ExecuteGetPedidosCommand();
        }

        async void rechazar_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (!String.IsNullOrEmpty(bt.CommandParameter.ToString()))
            {
                var id = bt.CommandParameter.ToString();
                var plato = ViewModelLocator.PedidoUsuarioViewModel.Espera.FirstOrDefault(p => p._id == id);

                plato.Estado = "Declinado";
                await ViewModelLocator.PedidoUsuarioViewModel.UpdatePedido(plato);
                ViewModelLocator.PedidoUsuarioViewModel.Espera.Remove(plato);
                ViewModelLocator.PedidoUsuarioViewModel.Pedidos.Add(plato);
            }
        }


    }
}
