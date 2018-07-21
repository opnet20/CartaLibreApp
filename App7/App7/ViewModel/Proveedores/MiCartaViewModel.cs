using App7.Interfaces;
using App7.ServiceReference1;
using App7.Views.Proveedores;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App7.ViewModel.Proveedores
{
    public class MiCartaViewModel : ViewModelBase
    {
        readonly IDataStore dataPedido;
        public event EventHandler agregar;
        Plato platoSelecc;

        private ObservableRangeCollection<Plato> platosCarta;
        public ObservableRangeCollection<Plato> PlatosCarta
        {
            get
            {
                return platosCarta;
            }

            set
            {
                platosCarta = value;
                OnPropertyChanged("PlatosCarta");
            }
        }

        public Plato PlatoSelecc
        {
            get
            {
                return platoSelecc;
            }

            set
            {
                platoSelecc = value;
                OnPropertyChanged("PlatoSelecc");
                if (value != null)
                {
                   
                    agregar(value, null);
                }
               
            }
        }

        //public ObservableRangeCollection<Plato> PlatosMenu { get; set; }
        //public ObservableRangeCollection<Plato> Historico { get; set; }

        public MiCartaViewModel(Page page) : base(page)
        {
            dataPedido = DependencyService.Get<IDataStore>();
            //PlatosCarta = new ObservableRangeCollection<Plato>();
            Load();
        }

        private void Addpl_close(object sender, EventArgs e)
        {
            Load();
        }

        public async void Load()
        {
            var platos = await dataPedido.GetPlatosProveedor(Settings.Provee._id);
            PlatosCarta = new ObservableRangeCollection<Plato>(platos.OrderBy(p=>p.FechaCreacion).OrderBy(p=>p.Categoria));
            
            //PlatosMenu = new ObservableRangeCollection<Plato>(platos.Where(p => p.Hoy && p.FechaCreacion > DateTime.Now.Date));
            //Historico = new ObservableRangeCollection<Plato>(platos.Where(p => p.Hoy && p.FechaCreacion < DateTime.Now.Date));

        }
    }
}
