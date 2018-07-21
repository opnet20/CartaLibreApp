using App7.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App7.ServiceReference1;
using App7.Views.VistasNavegacion;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using FormsToolkit;
using Xamarin.Netflix.ViewModels.Base;
using Plugin.ExternalMaps;
using Plugin.Messaging;

namespace App7.Views
{
    public partial class ProveedorPage : ContentPage
    {
        ProveedorViewModel viewModel;
        private const int ParallaxSpeed = 5;

        private double _lastScroll;

        public ProveedorPage(App7.ServiceReference1.Proveedor prov)
        {
            try
            {
                InitializeComponent();
                //imageLoad.IsVisible = true;
                //imageLoad.IsRunning = true;
                flowList.FlowItemTapped += FlowList_FlowItemTapped;
                verMapaBt.IsEnabled = false;
                verMapaStack.IsVisible = false;
                
                viewModel = new ProveedorViewModel(prov, this);
                viewModel.SetHeight += ViewModel_SetHeight;
                BindingContext = viewModel;
                if (prov != null && prov.Latitude == 0 && prov.Longitude == 0)
                {
                    verMapaBt.IsEnabled = false;
                    verMapaStack.IsVisible = false;
                }
                else
                {
                    verMapaBt.IsEnabled = true;
                    verMapaStack.IsVisible = true;
                }
                if (String.IsNullOrEmpty(prov.NumeroTelefono))
                {
                    llamarStack.IsVisible = false;
                }
                else
                {
                    llamarStack.IsVisible = true;
                }
                Load();
            }
            catch(Exception ex)
            {

            }
        }

        private void FlowList_FlowItemTapped(object sender, ItemTappedEventArgs e)
        {
            viewModel.PlatoSelecc = flowList.FlowLastTappedItem as Plato;
            if (viewModel.PlatoSelecc != null)
            {
                viewModel.LoadPlatoPage(viewModel.PlatoSelecc._id);
            }
        }

        public async void Load()
        {
            try
            {
                bool soyse = await this.viewModel.dataWCF.SoySeguidor(Settings.User._id, this.viewModel.Proveedor._id);
                //seguirBt.IsVisible = true;
                if (!soyse)
                {
                    likeImg.Source = "star.png";
                    //likeImg.Aspect = Aspect.AspectFit;
                    seguirLb.Text = "Seguir";
                }
                else
                {
                    likeImg.Source = "minus.png";
                    //likeImg.Aspect = Aspect.AspectFit;
                    seguirLb.Text = "No seguir";
                }

                var num = await this.viewModel.dataWCF.NumSeguidores(this.viewModel.Proveedor._id);
                numSeg.Text = num.ToString();
            }
            catch (Exception ex)
            {

            }
        }


        private void ViewModel_SetHeight(object sender, EventArgs e)
        {
            int val = 250;
            var dod = Math.Ceiling(Convert.ToDouble(sender) / 2);
            var list = dod * val;

            gridList.HeightRequest = 560 + list;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var navigationPage = Parent as Xamarin.Forms.NavigationPage;

            if (navigationPage != null)
                navigationPage.On<iOS>().EnableTranslucentNavigationBar();

            MessagingService.Current.SendMessage(MessageKeys.ChangeToolbar, false);
            MessagingService.Current.SendMessage(MessageKeys.ToolbarColor, Color.Transparent);

            ParallaxScroll.Scrolled += OnParallaxScrollScrolled;

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            var navigationPage = Parent as Xamarin.Forms.NavigationPage;

            if (navigationPage != null)
                navigationPage.On<iOS>().DisableTranslucentNavigationBar();

            MessagingService.Current.SendMessage(MessageKeys.ChangeToolbar, true);
            MessagingService.Current.SendMessage(MessageKeys.ToolbarColor, Color.Black);

            ParallaxScroll.Scrolled -= OnParallaxScrollScrolled;
        }

        private void OnParallaxScrollScrolled(object sender, ScrolledEventArgs e)
        {
            double translation = 0;

            if (_lastScroll < e.ScrollY)
            {
                translation = 0 - ((e.ScrollY / 2));

                if (translation > 0) translation = 0;
            }
            else
            {
                translation = 0 + ((e.ScrollY / 2));

                if (translation > 0) translation = 0;
            }

            HeaderPanel.TranslateTo(HeaderPanel.TranslationX, translation);
            _lastScroll = e.ScrollY;
        }

        void AgregarPedido_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (!String.IsNullOrEmpty(bt.CommandParameter.ToString()))
            {
                var id = bt.CommandParameter.ToString();
                FoodLine fl = new FoodLine { CPlato = viewModel.Carta.FirstOrDefault(p => p._id == id), CProveedor = viewModel.Proveedor };
                var ped = new PedidoModalTipo(fl);
                this.Navigation.PushPopupAsync(ped);
            }
            else
            {
                var tt = bt.CommandParameter.ToString();
            }
        }

        public void Llamar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.viewModel.Proveedor.NumeroTelefono))
            {
                var phoneCallTask = MessagingPlugin.PhoneDialer;
                if (phoneCallTask.CanMakePhoneCall)
                    phoneCallTask.MakePhoneCall(this.viewModel.Proveedor.NumeroTelefono);
            }
        }

        public void Cate_Click(object sender, EventArgs e)
        {

        }
        public void VerMapa_Click(object sender, EventArgs e)
        {
            //MapaRegistro mp = new MapaRegistro("p");
            //mp.InitOnePin(this.viewModel.Proveedor);
            //await this.Navigation.PushModalAsync(mp);                                                                           
            CrossExternalMaps.Current.NavigateTo(this.viewModel.Proveedor.Nombre, this.viewModel.Proveedor.Latitude, this.viewModel.Proveedor.Longitude);
        }



        public async void Seguir_Click(object sender, EventArgs e)
        {
            try
            {
                seguirBt2.IsEnabled = false;
                if (seguirLb.Text == "Seguir")
                {
                    Seguidores se = new Seguidores
                    {
                        idProv = this.viewModel.Proveedor._id,
                        idUsuario = Settings.User._id,
                        Fecha = DateTime.Now,
                        IdPlayer = Settings.User.IdPlayer
                    };

                    await this.viewModel.dataWCF.SetSeguidor(se);
                    seguirBt2.IsEnabled = true;
                    seguirLb.Text = "No seguir";
                    //likeImg.Source = "http://cartalibre.com/img/minus.png";
                    likeImg.Source = "minus.png";
                    likeImg.Aspect = Aspect.AspectFit;
                    likeImg.FadeAnimationEnabled = true;
                    likeImg.DownsampleToViewSize = true;
                    likeImg.LoadingDelay = 50;
                    
                    int num = int.Parse(numSeg.Text);
                    num++;
                    numSeg.Text = num.ToString();
                    return;
                }

                if (seguirLb.Text == "No seguir")
                {
                    await this.viewModel.dataWCF.DeleteSeguidor(Settings.User._id, this.viewModel.Proveedor._id);
                    seguirBt2.IsEnabled = true;
                    seguirLb.Text = "Seguir";
                    //likeImg.Source = "http://cartalibre.com/img/star.png";
                    likeImg.Source = "star.png";
                    likeImg.FadeAnimationEnabled = true;
                    likeImg.DownsampleToViewSize = true;
                    
                    likeImg.Aspect = Aspect.AspectFit;
                    likeImg.LoadingDelay = 50;
                    int num = int.Parse(numSeg.Text);
                    num--;
                    numSeg.Text = num.ToString();
                    return;
                }
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
