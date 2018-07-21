using App7.ViewModel.Proveedores;
using App7.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;

namespace App7.Views.Proveedor
{
    public partial class ConfirmarPedidoModalMap : PopupPage
    {
        public ConfirmarPedidoModalMap()
        {
            try
            {
                InitializeComponent();
                map.MoveToRegion(
                  MapSpan.FromCenterAndRadius(
                   new Xamarin.Forms.GoogleMaps.Position(-12.098957, -77.019766), Distance.FromMiles(100)));
                BindingContext = ViewModelLocator.PedidosViewModel;               
                InitMap(ViewModelLocator.PedidosViewModel.Solicitud.Location);
            }
            catch (Exception ex)
            {

            }
        }

        async void cancelar_Click(object sender, EventArgs e)
        {
            await this.Navigation.PopPopupAsync();
        }
        public void InitMap(ServiceReference1.Point p)
        {
            var pinTokyo = new Pin()
            {
                Type = PinType.Generic,
                Label = "",
                Address = "",
                Position = new Xamarin.Forms.GoogleMaps.Position(p.coordinates[0], p.coordinates[1]),
                IsVisible = true,
                Tag = p,
                Flat = false
            };

            map.Pins.Add(pinTokyo);

            map.MoveToRegion(
              MapSpan.FromCenterAndRadius(
                   new Xamarin.Forms.GoogleMaps.Position(p.coordinates[0],
                   p.coordinates[1]), Distance.FromMiles(1)));
        }
    }
}
