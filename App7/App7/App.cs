using App7.Model;
using App7.ServiceReference1;
using App7.Services;
using App7.ViewModel;
using App7.ViewModel.Proveedores;
using App7.Views;
using App7.Views.Proveedores;
using Com.OneSignal;
using Com.OneSignal.Abstractions;
using KickassUI.Spotify.Controls;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App7
{
    public class Conection
    {
        BasicHttpBinding BindingTemp = new BasicHttpBinding();
        

        public Conection()
        {
            
        }
    }

    public static class ViewModelLocator
    {
      
        public static MiCartaViewModel cartaViewModel;
        
        public static MisPedidosViewModel PedidosViewModel;

        public static PedidoViewModel PedidoUsuarioViewModel;

        public static PlatosViewModel PlatosViewModel;

    }
    
    public class PGlobal
    {
        //Soy un usuario 
        //public static Usuario MyUser;
        
        //Si tb soy proveedor P
        //public static Proveedor MyProv = new Proveedor {  };

        public static string accountURL = @"https://cartalibreddb.documents.azure.com:443/";
        public static string accountKey = @"lFeslLrkHSmY1GdeJkjCwHa4kuAdXKpdJwOlM9deHgKrLTUEpwxsORPeH7yacW7INjPBRk9qxgf0qvC0UJlqRg==";
        public static string databaseId = @"cartalibre";
        //public static List<ItemLista> categorias = new List<ItemLista>
        //{
        //    new ItemLista { Titulo = "Vehículos", Categoria = "Vehículos", Icono = "http://cartalibre.com/images/menu.jpg"},
        //    new ItemLista { Titulo = "Propiedades" , Categoria = "Propiedades",Icono = "http://cartalibre.com/images/tradicional.jpeg"},

        //    new ItemLista { Titulo = "Juguetes", Categoria = "Juguetes" ,Icono = "http://cartalibre.com/images/desayuno.jpg"},
        //    new ItemLista { Titulo = "Bebes", Categoria = "Bebes", Icono = "http://cartalibre.com/images/helados.jpg"},

        //    new ItemLista { Titulo = "Electrónicos", Categoria =  "Electrónicos",Icono = "http://cartalibre.com/images/parrilla.jpg"},
        //    new ItemLista { Titulo = "Móviles", Categoria = "Móviles" ,Icono = "http://cartalibre.com/images/pastas.jpg"},

        //    new ItemLista { Titulo = "Hogar-Muebles", Categoria = "Hogar-Muebles", Icono = "http://cartalibre.com/images/arequipena.jpg"},
        //    new ItemLista { Titulo = "Ropa-Moda" , Categoria = "Ropa-Moda",Icono = "http://cartalibre.com/images/aves.jpg"},

        //    new ItemLista { Titulo = "Belleza", Categoria = "Belleza", Icono = "http://cartalibre.com/images/helados.jpg"},
        //    new ItemLista { Titulo = "Zapatos", Categoria = "Zapatos", Icono = "http://cartalibre.com/images/helados.jpg"},

        //    new ItemLista { Titulo = "Deportes", Categoria =  "Deportes",Icono = "http://cartalibre.com/images/bebidas.jpg"},
        //    new ItemLista { Titulo = "Animales", Categoria = "Animales" ,Icono = "http://cartalibre.com/images/cafe.jpg"},

        //    new ItemLista { Titulo = "Servicios", Categoria = "Servicios", Icono = "http://cartalibre.com/images/ceviche.jpg"},
        //    new ItemLista { Titulo = "Musica" , Categoria = "Musica",Icono = "http://cartalibre.com/images/chifa.jpg"},

        //    new ItemLista { Titulo = "Comida", Categoria =  "Comida",Icono = "http://cartalibre.com/images/criolla.jpg"},
        //    new ItemLista { Titulo = "Bodega", Categoria = "Bodega", Icono = "http://cartalibre.com/images/helados.jpg"},

        //    new ItemLista { Titulo = "Accesorios", Categoria = "Accesorios", Icono = "http://cartalibre.com/images/helados.jpg"},
        //    new ItemLista { Titulo = "Diversión", Categoria = "Diversión", Icono = "http://cartalibre.com/images/helados.jpg"},
        //};

        public static List<ItemLista> categorias = new List<ItemLista>
        {
            new ItemLista { Titulo = "Menú", Categoria = "Menú", Icono = "http://cartalibre.com/images/menu.jpg"},
            new ItemLista { Titulo = "Tradicional" , Categoria = "Tradicional",Icono = "http://cartalibre.com/images/tradicional.jpeg"},
            new ItemLista { Titulo = "Carnes", Categoria =  "Carnes",Icono = "http://cartalibre.com/images/parrilla.jpg"},
            new ItemLista { Titulo = "Pastas", Categoria = "Pastas" ,Icono = "http://cartalibre.com/images/pastas.jpg"},
             new ItemLista { Titulo = "Arequipeña", Categoria = "Arequipeña", Icono = "http://cartalibre.com/images/arequipena.jpg"},
            new ItemLista { Titulo = "Aves" , Categoria = "Aves",Icono = "http://cartalibre.com/images/aves.jpg"},
            new ItemLista { Titulo = "Bebidas", Categoria =  "Bebidas",Icono = "http://cartalibre.com/images/bebidas.jpg"},
            new ItemLista { Titulo = "Café", Categoria = "Café" ,Icono = "http://cartalibre.com/images/cafe.jpg"},
             new ItemLista { Titulo = "Ceviche", Categoria = "Ceviche", Icono = "http://cartalibre.com/images/ceviche.jpg"},
            new ItemLista { Titulo = "Chifa" , Categoria = "Chifa",Icono = "http://cartalibre.com/images/chifa.jpg"},
            new ItemLista { Titulo = "Criolla", Categoria =  "Criolla",Icono = "http://cartalibre.com/images/criolla.jpg"},
            new ItemLista { Titulo = "Desayuno", Categoria = "Desayuno" ,Icono = "http://cartalibre.com/images/desayuno.jpg"},
             new ItemLista { Titulo = "Helados", Categoria = "Helado", Icono = "http://cartalibre.com/images/helados.jpg"},
            new ItemLista { Titulo = "Internacional" , Categoria = "Internacional",Icono = "http://cartalibre.com/images/internacional.jpg"},
            new ItemLista { Titulo = "Japonesa", Categoria =  "Japonesa",Icono = "http://cartalibre.com/images/japonesa.jpg"},
            new ItemLista { Titulo = "Marina", Categoria = "Marina" ,Icono = "http://cartalibre.com/images/marina.jpg"},
            new ItemLista { Titulo = "Mexicana", Categoria = "Mexicana" ,Icono = "http://cartalibre.com/images/mexicana.jpg"},
            new ItemLista { Titulo = "Pasteleria", Categoria = "Pasteleria" ,Icono = "http://cartalibre.com/images/pasteleria.jpg"},
            new ItemLista { Titulo = "Pizzas", Categoria = "Pizzas" ,Icono = "http://cartalibre.com/images/pizzas.jpg"},
            new ItemLista { Titulo = "Rapida", Categoria = "Rapida" ,Icono = "http://cartalibre.com/images/rapida.jpg"},
            new ItemLista { Titulo = "Sanguches", Categoria = "Sanguches" ,Icono = "http://cartalibre.com/images/sanguches.jpg"},
            new ItemLista { Titulo = "Vegetariano", Categoria = "Vegetariano" ,Icono = "http://cartalibre.com/images/vegetariano.jpg"}
        };




        //public static void IniciarUsuario()
        //{
        //     ViewModelLocator.PedidoUsuarioViewModel = new PedidoViewModel(null);
        //}
        //public static void IniciarProveedor()
        //{
        //    //ViewModelLocator.cartaViewModel = new MiCartaViewModel(null);
        //    ViewModelLocator.PedidosViewModel = new MisPedidosViewModel(null);
        //}

    }

    public class App : Application
    {
        bool inicio = false;
        
        public enum Notificacion
        {
            PedidosUsuario,
            PedidosProveedor,
            Plato,
            Ninguno
        }

        public App()
        {
            try
            {
                inicio = true;
                
                string usern = Settings.UserName;
                OneSignal.Current.StartInit("855e791f-bcc6-4af3-a9e2-7e8aca4fed98")
                    .Settings(new Dictionary<string, bool>() {
                        { IOSSettings.kOSSettingsKeyAutoPrompt, false },
                        { IOSSettings.kOSSettingsKeyInAppLaunchURL, true } })
                        .InFocusDisplaying(OSInFocusDisplayOption.Notification)
                        .HandleNotificationOpened((result) =>
                        {
                            try
                            {
                                result.action = new OSNotificationAction();
                                //var sss = "HandleNotificationOpened: {0}" + result.notification.payload.body;
                                if (result.notification.payload.body.StartsWith("Oferta"))
                                {
                                    Settings.SemIni2 = "Oferta";
                                }
                                if (result.notification.payload.body.StartsWith("Pedido"))
                                {
                                    Settings.SemIni2 = "PedidoProveedor";
                                    //ShowMainWin("", Notificacion.PedidosProveedor);
                                    return;
                                }
                                if (result.notification.payload.body.StartsWith("Nuevo"))
                                {
                                    string id = result.notification.payload.additionalData.ElementAt(0).Value.ToString();
                                    //Uri uri = new Uri(result.notification.payload.bigPicture);
                                    //string id = System.IO.Path.GetFileNameWithoutExtension(uri.LocalPath);
                                    try
                                    {
                                        Settings.SemIni2 = "Nuevo:"+id;
                                    }
                                    catch (Exception ex)
                                    {
                                        Settings.SemIni2 = string.Empty;
                                    }
                                    return;
                                }
                                if (result.notification.payload.body.Contains("Pedido Confirmado") || result.notification.payload.body.Contains("Pedido Rechazado"))
                                {
                                    Settings.SemIni2 = "PedidoUsuario";
                                    return;
                                }
                                if (result.notification.payload.body.Contains("Pedido Aceptado") || result.notification.payload.body.Contains("Pedido Declinado"))
                                {
                                    Settings.SemIni2 = "PedidoProveedor";
                                    return;
                                }
                            }
                            catch(Exception ex)
                            {

                            }
                        })
                        .HandleNotificationReceived((notification) =>
                        {
                          
                        })
                       .EndInit();
 
                if (usern == "")
                {
                    var login = new Login();
                    login.loginEvent += Login_loginEvent;
                    MainPage = login;
                }
                else
                {
                    ShowMainWin();
                }



            }
            catch(Exception ex)
            {
                
            }
            
        }

        public async Task ShowMainWin()
        {
            try
            {
                string id = "";
                
                var platos = new PlatosPage();
                platos.setColorMain += Platos_setColorMain;
                platos.salirMain += Platos_salirMain;
                var nav = new NavigationPage(platos)
                {
                    BarTextColor = Color.White,
                    BackgroundColor = GetColorBack(),
                };

                MainPage = nav;
                
                //Ventanas de notificaciones//
                if (Settings.SemIni2 == "Oferta")
                    await nav.PushAsync(new PedidoPage(1));
                if (Settings.SemIni2.StartsWith("Nuevo"))
                {
                    id = Settings.SemIni2.Substring(6);
                    if (id.Length > 10)
                        ViewModelLocator.PlatosViewModel.SelectedPlato = await ViewModelLocator.PlatosViewModel.GetFoodLineOne(id);
                    else
                        ViewModelLocator.PlatosViewModel.SelectedPlato = await ViewModelLocator.PlatosViewModel.GetFoodLineOne(id);
                }
                if (Settings.SemIni2 == "PedidoProveedor")
                    await nav.PushAsync(new TiendaPage());

                if (Settings.SemIni2 == "PedidoUsuario")
                    await nav.PushAsync(new PedidoPage(0));

                Settings.SemIni2 = string.Empty;
                //////////////////////////////
            }
            catch(Exception ex)
            {
                Settings.SemIni2 = string.Empty;
            }
        }

        private void Platos_salirMain(object sender, EventArgs e)
        {
            Settings.UserName = "";
            Settings.UserPass = "";
            Settings.UserTipo = "";
            Settings.FaceId = "";
            Settings.SemIni2 = "";
            Settings.SomeInt = 0;
            Settings.User = null;
            Settings.Provee = null;

            var login = new Login();
            login.loginEvent += Login_loginEvent;
            MainPage = login;
        }



        //private static void HandleNotificationOpened(OSNotificationOpenedResult result)
        //{
        //    OSNotificationPayload payload = result.notification.payload;
        //    Dictionary<string, string> additionalData = payload.additionalData;
        //    string message = payload.body;
        //    string actionID = result.action.actionID;

        //    print("GameControllerExample:HandleNotificationOpened: " + message);
        //    extraMessage = "Notification opened with text: " + message;

        //    if (additionalData != null)
        //    {
        //        if (additionalData.ContainsKey("discount"))
        //        {
        //            extraMessage = (string)additionalData["discount"];
        //            // Take user to your store.
        //        }
        //    }
        //    if (actionID != null)
        //    {
        //        // actionSelected equals the id on the button the user pressed.
        //        // actionSelected will equal "__DEFAULT__" when the notification itself was tapped when buttons were present.
        //        extraMessage = "Pressed ButtonId: " + actionID;
        //    }
        //}

        private void Platos_setColorMain(object sender, EventArgs e)
        {
            this.MainPage.BackgroundColor = GetColorBack();
        }

        private void Login_loginEvent(object sender, EventArgs e)
        {
            var platos = new PlatosPage();
            platos.setColorMain += Platos_setColorMain;
            platos.salirMain += Platos_salirMain;
            MainPage = new NavigationPage(platos)
            {
                BarTextColor = Color.White,
                BackgroundColor = GetColorBack(),
            };
        }

        private Color GetColorBack()
        {
            var tht = Settings.UserTipo;
            var color = Color.FromHex("#EF4F50");
            if (tht == "P")
                color = Color.FromHex("#fc8a25");

            return color;
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            
        }

        public static byte[] ResizeImageAndroid(byte[] imageData, float width, float height)
        {
            /*
            // Load the bitmap 
            Android.Graphics.Bitmap originalImage = Android.Graphics.BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            //
            float ZielHoehe = 0;
            float ZielBreite = 0;
            //
            var Hoehe = originalImage.Height;
            var Breite = originalImage.Width;
            //
            if (Hoehe > Breite) // Höhe (71 für Avatar) ist Master
            {
                ZielHoehe = height;
                float teiler = Hoehe / height;
                ZielBreite = Breite / teiler;
            }
            else // Breite (61 für Avatar) ist Master
            {
                ZielBreite = width;
                float teiler = Breite / width;
                ZielHoehe = Hoehe / teiler;
            }
            //
            Android.Graphics.Bitmap resizedImage = Android.Graphics.Bitmap.CreateScaledBitmap(originalImage, (int)ZielBreite, (int)ZielHoehe, false);
            // 
            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Android.Graphics.Bitmap.CompressFormat.Jpeg, 100, ms);
                return ms.ToArray();
            }
            */
            return null;
        }
    }
}
