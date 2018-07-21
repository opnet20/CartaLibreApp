
using App7.Interfaces;
using MvvmHelpers;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using XLabs.Forms.Controls;
//using Xamarin.Forms.Maps;
//xmlns:maps="clr-namespace:Xamarin.Forms.Maps;assembly=Xamarin.Forms.Maps"
using XLabs.Ioc;
using XLabs.Platform.Services.Geolocation;

namespace App7.Views.VistasNavegacion
{
    public partial class MapaRegistro : ContentPage
    {
        private IGeolocator _geolocator;
        double la = 0;
        double lo = 0;
        XLabs.Platform.Services.Geolocation.Position posicion;
        public event EventHandler close;
        //public event EventHandler listalocalizada;
        readonly IDataStore dataProveedor;
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
                    _geolocator.PositionError += _geolocator_PositionError; ;
                    _geolocator.PositionChanged += _geolocator_PositionChanged; ;
                }
                return _geolocator;
            }
        }
       
        private void _geolocator_PositionChanged(object sender, PositionEventArgs e)
        {
            ////			BeginInvokeOnMainThread (() => {
            ////				ListenStatus.Text = e.Position.Timestamp.ToString("G");
            ////				ListenLatitude.Text = "La: " + e.Position.Latitude.ToString("N4");
            ////				ListenLongitude.Text = "Lo: " + e.Position.Longitude.ToString("N4");
            ////			});
            la = e.Position.Latitude;
            lo = e.Position.Longitude;
            posicion = e.Position;

            coordenadasLB.Text = la + "  " + lo;
            
        }

        private void _geolocator_PositionError(object sender, PositionErrorEventArgs e)
        {
            ////			BeginInvokeOnMainThread (() => {
            ////				ListenStatus.Text = e.Error.ToString();
            ////			});
        }

        public MapaRegistro(string t)
        {
            try
            {
                InitializeComponent();
                dataProveedor = DependencyService.Get<IDataStore>();
                Geolocator.StartListening(2000, 0);
                map.MoveToRegion(
                 MapSpan.FromCenterAndRadius(
                  new Xamarin.Forms.GoogleMaps.Position(-12.098957, -77.019766), Distance.FromMiles(100)));

                //var map = new Map(MapSpan.FromCenterAndRadius(new Position(37, -122), Distance.FromMiles(0.3)))
                //{
                //    IsShowingUser = true,
                //    HeightRequest = 100,
                //    WidthRequest = 960,
                //    VerticalOptions = LayoutOptions.FillAndExpand
                //};

                //var stack = new StackLayout { Spacing = 0 };
                //stack.Children.Add(map);
                //Content = stack;

                if (t == "a")
                {
                    GuardarBt.IsVisible = false;
                    //VerBt.IsVisible = true;
                    InitPins();
                    InitMap();
                }
                if (t == "r")
                {
                    GuardarBt.IsVisible = true;
                    //VerBt.IsVisible = false;
                    //InitPins();
                    InitMap();
                }
                if (t == "p")
                {
                    GuardarBt.IsVisible = false;
                    //VerBt.IsVisible = false;
                }
            }
            catch(System.Exception ex)
            {

            }
        }

        public async void InitMap()
        {
            
            map.SelectedPinChanged += Map_SelectedPinChanged;
            map.PinClicked += Map_PinClicked;
            map.InfoWindowClicked += Map_InfoWindowClicked;
            var posit = await Geolocator.GetPositionAsync(10000);
            map.MoveToRegion(
              MapSpan.FromCenterAndRadius(
                  new Xamarin.Forms.GoogleMaps.Position(posit.Latitude, posit.Longitude), Distance.FromMiles(1)));

        }

        private void Map_InfoWindowClicked(object sender, InfoWindowClickedEventArgs e)
        {
            try
            {
                var proveedor = e.Pin.Tag as App7.ServiceReference1.Proveedor;
                this.Navigation.PushAsync(new ProveedorPage(proveedor));
            }
            catch(System.Exception ex)
            {
            }
        }

        private void Map_PinClicked(object sender, PinClickedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Map_SelectedPinChanged(object sender, SelectedPinChangedEventArgs e)
        {
           //this.Navigation.PushAsync(new ProveedorPage(e.SelectedPin.Tag as ServiceReference1.Proveedor));
        }

        public void InitOnePin(App7.ServiceReference1.Proveedor p)
        {
            var pinTokyo = new Pin()
            {
                Type = PinType.Generic,
                Label = (p.Nombre == null) ? "" : p.Nombre,
                Address = (p.Direccion == null) ? "" : p.Direccion,
                Position = new Xamarin.Forms.GoogleMaps.Position(p.Latitude, p.Longitude),
                IsVisible = true, 
                Tag = p,
                Flat = false
            };
            var conv = new ContentView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 60,
                WidthRequest = 60,
            };

            var image = new CircleImage
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Xamarin.Forms.Color.White,
                Aspect = Aspect.AspectFill,
                Source = ImageSource.FromUri(p.ImageUri)
            };

            var grid = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection(),
                HeightRequest = 160,
                WidthRequest = 160
            };
            grid.ColumnDefinitions = new ColumnDefinitionCollection();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 60 });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 60 });

            grid.RowDefinitions = new RowDefinitionCollection();
            grid.RowDefinitions.Add(new RowDefinition { Height = 60 });
            grid.RowDefinitions.Add(new RowDefinition { Height = 60 });


            grid.Children.Add(image);

            conv.Content = grid;

            pinTokyo.Icon = BitmapDescriptorFactory.FromView(conv);
            map.Pins.Add(pinTokyo);

            map.MoveToRegion(
            MapSpan.FromCenterAndRadius(
                new Xamarin.Forms.GoogleMaps.Position(p.Latitude, p.Longitude), Distance.FromMiles(1)));
        }

        public async void InitPins()
        {
            var proveedores = await dataProveedor.GetProveedores(Settings.User.Ciudad,"");

            foreach (var i in proveedores)
            {
                if (i.Location != null && i.Location.coordinates[0] != 0)
                {
                    try
                    {
                        //var assembly = typeof(MapaRegistro).GetTypeInfo().Assembly;
                        //var stream = assembly.GetManifestResourceStream("Food.png");

                        //var url = new URL(i.ImageUri.ToString());
                        //var MyHttpRequest = (HttpWebRequest)HttpWebRequest.Create(i.ImageUri);
                        //using (HttpClient client = new HttpClient())
                        //{
                        //    Stream stream = await client.GetStreamAsync(i.ImageUri.AbsoluteUri);
                        //}

                        //var request = HttpWebRequest.Create(i.ImageUri);
                        //request.Method = "GET";
                        //var result = (IAsyncResult)request.BeginGetResponse(ResponseCallback, request);
                        
                        var pinTokyo = new Pin()
                        {
                            Type = PinType.Generic,
                            Label = (i.Nombre == null) ? "" : i.Nombre,
                            Address = (i.Direccion == null) ? "" : i.Direccion,
                            Position = new Xamarin.Forms.GoogleMaps.Position(i.Location.coordinates[0], i.Location.coordinates[1]),
                            IsVisible = true,
                            Tag = i, 
                            
                            Flat = false,
                            
                        };
                        //var conv = new ContentView
                        //{
                        //    HorizontalOptions = LayoutOptions.FillAndExpand,
                        //    VerticalOptions = LayoutOptions.FillAndExpand,
                        //    HeightRequest = 60,
                        //    WidthRequest = 60,
                        //};

                        //var image = new CircleImage
                        //{
                        //    HorizontalOptions = LayoutOptions.FillAndExpand,
                        //    VerticalOptions = LayoutOptions.FillAndExpand,
                        //    BackgroundColor = Xamarin.Forms.Color.White,
                        //    Aspect = Aspect.AspectFill,
                        //    Source = ImageSource.FromUri(i.ImageUri)
                        //};

                        //var grid = new Grid
                        //{
                        //    ColumnDefinitions = new ColumnDefinitionCollection(),    
                        //    HeightRequest = 160,
                        //    WidthRequest = 160
                        //};
                        //grid.ColumnDefinitions = new ColumnDefinitionCollection();
                        //grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 60 });
                        //grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 60 });

                        //grid.RowDefinitions = new RowDefinitionCollection();
                        //grid.RowDefinitions.Add(new RowDefinition { Height = 60 });
                        //grid.RowDefinitions.Add(new RowDefinition { Height = 60 });


                        //grid.Children.Add(image);

                        //conv.Content = grid;
                        //pinTokyo.Icon = 
                        //pinTokyo.Icon = BitmapDescriptorFactory.FromView(conv);

                        map.Pins.Add(pinTokyo);

                     
                    }
                    catch (System.Exception ex)
                    {

                    }
                }
            }
        }

        //static UIImage xFromUrl(string uri)
        //{
        //    using (var url = new NSUrl(uri))
        //    using (var data = NSData.FromUrl(url))
        //        return UIImage.LoadFromData(data);
        //}

        private void ResponseCallback(IAsyncResult result)
        {
            try
            {
                var request = (HttpWebRequest)result.AsyncState;
                var response = request.EndGetResponse(result);

                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    var pinTokyo = new Pin()
                    {
                        Type = PinType.Place,
                        Label = "sdf",
                        Address = "sdf",
                        Position = new Xamarin.Forms.GoogleMaps.Position(0,0),
                        IsVisible = true,
                    };
                    string contents = reader.ReadToEnd();
                    map.Pins.Add(pinTokyo);
                    
                   // ParseJson(contents);
                }
            }
            catch (System.Exception ex)
            {
                //if (RequestError != null)
                //{
                //    //Raise Sync Error (Custom Exception during response call back)
                //    RequestError(this, new RequestEventArgs()
                //    {
                //        IsError = true,
                //        ErrorMessage = Constants.REQUEST_CALLBACK_EXCEPTION,
                //        Exception = ex
                //    });
                //}
            }
        }
        //public async void Ver_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        listalocalizada(this.posicion, null);
        //        await this.Navigation.PopAsync();
        //    }
        //    catch (Java.Lang.Exception ex)
        //    {
        //        await this.DisplayAlert("Error", ex.Message, "OK");
        //    }
        //}
        public async void Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                close(this.posicion, null);
                await this.Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Error", ex.Message, "OK");
            }
        }

      
    }

}
