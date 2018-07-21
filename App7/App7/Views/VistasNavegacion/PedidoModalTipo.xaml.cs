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
    public partial class PedidoModalTipo: PopupPage
    {
        public event EventHandler confirmEvent;

        FoodLine fll;

        public PedidoModalTipo(FoodLine fl)
        {
            InitializeComponent();
            fll = fl;
        }

        async void reserva_Click(object sender, EventArgs e)
        {
            try
            {
                var ped = new PedidoModalPage(fll, "Reserva");
                await this.Navigation.PushModalAsync(ped);
                await this.Navigation.PopPopupAsync();
            }
            catch (Exception ex)
            {

            }
        }

        async void delivery_Click(object sender, EventArgs e)
        {
            try
            {
                var ped = new PedidoModalPage(fll, "");
                await this.Navigation.PushModalAsync(ped);
                await this.Navigation.PopPopupAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
