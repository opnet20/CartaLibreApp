using App7.Interfaces;
using App7.ServiceReference1;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App7.Views.VistasNavegacion
{
    public partial class OfertarPopUp : PopupPage
    {
        readonly IDataStore dataPedido;
        FoodLine platooferta;
        public OfertarPopUp(FoodLine p)
        {
            InitializeComponent();
            dataPedido = DependencyService.Get<IDataStore>();
            platooferta = p;
        }
        public async void AddPedido()
        {
            Pedido pp = new Pedido();
            pp.Cantidad = 1;
            pp.Plato = platooferta.CPlato.Nombre;
            pp.Total = double.Parse(precioEn.Text);
            pp.idPlato = platooferta.CPlato._id.ToString();
            pp.Usuario = platooferta.CPlato.NombreUsuario;
            pp.idProv = Settings.Provee._id; // yo
            pp.Proveedor = Settings.Provee.Nombre;
            pp.idUsu = platooferta.CPlato.idUsuario;
            pp.Detalle = detalleOfertaEn.Text;
            pp.Estado = "Oferta";
            pp.FechaPedido = DateTime.Now;
            pp.Select = true;
            pp.Direccion = "";
            await dataPedido.SetPedido(pp);
            loading.IsRunning = false; ;
            await this.DisplayAlert("¡Listo!", "Se envió tu oferta", "OK");
        }
        void confirmar_Click(object sender, EventArgs e)
        {
            confirmarBT.IsEnabled = false;
            loading.IsRunning = true;
            AddPedido();
            
            //this.Navigation.PushAsync(new PedidoPage());
        }
        async void cancelar_Click(object sender, EventArgs e)
        {
            await this.Navigation.PopPopupAsync();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
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

        protected override bool OnBackButtonPressed()
        {
            // Prevent hide popup
            //return base.OnBackButtonPressed();
            return true;
        }

        // Invoced when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return default value - CloseWhenBackgroundIsClicked
            return base.OnBackgroundClicked();
        }

    }
}
