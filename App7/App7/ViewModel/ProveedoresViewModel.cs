using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using MvvmHelpers;
using App7.Interfaces;
using App7.Model;
using App7.Views;
using App7.ServiceReference1;
using System.Collections.Generic;

namespace App7
{
    public class ProveedoresViewModel : ViewModelBase
    {
        readonly IDataStore dataProveedor;

        ObservableRangeCollection<Proveedor> proveedores;
        public ObservableRangeCollection<Proveedor> Proveedores
        {
            get
            {
                return proveedores;
            }

            set
            {
                proveedores = value;
                OnPropertyChanged("Proveedores");
            }
        }
        //public ObservableRangeCollection<Proveedor> Proveedores
        //{
        //    get;
        //    set;
        //}
        //public ObservableRangeCollection<Proveedor> Favoritos { get; set; }
        public ObservableRangeCollection<Grouping<string, Proveedor>> ProveedoresGrouped { get; set; }
        public bool ForceSync { get; set; }
        private string text;
        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }

        public bool localizador;
        public double la;
        public double lo;
        public double distancia;


        public ProveedoresViewModel(Page page) : base(page)
        {
            Title = "Restaurante";
            Text = "";
            dataProveedor = DependencyService.Get<IDataStore>();
            Proveedores = new ObservableRangeCollection<Proveedor>();
            //Favoritos = new ObservableRangeCollection<Proveedor>();
            ProveedoresGrouped = new ObservableRangeCollection<Grouping<string, Proveedor>>();
        }
        public Action<Proveedor> ItemSelected { get; set; }

        Proveedor selectedProveedor;
        public Proveedor SelectedProveedor
        {
            get { return selectedProveedor; }
            set
            {
                selectedProveedor = value;
                OnPropertyChanged("SelectedProveedor");
                if (selectedProveedor == null)
                    return;

                if (ItemSelected == null)
                {
                    page.Navigation.PushModalAsync(new ProveedorPage(selectedProveedor));
                    SelectedProveedor = null;
                }
                else
                {
                    ItemSelected.Invoke(selectedProveedor);
                }
            }
        }


        public async Task DeleteProveedor(Proveedor proveedor)
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                //await dataProveedor.RemoveProveedorAsync(proveedor);
                //Proveedores.Remove(proveedor);
                //Sort();
            }
            catch (Exception ex)
            {
                await page.DisplayAlert("Uh Oh :(", $"Unable to remove {proveedor?.Nombre ?? "Unknown"}, please try again: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }



        }

        private Command forceRefreshCommand;
        public Command ForceRefreshCommand
        {
            get
            {
                return forceRefreshCommand ??
                    (forceRefreshCommand = new Command(async () =>
                    {
                        ForceSync = true;
                        await ExecuteGetProveedoresCommand();
                    }));
            }
        }

        private Command getProveedoresCommand;
        public Command GetProveedoresCommand
        {
            get
            {
                return getProveedoresCommand ??
                    (getProveedoresCommand = new Command(async () => await ExecuteGetProveedoresCommand(), () => { return !IsBusy; }));
            }
        }

       

        private async Task ExecuteGetProveedoresCommand()
        {
            if (IsBusy)
                return;

            if (ForceSync)
                Settings.LastSync = DateTime.Now.AddDays(-30);

            IsBusy = true;
            GetProveedoresCommand.ChangeCanExecute();
            var showAlert = false;
            try
            {
                Proveedores.Clear();
                ObservableCollection<Proveedor> proveedores = new ObservableCollection<Proveedor>();
                if (!localizador)
                {
                    proveedores = await dataProveedor.GetProveedores(Settings.User.Ciudad, Text);
                }
                else
                {
                    proveedores = await dataProveedor.GetProveedoresGPS(la, lo, distancia);
                }
                
                var preferencias = await dataProveedor.GetPreferencias(Settings.User._id);

                var provecod = proveedores.Select(p => p._id);
                var prefecod = preferencias.Select(p => p.idProv);

                var resul = provecod.Intersect(prefecod); //los q sigo en esta lista de proveedores

                foreach (var i in resul)
                {
                    proveedores.First(p => p._id == i).Sigo = true;
                }

                Proveedores.ReplaceRange(proveedores);
                //Sort();
            }
            catch (Exception ex)
            {
                showAlert = true;

            }
            finally
            {
                IsBusy = false;
                GetProveedoresCommand.ChangeCanExecute();
            }

            if (showAlert)
                await page.DisplayAlert("Uh Oh :(", "Unable to gather proveedores.", "OK");


        }


        public async Task SetSeguidor(Seguidores se)
        {
            try
            {
                await dataProveedor.SetSeguidor(se);
                Proveedores.First(p => p._id == se.idProv).Sigo = true;
                OnPropertyChanged("Proveedores");
            }
            catch (Exception ex)
            {
                
            }
        }
        private void Sort()
        {

            ProveedoresGrouped.Clear();
            var sorted = from proveedor in Proveedores
                         orderby proveedor.Pais, proveedor.Ciudad
                         group proveedor by proveedor.Pais into proveedorGroup
                         select new Grouping<string, Proveedor>(proveedorGroup.Key, proveedorGroup);

            ProveedoresGrouped.ReplaceRange(sorted);
        }
    }

}

