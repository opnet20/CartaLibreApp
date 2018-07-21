using App7.Model;
using App7.ViewModel;
using App7.Views;
using App7.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.Primitives;
using Xamarin.Forms;
using App7.Views.Proveedores;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Extensions;
using System.Collections.ObjectModel;
using App7.Views.VistasNavegacion;
using Com.OneSignal;
using XLabs.Forms.Controls;
using XLabs.Platform.Services.Geolocation;
using XLabs.Ioc;
using HorizontalListView;

namespace App7
{
    public partial class PlatosPage : ContentPage
    {
        //PlatosViewModel viewModel;
        //public Action<FoodLine> ItemSelected
        //{
        //    get { return viewModel.ItemSelected; }
        //    set { viewModel.ItemSelected = value; }
        //}

        Service1Client mds = new Service1Client();
        public event EventHandler setColorMain;
        public event EventHandler salirMain;
        
        bool locator = false;
      
        bool inicio = true;
        string idPlato = "";

       
        public PlatosPage()
        {
            try
            {
                Proveedor p = new Proveedor();
              
                InitializeComponent();

                //BindingContext = viewModel = new PlatosViewModel(this);
                //ViewModelLocator.PlatosViewModel.SetPage(this);
                //if(ViewModelLocator.PlatosViewModel == null)
                ViewModelLocator.PlatosViewModel = new PlatosViewModel(this);
                ViewModelLocator.PlatosViewModel.salirMainMv += PlatosViewModel_salirMainMv;
                BindingContext =  ViewModelLocator.PlatosViewModel;

                ViewModelLocator.PlatosViewModel.hideNav += ViewModel_hideNav;
                ViewModelLocator.PlatosViewModel.setColor += ViewModel_setColor;
                
                drawer.DrawerTransitionType = SideDrawerTransitionType.SlideAlong;
                drawer.DrawerLocation = Telerik.XamarinForms.Primitives.SideDrawer.SideDrawerLocation.Left;
                //NavigationPage.SetTitleIcon(this, "Logoletra.png");
                PlatoList.RefreshRequested += PlatoList_RefreshRequested;
                ViewModelLocator.PlatosViewModel.LoadMenu();

                //idPlato = id;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PlatosViewModel_salirMainMv(object sender, EventArgs e)
        {
            salirMain(null, null);
        }

      

        private void ViewModel_setColor(object sender, EventArgs e)
        {
            setColorMain(null,null);
        }
        
        //private void Mds_GetUsuarioCompleted(object sender, GetUsuarioCompletedEventArgs e)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //private void Mds_GetProveedorPorUsuarioCompleted(object sender, GetProveedorPorUsuarioCompletedEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Result != null)
        //        {
        //            Settings.Provee = e.Result;

        //            //CONTINUO AQUI
        //            IniciarUsuario();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //public void IniciarUsuario()
        //{
        //    if (!String.IsNullOrEmpty(Settings.Provee._id))
        //        PGlobal.IniciarProveedor();
        //    if (!String.IsNullOrEmpty(Settings.User._id))
        //        PGlobal.IniciarUsuario();


        //}

        private void PlatoList_RefreshRequested(object sender, Telerik.XamarinForms.DataControls.ListView.PullToRefreshRequestedEventArgs e)
        {
            //await viewModel.ExecuteGetPlatosCommand();
            ViewModelLocator.PlatosViewModel.GetPlatosCommand.Execute(null);
            PlatoList.EndRefresh();
        }
        
        private void ViewModel_hideNav(object sender, EventArgs e)
        {
            drawer.IsOpen = false;
            drawerList.SelectedItem = null;
        }

        public void SolicitudesClick(object sender, EventArgs e)
        {
            ViewModelLocator.PlatosViewModel.LoadMenu("Mis Solicitudes");
        }

        public void EventosClick(object sender, EventArgs e)
        {
            ViewModelLocator.PlatosViewModel.LoadMenu("Eventos");
        }

        public void OfertasClick(object sender, EventArgs e)
        {
            ViewModelLocator.PlatosViewModel.LoadMenu("Ofertas");
        }

        public async void Like_Click(object sender, EventArgs e)
        {
            try
            {
                Button bt = sender as Button;
                if (!String.IsNullOrEmpty(bt.CommandParameter.ToString()))
                {
                    var id = bt.CommandParameter.ToString();
                    await LikeFo(id);
                }
                else
                {
                    var tt = bt.CommandParameter.ToString();
                }
            }
            catch(Exception ex)
            {

            }
        }

        public async Task LikeFo(string id)
        {
            //Device.BeginInvokeOnMainThread(async () =>
            //{
            //    bool val = await ViewModelLocator.PlatosViewModel.Like(id, Settings.User._id);
            //    if (val)
            //    {
            //        ViewModelLocator.PlatosViewModel.Platos.FirstOrDefault(p => p.CPlato._id == id).CPlato.Valoracion++;
            //        ViewModelLocator.PlatosViewModel.Platos.FirstOrDefault(p => p.CPlato._id == id).LikeImg = false;
            //    }
            //});
        }

        void Usuario_Click(object sender, EventArgs e)
        {
            //Button bt = sender as Button;
            //if (!String.IsNullOrEmpty(bt.CommandParameter.ToString()))
            //{
            //    var id = bt.CommandParameter.ToString();
            //    var foo = ViewModelLocator.PlatosViewModel.Platos.FirstOrDefault(p => p.CPlato._id == id);
            //    if (foo != null)
            //        ViewModelLocator.PlatosViewModel.SelectedPlato = foo;
            //}
        }

        void Mensaje_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (!String.IsNullOrEmpty(bt.CommandParameter.ToString()))
            {
                var id = bt.CommandParameter.ToString();
                //ViewModelLocator.PlatosViewModel.SelectedPlato = ViewModelLocator.PlatosViewModel.Platos.FirstOrDefault(p => p.CPlato._id == id);

            }
            else
            {
                var tt = bt.CommandParameter.ToString();
            }
        }

        public async Task OpenPopUpPedirPlato(string id)
        {
            
            //var ped = new PedidoModalPage(await viewModel.GetFoodLineOne(id));
            //ped.Disappearing += Ped_Disappearing;
            //await this.Navigation.PushModalAsync(ped);
        }

        void AgregarPedido_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (!String.IsNullOrEmpty(bt.CommandParameter.ToString()))
            {
                //var id = bt.CommandParameter.ToString();
                //PedidoModalTipo pt = new PedidoModalTipo(ViewModelLocator.PlatosViewModel.Platos.FirstOrDefault(p => p.CPlato._id == id));
                //this.Navigation.PushPopupAsync(pt);
            }
            else
            {
                var tt = bt.CommandParameter.ToString();
            }
        }

        async void CerrarOferta_Click(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Confirmación", "¿Quieres Cerrar las Ofertas?", "Si", "No");
            if (answer)
            {
                Button bt = sender as Button;
                var id = bt.CommandParameter.ToString();
                await ViewModelLocator.PlatosViewModel.UpdatePlato(id, "Cerrado");
            }
        }

        void Ofertar_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (!String.IsNullOrEmpty(bt.CommandParameter.ToString()))
            {
                //var id = bt.CommandParameter.ToString();
                //var ped = new OfertarPopUp(ViewModelLocator.PlatosViewModel.Platos.FirstOrDefault(p => p.CPlato._id == id));
                
                //this.Navigation.PushPopupAsync(ped);
            }
            else
            {
                var tt = bt.CommandParameter.ToString();
            }
        }

        private void Proveedor_Click(object sender, EventArgs e)
        {
            try
            {
                Button bt = sender as Button;

                if (bt.CommandParameter != null)// es proveedor
                {
                    var win = new ProveedorPage(bt.CommandParameter as Proveedor);
                    this.Navigation.PushModalAsync(win);
                }
            }
            catch (Exception ex)
            {

            }
        }
        
        void AgregarSolicitud_Click(object sender, EventArgs e)
        {
            try
            {
                //this.Navigation.PushModalAsync(new AgregarPlato(AgregarPlato.Modo.Usuario));
                this.Navigation.PushPopupAsync(new SolicitudPopUp());
                //await PopupNavigation.PushAsync(new SolicitudPopUp(), true);
            }
            catch (Exception ex)
            {
            }
        }

        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                if (inicio)
                {
                    ViewModelLocator.PlatosViewModel.IsBusy = true;
                    await LoadUser();
                    inicio = false;
                    ViewModelLocator.PlatosViewModel.LoadMenu();
                    Usuario use = Settings.User;
                    if (String.IsNullOrEmpty(use.Ciudad))
                    {
                        LoginPass lg = new LoginPass();
                        await this.Navigation.PushPopupAsync(lg);
                    }
                    ViewModelLocator.PlatosViewModel.IsBusy = false;
                }
               
                if (idPlato != "")
                {
                    await OpenPopUpPedirPlato(idPlato);
                }
                else
                {
                    if (ViewModelLocator.PlatosViewModel.Platos.Count > 0 )
                        return;

                    //viewModel.GetPlatosCommand.Execute(null);
                    await ViewModelLocator.PlatosViewModel.ExecuteGetPlatosCommand();
                   
                }              
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        public async Task LoadUser()
        {
            try
            {
                if (Settings.User == null)
                {
                    Settings.User = await ViewModelLocator.PlatosViewModel.GetUsuario(Settings.UserName,
                                                                                    Settings.FaceId,
                                                                                    String.IsNullOrEmpty(Settings.UserPass) ? null : Settings.UserPass);
                    //PGlobal.IniciarUsuario();
                    
                    return;
                }

                if (Settings.User != null)
                {
                    if (Settings.User._id == null)
                    {
                        try
                        {
                            Settings.User = await ViewModelLocator.PlatosViewModel.GetUsuario(Settings.UserName, Settings.FaceId, String.IsNullOrEmpty(Settings.UserPass) ? null : Settings.UserPass);
                            //PGlobal.IniciarUsuario();
                            //ViewModelLocator.PlatosViewModel.LoadMenu();
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    if (Settings.User.Tipo == "P")
                    {
                        if (Settings.Provee == null)
                        {
                            Settings.Provee = await ViewModelLocator.PlatosViewModel.GetProveedor(Settings.User._id);
                            //PGlobal.IniciarUsuario();
                            //PGlobal.IniciarProveedor();
                            //ViewModelLocator.PlatosViewModel.LoadMenu();
                        }
                        else
                        {
                            if (Settings.Provee._id == null)
                            {
                                Settings.Provee = await ViewModelLocator.PlatosViewModel.GetProveedor(Settings.User._id);
                                //PGlobal.IniciarUsuario();
                                //PGlobal.IniciarProveedor();
                                //ViewModelLocator.PlatosViewModel.LoadMenu();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        void OnToolbarButtonClick(object sender, EventArgs e)
        {
            drawer.IsOpen = !drawer.IsOpen;
        }

        async void SearchButtonClick(object sender, EventArgs e)
        {
            var search = new SearchModalPage();
            search.close += Search_close;
            await this.Navigation.PushModalAsync(search);
        }

        async void MapButtonClick(object sender, EventArgs e)
        {
            if (!locator)
            {
                MapaRegistro mpr = new MapaRegistro("a");
                //mpr.listalocalizada += Mpr_listalocalizada;
                await this.Navigation.PushAsync(mpr);
            }
            else
            {
                //Geolocator.StopListening();
                //locator = false;
                ////icono blanco
                //ViewModelLocator.PlatosViewModel.la = 0;
                //ViewModelLocator.PlatosViewModel.lo = 0;
                //iconGps.Icon = "ic_room_white_24dp.png";
            }
        }
        async void ProveedoresClick(object sender, EventArgs e)
        {
            await this.Navigation.PushModalAsync(new ProveedoresPage());
        }
        //private void Mpr_listalocalizada(object sender, EventArgs e)
        //{
        //    //Geolocator.StartListening(40000, 10);
        //    //locator = true;
        //    ////icono rojo 
        //    //iconGps.Icon = "ic_room_red_light_24dp.png";
        //}

        private void Search_close(object sender, EventArgs e)
        {
            ViewModelLocator.PlatosViewModel.categoria = sender.ToString();
            ViewModelLocator.PlatosViewModel.GetPlatosCommand.Execute(null);
        }

        public async void Producto_Click(object sender, EventArgs e)
        {
            try
            {
                Button bt = sender as Button;
                if (bt.CommandParameter != null)
                {
                    var plato = bt.CommandParameter as Plato;
                    if (plato.idProv != null )
                    {
                        
                        if (plato.Categoria != "Oferta" && plato.Categoria != "Evento")
                        {
                            var pa = ViewModelLocator.PlatosViewModel.Platos.First(p => p.CProveedor != null && 
                                                                                        p.CProveedor._id == plato.idProv);
                            var pla = pa.Lines.FirstOrDefault(p => p.CPlato._id == plato._id);
                            await this.Navigation.PushModalAsync(new PlatoPage(pla));
                        }
                        else
                        {
                            var pa = ViewModelLocator.PlatosViewModel.Platos.First(p => p.CPlato != null && 
                                                                                        p.CProveedor != null && 
                                                                                        p.CPlato == plato &&
                                                                                        p.CProveedor._id == plato.idProv);
                            var pla = pa.Lines[0];
                            await this.Navigation.PushModalAsync(new PlatoPage(pla));
                        }
                    }
                    else
                    {
                        var ssd = ViewModelLocator.PlatosViewModel.Platos.FirstOrDefault(p => p.Usuario == plato.NombreUsuario);
                        var pla = ssd.Lines[0];
                        await this.Navigation.PushModalAsync(new PlatoPage(pla));
                    }

                   
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void hlc_Click(object sender, EventArgs e)
        {

        }
    }
}
