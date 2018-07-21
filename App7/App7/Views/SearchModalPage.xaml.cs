using App7.Interfaces;
using App7.Model;
using App7.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App7.Views
{
    public partial class SearchModalPage : ContentPage
    {
        
        public event EventHandler close;
        readonly IDataStore dataPlato;

        public SearchModalPage()
        {
            InitializeComponent();
            dataPlato = DependencyService.Get<IDataStore>();
            searchBar.SearchButtonPressed += SearchBar_SearchButtonPressed;
            ObservableCollection<Result> opciones = new ObservableCollection<Result>();
           
            foreach (var i in PGlobal.categorias)
            {
                opciones.Add(new Result { Nombre = i.Titulo, Descripcion = "Categoría", Tipo = "General" });
            }

            CatlistView.ItemsSource = opciones;
            CatlistView.ItemSelected += CatlistView_ItemSelected; 
        }

        private async void CatlistView_SelectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (CatlistView.SelectedItem != null)
            {
                var item = (CatlistView.SelectedItem as Result);

                if (item.Tipo == "General")
                {
                    close(item.Nombre, null);
                    await this.Navigation.PopModalAsync();
                }
                if (item.Tipo == "Plato")
                {
                    var resultado = await dataPlato.GetOneFoodLine(item.Id);
                    await this.Navigation.PushModalAsync(new PlatoPage(resultado));
                    //Lanzo Plato por id
                }
                if (item.Tipo == "Proveedor")
                {
                    var resultado = await dataPlato.GetProveedorPorUsuario(item.Id);
                 
                    //Lanzo Proveedor por id

                    var win = new ProveedorPage(resultado);
                    await this.Navigation.PushModalAsync(win);
                }
            }
        }

        private async void CatlistView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //var item = (CatlistView.SelectedItem as ItemLista).Categoria;
            //close(item, null);
            //this.Navigation.PopModalAsync();

            if (CatlistView.SelectedItem != null)
            {
                var item = (CatlistView.SelectedItem as Result);
                CatlistView.SelectedItem = null;
                if (item.Tipo == "General")
                {
                    close(item.Nombre, null);
                    await this.Navigation.PopModalAsync();
                }
                if (item.Tipo == "Plato")
                {
                    var resultado = await dataPlato.GetOneFoodLine(item.Id);
                    await this.Navigation.PushModalAsync(new PlatoPage(resultado));
                    //Lanzo Plato por id
                }
                if (item.Tipo == "Proveedor")
                {
                    var resultado = await dataPlato.GetProveedorPorUsuario(item.Id);

                    //Lanzo Proveedor por id

                    var win = new ProveedorPage(resultado);
                    await this.Navigation.PushModalAsync(win);
                }
            }
        }

        private void Search(object sender, EventArgs e)
        {
            
        }

        private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            var resultado = await dataPlato.Search(searchBar.Text);

            CatlistView.ItemsSource = resultado;
        }
    }

 
}
