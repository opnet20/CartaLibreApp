using System;
using Xamarin.Forms;
using Plugin.ExternalMaps;
using App7.Helpers;
using App7.Model;
using App7.ServiceReference1;
using System.Collections.ObjectModel;
using App7.Interfaces;
using App7.Views;

namespace App7
{
    public class ProveedorViewModel : ViewModelBase
    {
        public readonly IDataStore dataWCF;
        public Proveedor Proveedor { get; set; }
        public event EventHandler SetHeight;
        
        private ObservableCollection<Plato> carta;
        public ObservableCollection<Plato> Carta
        {
            get
            {
                return carta;
            }

            set
            {
                carta = value;
                OnPropertyChanged("Carta");
            }
        }
        Plato platoSelecc;
        public  Plato PlatoSelecc
        {
            get
            {
                return platoSelecc;
            }
            set
            {
                platoSelecc = value;
              
                OnPropertyChanged("PlatoSelecc");
            }
        }

       

        public string Address1 { get { return Proveedor.Direccion; } }
        public string Address2 { get { return string.Format("{0}, {1} {2}", Proveedor.Ciudad , Proveedor.Distrito , Proveedor.Pais); } }

        public ProveedorViewModel(Proveedor proveedor, Page page) : base(page)
        {
            this.Proveedor = proveedor;
            dataWCF = DependencyService.Get<IDataStore>();
            Load();
        }

        public async void Load()
        {
            this.IsBusy = true;
            this.Carta = await dataWCF.GetPlatosActivosProveedor(this.Proveedor._id);
            this.IsBusy = false;
            SetHeight(this.Carta.Count, null);
        }

        public async void LoadPlatoPage(string id)
        {
            var food = await dataWCF.GetOneFoodLine(id);
            await page.Navigation.PushModalAsync(new PlatoPage(food));
        }

        Command navigateCommand;
        public Command NavigateCommand
        {
            get
            {
                return navigateCommand ?? (navigateCommand = new Command(() =>
                CrossExternalMaps.Current.NavigateTo(Proveedor.Nombre, Proveedor.Latitude, Proveedor.Longitude)));
            }
        }

        Command callCommand;
        public Command CallCommand
        {
            get
            {
                //return callCommand ?? (callCommand = new Command(() =>
                //{
                //    var phoneCallTask = MessagingPlugin.PhoneDialer;
                //    if (phoneCallTask.CanMakePhoneCall)
                //        phoneCallTask.MakePhoneCall(Proveedor.NumeroTelefono.CleanPhone());
                //}));
                return null;
            }
        }

      
    }
}

