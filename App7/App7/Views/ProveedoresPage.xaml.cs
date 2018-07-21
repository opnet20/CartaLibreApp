using App7.Model;
using App7.ServiceReference1;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Services.Geolocation;

namespace App7
{
    public partial class ProveedoresPage : ContentPage
    {
        ProveedoresViewModel viewModel;
        public Action<Proveedor> ItemSelected
        {
            get { return viewModel.ItemSelected; }
            set { viewModel.ItemSelected = value; }
        }

        private IGeolocator _geolocator;

        private IGeolocator Geolocator
        {
            get
            {
                if (_geolocator == null)
                {
                    var device = Resolver.Resolve<IGeolocator>();

                    ////RM: hack for working on windows phone? ?? Resolver.Resolve<IGeolocator>();
                    //_mediaPicker = DependencyService.Get<IMediaPicker>() ?? Resolver.Resolve<IGeolocator>();
                    _geolocator = DependencyService.Get<IGeolocator>() ?? device;
                    _geolocator.PositionError += _geolocator_PositionError;
                    _geolocator.PositionChanged += _geolocator_PositionChanged; 
                }
                return _geolocator;
            }
        }

        public ProveedoresPage()
        {
            try
            {
                InitializeComponent();
                searchBar.SearchButtonPressed += SearchBar_SearchButtonPressed;
                searchBar.TextChanged += SearchBar_TextChanged;
                BindingContext = viewModel = new ProveedoresViewModel(this);
                ProveedorList.Refreshing += ProveedorList_Refreshing;
                //FavoritosList.Refreshing += FavoritosList_Refreshing;
                //ProveedorList.IsGroupingEnabled = false;
                //ProveedorList.ItemsSource = viewModel.Proveedores;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async void _geolocator_PositionChanged(object sender, PositionEventArgs e)
        {
            this.viewModel.la = e.Position.Latitude;
            this.viewModel.lo = e.Position.Longitude;
            this.viewModel.distancia = int.Parse(valNum.Text) * 1000;
            
            this.viewModel.GetProveedoresCommand.Execute(null);
        }

        private void _geolocator_PositionError(object sender, PositionErrorEventArgs e)
        {
          
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchBar.Text.Length == 0)
            {
                if (viewModel.IsBusy)
                    return;
                viewModel.Text = "";
                viewModel.GetProveedoresCommand.Execute(null);
            }
        }

        //private async void FavoritosList_Refreshing(object sender, EventArgs e)
        //{
        //    await viewModel.GetFavoritos();
        //    FavoritosList.IsRefreshing = false;
        //}

        private void ProveedorList_Refreshing(object sender, EventArgs e)
        {
            viewModel.Text = "";
            viewModel.GetProveedoresCommand.Execute(null);
            ProveedorList.IsRefreshing = false;
        }

        private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            viewModel.Text = searchBar.Text;
            var prueba = viewModel.Proveedores;
            viewModel.GetProveedoresCommand.Execute(null);

        }

        private async void OK_Click(object sender, EventArgs e)
        {
            await this.Navigation.PopModalAsync();
        }

        private async void mas_Click(object sender, EventArgs e)
        {
            var val = int.Parse(valNum.Text);
            val++;
            valNum.Text = val.ToString();
        }

        private async void menos_Click(object sender, EventArgs e)
        {
            
            var val = int.Parse(valNum.Text);
            if (val > 1)
            {
                val--;
                valNum.Text = val.ToString();
            }
        }

        async void Seguir_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            string coman = bt.CommandParameter.ToString();

            Seguidores se = new Seguidores
            {
                idProv = coman,
                idUsuario = Settings.User._id,
                Fecha = DateTime.Now,
                IdPlayer = Settings.User.IdPlayer
            };

            await this.viewModel.SetSeguidor(se);
        }

        async void Localizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!viewModel.localizador)
                {
                    localizarBt.BackgroundColor = Color.FromHex("#e6e6e6");
                    localizarBt.TextColor = Color.FromHex("#EF4F50");
                    viewModel.localizador = true;
                    Geolocator.StartListening(40000, 10);
                }
                else
                {
                    localizarBt.BackgroundColor = Color.FromHex("#EF4F50");
                    localizarBt.TextColor = Color.White;
                    viewModel.localizador = false;
                    Geolocator.StopListening();
                    
                }
            }
            catch
            {

            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.Proveedores.Count > 0 || viewModel.IsBusy)
                return;
            viewModel.GetProveedoresCommand.Execute(null);
            //await viewModel.GetFavoritos();
        }

        //protected override bool OnBackButtonPressed()
        //{
        //    //base.OnBackButtonPressed();
        //    //if (this.Detail.GetType().ToString() == "Klaim.HomePage")
        //    //{
        //    //    Task<bool> action = DisplayAlert("Quitter?", "Voulez-vous quitter l'application?", "Oui", "Non");
        //    //    action.ContinueWith(task =>
        //    //    {
        //    //        if (task.Result)
        //    //            DisplayAlert("debugvalue", "TRUE", "ok");
        //    //    });
        //    //}
        //    //else
        //    //{
        //    //    ViewModel.NavigateTo(new HomePage(new HomeViewModel()));
        //    //    return true;
        //    //}
            
        //    return false;
        //}
   }
}
