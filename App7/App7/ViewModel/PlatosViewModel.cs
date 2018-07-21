using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using App7.Interfaces;
using App7.Views;
using App7.ServiceReference1;
using App7.Views.Proveedores;
using MvvmHelpers;
using App7.Model;
using System.Windows.Input;

namespace App7.ViewModel
{
    public class PlatosViewModel : ViewModelBase
    {
        #region Variables
        readonly IDataStore dataPlato;
        ObservableCollection<FoodLineCom> platos;
        public bool ForceSync { get; set; }
        ObservableRangeCollection<ImagenSrc> enlaces;
        public event EventHandler hideNav;
        public event EventHandler setColor;
        public event EventHandler salirMainMv;
        public bool platoAbierto = false;
        ImagenSrc enlace;
        FoodLine selectedPlato;
        public string categoria;
       
        #endregion

        #region Propiedades
        public ObservableCollection<FoodLineCom> Platos
        {
            get
            {
                return platos;
            }
            set
            {
                platos = value;
                OnPropertyChanged("Platos");
            }
        }
        public ObservableRangeCollection<ImagenSrc> Enlaces
        {
            get
            {
                return enlaces;
            }

            set
            {
                enlaces = value;
                OnPropertyChanged("Enlaces");
            }
        }
        public ImagenSrc Enlace
        {
            get
            {
                return enlace;
            }
            set
            {
                enlace = value;
                if (enlace != null)
                {
                    LoadMenu(enlace.Name);
                }
            }
        }

        public async void LoadMenu(string Name)
        {
            if (Name == "Mi Perfil")
            {
                var registro = new RegistroUsuarioPage(Settings.User);
                registro.close += Registro_close;
                await page.Navigation.PushAsync(registro);
            };
            if (Name == "Proveedores")
            {
                await page.Navigation.PushModalAsync(new ProveedoresPage());
            };
            if (Name == "Mis Pedidos")
            {
                await page.Navigation.PushAsync(new PedidoPage(0));
            };
            if (Name == "Mi Carta")
            {
                var ss = new TiendaPage();

                await page.Navigation.PushAsync(new TiendaPage());
            };
            //if (Name == "Pedidos de clientes")
            //{
            //    await page.Navigation.PushAsync(new MisPedidosPage());
            //};
            if (Name == "Perfil Proveedor")
            {
                var vp = new RegistroProveedorPage(Settings.Provee);
                vp.close += Vp_close;
                await page.Navigation.PushAsync(vp);
            };
            if (Name == "¡Quiero Ser Proveedor!")
            {
                var vp = new RegistroProveedorPage(new Proveedor { IdUsuario = Settings.User._id.ToString() });
                vp.close += Vp_close;
                await page.Navigation.PushAsync(vp);
            };

            if (Name == "Ofertas")
            {
                this.categoria = "Oferta";
                GetPlatosCommand.Execute(null);
            };

            if (Name == "Mi Actividad")
            {
                this.categoria = "Mis Platos";
                GetPlatosCommand.Execute(null);
            };

            if (Name == "Todas las Solicitudes")
            {
                this.categoria = "Solicitudes";
                GetPlatosCommand.Execute(null);
            };
            if (Name == "Mis Solicitudes")
            {
                this.categoria = "Mis Solicitudes";
                GetPlatosCommand.Execute(null);
            };
            if (Name == "Eventos")
            {
                this.categoria = "Evento";
                GetPlatosCommand.Execute(null);
            };
            if (Name == "Salir")
            {
                salirMainMv(null, null);
            }; if (Name == "Inicio")
            {
                this.categoria = "";
                GetPlatosCommand.Execute(null);
            };
            hideNav(null, null);
        }

        private void Registro_close(object sender, EventArgs e)
        {
            
        }

        public FoodLine SelectedPlato
        {
            get { return selectedPlato; }
            set
            {
                try
                {
                    selectedPlato = value;
                    OnPropertyChanged("SelectedPlato");

                    if (selectedPlato == null)
                        return;

                    //if (ItemSelected == null)
                    //{
                    try
                    {
                        //page.Navigation.PushModalAsync(new PlatoPage(selectedPlato));
                        //SelectedPlato = null;
                    }
                    catch (Exception ex)
                    {

                    }
                    //}
                    //else
                    //{
                    //    ItemSelected.Invoke(selectedPlato);
                    //}
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        //public Action<FoodLine> ItemSelected { get; set; }
        #endregion

        #region Metodos

        public PlatosViewModel(Page page) : base(page)
        {
            Title = "Carta Libre";
            //NavigationPage.SetTitleIcon(this, "Logoletra.png");
            dataPlato = DependencyService.Get<IDataStore>();
            Platos = new ObservableCollection<FoodLineCom>();
            //PlatosGrouped = new ObservableRangeCollection<Grouping<string, Plato>>();
            categoria = "";
            Grid fr = new Grid();
            Image im = new Image();
        }

        public void SetPage(Page _page)
        {
            this.page = _page;
        }

        private void Vp_close(object sender, EventArgs e)
        {
            LoadMenu();
            setColor(null,null);
        }

        public void LoadMenu()
        {
            this.Enlaces = new ObservableRangeCollection<ImagenSrc>();

            if (Settings.User.Tipo == "P")
            {
                Enlaces.Add(new ImagenSrc { Name = "Inicio", Src = "ic_home_red_light_24dp.png" });
                //Enlaces.Add(new ImagenSrc { Name = "Mi Actividad", Src = "ic_message_red_light_24dp.png" });
                //Enlaces.Add(new ImagenSrc { Name = "Todas las Solicitudes", Src = "ic_group_red_light_24dp.png" });
                
                //Enlaces.Add(new ImagenSrc { Name = "Mis Solicitudes", Src = "ic_record_voice_over_red_light_24dp.png" });
                //Enlaces.Add(new ImagenSrc { Name = "Eventos", Src = "ic_event_available_red_light_24dp.png" });
                //Enlaces.Add(new ImagenSrc { Name = "Ofertas", Src = "ic_monetization_on_red_light_24dp.png" });

                //Enlaces.Add(new ImagenSrc { Src = "ic_store_red_light_24dp.png", Name = "Proveedores" });

                Enlaces.Add(new ImagenSrc { Src = "ic_add_shopping_cart_red_light_24dp.png", Name = "Mis Pedidos" });
                //Enlaces.Add(new ImagenSrc { Src = "ic_description_red_light_24dp.png", Name = "Pedidos de clientes" });
                Enlaces.Add(new ImagenSrc { Src = "ic_find_in_page_red_light_24dp.png", Name = "Mi Carta" });
                Enlaces.Add(new ImagenSrc { Src = "ic_home_red_light_24dp.png", Name = "Perfil Proveedor" });
                Enlaces.Add(new ImagenSrc { Src = "ic_account_circle_red_light_24dp.png", Name = "Mi Perfil" });
            }
            if (Settings.User.Tipo == "U")
            {
                //Enlaces.Add(new ImagenSrc { Name = "Mis Solicitudes", Src = "ic_record_voice_over_red_light_24dp.png" });
                //Enlaces.Add(new ImagenSrc { Name = "Eventos", Src = "ic_event_available_red_light_24dp.png" });
                //Enlaces.Add(new ImagenSrc { Name = "Ofertas", Src = "ic_monetization_on_red_light_24dp.png" });
                Enlaces.Add(new ImagenSrc { Name = "Inicio", Src = "ic_home_red_light_24dp.png" });
                Enlaces.Add(new ImagenSrc { Src = "ic_add_shopping_cart_red_light_24dp.png", Name = "Mis Pedidos" });
                //Enlaces.Add(new ImagenSrc { Src = "ic_store_red_light_24dp.png", Name = "Proveedores" });
                Enlaces.Add(new ImagenSrc { Src = "ic_account_circle_red_light_24dp.png", Name = "Mi Perfil" });
                Enlaces.Add(new ImagenSrc { Name = "¡Quiero Ser Proveedor!", Src = "ic_local_library_red_light_24dp.png" });

            }

            Enlaces.Add(new ImagenSrc { Name = "Salir", Src = "ic_highlight_off_red_light_24dp.png" });
        }

        public async Task<bool> Like(string idplato, string idusuario)
        {
            return await dataPlato.SetLike(idplato, idusuario);
        }

        public async Task<Usuario> GetUsuario(string user, string facebookid, string pass)
        {
            try
            {
                return await dataPlato.GetUsuario(user, facebookid, pass);
            }
            catch (Exception ex)
            {
                return null;
                //await page.DisplayAlert("Uh Oh :(", $"Unable to remove {plato?.Nombre ?? "Unknown"}, please try again: {ex.Message}", "OK");
            }
        }

        public async Task<Proveedor> GetProveedor(string idUsuario)
        {
            return await dataPlato.GetProveedorPorUsuario(idUsuario);
        }

        public async Task<FoodLine> GetFoodLineOne(string id)
        {
            return await dataPlato.GetOneFoodLine(id);
        }

        public async Task DeletePlato(Plato plato)
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                //await dataPlato.RemovePlatoAsync(plato);
                //Platos.Remove(plato);
                //Sort();
            }
            catch (Exception ex)
            {
                await page.DisplayAlert("Uh Oh :(", $"Unable to remove {plato?.Nombre ?? "Unknown"}, please try again: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }



        }

        public async Task UpdatePlato(string idPlato, string estado)
        {
            try
            {
                await dataPlato.UpdatePlato(idPlato,estado);
                //Platos.FirstOrDefault(p => p.Lines[0].CPlato._id == idPlato).CPlato.Estado = "Cerrado";
                //Platos.FirstOrDefault(p => p.CPlato._id == idPlato).CerrarOferta = true;
            }
            catch (Exception ex)
            {
                //await page.DisplayAlert("Uh Oh :(", $"Unable to remove {plato?.Nombre ?? "Unknown"}, please try again: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }



        }
        #endregion

        #region Comandos
        private Command forceRefreshCommand;
        public Command ForceRefreshCommand
        {
            get
            {
                return forceRefreshCommand ??
                    (forceRefreshCommand = new Command(async () =>
                    {
                        ForceSync = true;
                        await ExecuteGetPlatosCommand();
                    }));
            }
        }

        private Command getPlatosCommand;
        public Command GetPlatosCommand
        {
            get
            {
                return getPlatosCommand ??
                    (getPlatosCommand = new Command(async () => await ExecuteGetPlatosCommand(), () => { return !IsBusy; }));
            }
        }
        public async Task ExecuteGetPlatosCommand()
        {
            if (IsBusy || Settings.User == null)
                return;
                
            if (ForceSync)
                Settings.LastSync = DateTime.Now.AddDays(-30);

            IsBusy = true;
            GetPlatosCommand.ChangeCanExecute();
            var showAlert = false;
            try
            {
                Platos.Clear();
                ObservableCollection<FoodLineCom> platos = new ObservableCollection<FoodLineCom>();
                if (categoria == "Mis Platos")
                    platos = await dataPlato.GetFoodLine(0,0,0,Settings.Provee._id, categoria);
                else
                    platos = await dataPlato.GetFoodLine(0,0,0,Settings.User._id, categoria);
                if (categoria != "" && categoria != "Mis Solicitudes")
                    Platos = new ObservableCollection<FoodLineCom>(platos.OrderByDescending(p => p.CProveedor.Categoria));
                else
                    Platos = platos;
                //Sort();
            }
            catch (Exception ex)
            {
                showAlert = true;

            }
            finally
            {
                IsBusy = false;
                GetPlatosCommand.ChangeCanExecute();
            }

            if (showAlert)
                await page.DisplayAlert("Uh Oh :(", "Ocurrio un error.", "OK");
            
        }


        ICommand openPlatoCommand;
        public ICommand OpenPlatoCommand
        {
            get
            {
                return openPlatoCommand ?? (openPlatoCommand = new Command<FoodLine>(async (item) => await OpenPlato(item), (arg) => !IsBusy));
            }
        }

        public async Task OpenPlato(FoodLine fl)
        {
            //if (IsBusy || Settings.User == null)
            //    return;

            //if (ForceSync)
            //    Settings.LastSync = DateTime.Now.AddDays(-30);

            //IsBusy = true;
            //ShowPlatoCommand.ChangeCanExecute();
            //var showAlert = false;
            //try
            //{
                
            //}
            //catch (Exception ex)
            //{
            //    showAlert = true;

            //}
            //finally
            //{
            //    IsBusy = false;
            //    ShowPlatoCommand.ChangeCanExecute();
            //}

            //if (showAlert)
            //    await page.DisplayAlert("Uh Oh :(", "Ocurrio un error.", "OK");
            
        }
        //public async Task ExecuteGetPlatosCommand(double la, double lo, double distancia)
        //{
        //    if (IsBusy || Settings.User == null)
        //        return;

        //    if (ForceSync)
        //        Settings.LastSync = DateTime.Now.AddDays(-30);

        //    IsBusy = true;

        //    var showAlert = false;
        //    try
        //    {
        //        Platos.Clear();
        //        ObservableCollection<FoodLineCom> platos = new ObservableCollection<FoodLineCom>();
        //        platos = await dataPlato.GetFoodLine(la, lo, distancia, Settings.User._id, categoria);
        //    }
        //    catch (Exception ex)
        //    {
        //        showAlert = true;

        //    }
        //    finally
        //    {
        //        IsBusy = false;

        //    }

        //    if (showAlert)
        //        await page.DisplayAlert("Uh Oh :(", "Ocurrio un error.", "OK");

        //}
        //public async Task<ObservableCollection<FoodLine>> GetPlatos()
        //{
        //    try
        //    {
        //        if (IsBusy)
        //            return null;
        //        IsBusy = true;
        //        Platos.Clear();
        //        var platos = await dataPlato.GetFoodLine(Settings.User._id);
        //        return platos;
        //    }
        //    catch (Exception ex)
        //    {

        //        return null;
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}

        //private Command<Plato> mostrarUsuarioCommand;
        //public Command<Plato> MostrarUsuarioCommand
        //{
        //    get
        //    {
        //        return mostrarUsuarioCommand ??
        //            (mostrarUsuarioCommand = new Command<Plato>(async (s)=> await ExecuteMostrarUsuarioCommand(s), (s) => { return !IsBusy; }));
        //    }
        //}
        //public async Task ExecuteMostrarUsuarioCommand(Plato param)
        //{

        //    try
        //    {

        //        //Sort();
        //    }
        //    catch (Exception ex)
        //    {
        //        //showAlert = true;

        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //        GetPlatosCommand.ChangeCanExecute();
        //    }



        //}

        public Command MostrarUsuarioCommand
        {
            get
            {

                return new Command((o) =>
                {
                    var tt= o.ToString();
                });
            }
        }
        #endregion

    }

    public class ImagenSrc
    {
        string src;
        string name;

        public string Src
        {
            get
            {
                return src;
            }

            set
            {
                src = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
    }
}

