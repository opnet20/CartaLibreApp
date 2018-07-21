using App7.Interfaces;
using App7.ViewModel.Proveedores;
using App7.Views.Proveedor;
using App7.ServiceReference1;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App7.Views.Proveedores
{
    public partial class TiendaPage : TabbedPage
    {

        public TiendaPage()
        {
            try
            {
                InitializeComponent();
                if(ViewModelLocator.PedidosViewModel == null)
                    ViewModelLocator.PedidosViewModel = new MisPedidosViewModel(this);
                BindingContext = ViewModelLocator.PedidosViewModel;
               ViewModelLocator.PedidosViewModel.ConfirModal += PedidosViewModel_ConfirModal;
                ViewModelLocator.PedidosViewModel.agregar += CartaViewModel_agregar;
            }
            catch(Exception ex)
            {

            }
        }

        private async void CartaViewModel_agregar(object sender, EventArgs e)
        {
            var addpl = new AgregarPlato("Plato");
            addpl.close += Addpl_close;
            addpl.AgregarPlatillo(sender as Plato);
            await this.Navigation.PushAsync(addpl);
        }

        private async void PedidosViewModel_ConfirModal(object sender, EventArgs e)
        {
            var pa = new ConfirmarPedidoModal();
            await this.Navigation.PushAsync(pa);
        }

        void aceptar_Click(object sender, EventArgs e)
        {

        }
        void rechazar_Click(object sender, EventArgs e)
        {

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await ViewModelLocator.PedidosViewModel.Load();
        }

        //void Agregar_Click(object sender, EventArgs e)
        //{
        //    Button bt = sender as Button;

        //    var addpl = new AgregarPlato( bt.Text);
        //    addpl.close += Addpl_close;
        //    this.Navigation.PushModalAsync(addpl);
        //}

        private void Addpl_close(object sender, EventArgs e)
        {
            (BindingContext as MisPedidosViewModel).Load2();
        }

        void AddCircleButtonClick(object sender, EventArgs e)
        {
            var addpl = new AgregarPlato( "Plato");
            addpl.close += Addpl_close;
            this.Navigation.PushAsync(addpl);
        }
        void AddAlertButtonClick(object sender, EventArgs e)
        {
            var addpl = new AgregarPlato( "Oferta");
            addpl.close += Addpl_close;
            this.Navigation.PushAsync(addpl);
        }
        void EventButtonClick(object sender, EventArgs e)
        {
            var addpl = new AgregarPlato( "Evento");
            addpl.close += Addpl_close;
            this.Navigation.PushAsync(addpl);
        }

    }
}
