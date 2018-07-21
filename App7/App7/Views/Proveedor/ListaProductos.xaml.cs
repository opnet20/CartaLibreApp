using App7.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
//using Xamarin.Forms.Maps;
using App7.ServiceReference1;
using App7.ViewModel;
using App7.ViewModel.Proveedores;

namespace App7.Views.Proveedores
{
    public partial class ListaProductos : ContentPage
    {
        
        public ListaProductos()
        {
            try
            {
                InitializeComponent();
                if (ViewModelLocator.cartaViewModel == null)
                    ViewModelLocator.cartaViewModel = new MiCartaViewModel(this);
                //ViewModelLocator.cartaViewModel.agregar += CartaViewModel_agregar;
                BindingContext = ViewModelLocator.cartaViewModel;
            }
            catch (Exception ex)
            {

            }
        }

       

        //private void PlatoList_SelectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    var addpl = new AgregarPlato(AgregarPlato.Modo.Proveedor);
        //    addpl.close += Addpl_close;
        //    addpl.AgregarPlatillo(PlatoList.SelectedItem as Plato);
        //    PlatoList.SelectedItems.Clear();
        //    this.Navigation.PushModalAsync(addpl);
        //}

        //private void LvM_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var en = e.SelectedItem as Plato;
        //    var addpl = new AgregarPlato(AgregarPlato.Modo.Proveedor);
        //    addpl.close += Addpl_close;
        //    addpl.AgregarPlatillo(en);
        //    this.Navigation.PushModalAsync(addpl);
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

       
    }
}
