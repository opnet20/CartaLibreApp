using App7.Interfaces;
using App7.Views.Proveedor;
using App7.ServiceReference1;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App7.ViewModel.Proveedores
{
    public class MisPedidosViewModel : ViewModelBase
    {
        public event EventHandler ConfirModal;
        readonly IDataStore dataPedido;
        private ObservableRangeCollection<Comentario> comentarios;
        private ObservableRangeCollection<Pedido> solicitudes;
        private ObservableRangeCollection<Pedido> confirmados;
        public ObservableRangeCollection<Pedido> Solicitudes
        {
            get
            {
                return solicitudes;
            }

            set
            {
                solicitudes = value;
                OnPropertyChanged("Solicitudes");
            }
        }
        public ObservableRangeCollection<Comentario> Comentarios
        {
            get
            {
                return comentarios;
            }

            set
            {
                comentarios = value;
                OnPropertyChanged("Comentarios");
            }
        }
        public ObservableRangeCollection<Pedido> Confirmados
        {
            get
            {
                return confirmados;
            }

            set
            {
                confirmados = value;
                OnPropertyChanged("Confirmados");
            }
        }


        Pedido solicitud;
        public Pedido Solicitud
        {
            get
            {
                return solicitud;
            }
            set
            {
                solicitud = value;
                if (solicitud != null)
                {
                    try
                    {
                        
                        ConfirModal(null,null);
                        //page.Navigation.PushModalAsync(pa);
                    }
                    catch (Exception ex)
                    {

                    }
                }

                OnPropertyChanged("Solicitud");
            }
        }


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


        public MisPedidosViewModel(Page page) : base(page)
        {
            try
            {
                dataPedido = DependencyService.Get<IDataStore>();
                //Traigo los pedidos dirigidos a mi con estado por confirmar y confirmar
                //Load();
                
                this.Comentarios = new ObservableRangeCollection<Comentario>();
                
                Load2();
            }
            catch
            {

            }
        }

        private Command getPedidosCommand;
        public Command GetPedidosCommand
        {
            get
            {
                return getPedidosCommand ??
                    (getPedidosCommand = new Command(async () => await Load(), () => { return !IsBusy; }));
            }
        }
        
        public async Task TraerNumComentarios()
        {
            foreach (var i in this.Solicitudes)
            {
                i.Msgs = await dataPedido.GetComentariosNuevos(i._id, i.FechaVistaProveedor);
                if (i.Msgs > 0)
                {
                    i.AltoMs = 20;
                }
            }
        }

        public async Task Load()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                GetPedidosCommand.ChangeCanExecute();

                var pedidos = await dataPedido.GetPedidoProveedor(Settings.Provee._id.ToString());
                this.Solicitudes = new ObservableRangeCollection<Pedido>(pedidos.Where(p => p.Estado == "Por Confirmar" 
                                                                                            || p.Estado == "Oferta").OrderBy(p=>p.FechaEntrega).OrderBy(p => p.Estado));
                this.Solicitudes = new ObservableRangeCollection<Pedido>(this.Solicitudes);
                this.Confirmados = new ObservableRangeCollection<Pedido>(pedidos.Where(p => p.Estado == "Confirmado"
                                                                                            || p.Estado == "Aceptado"
                                                                                            || p.Estado == "Declinado").OrderBy(p => p.FechaEntrega).OrderBy(p=>p.Estado));
                await TraerNumComentarios();
                IsBusy = false;
            }
            catch(Exception ex)
            {

            }
        }

        public async void Load2()
        {
            var platos = await dataPedido.GetPlatosProveedor(Settings.Provee._id);
            PlatosCarta = new ObservableRangeCollection<Plato>(platos.OrderBy(p => p.FechaCreacion).OrderBy(p => p.Categoria));
            
            //PlatosMenu = new ObservableRangeCollection<Plato>(platos.Where(p => p.Hoy && p.FechaCreacion > DateTime.Now.Date));
            //Historico = new ObservableRangeCollection<Plato>(platos.Where(p => p.Hoy && p.FechaCreacion < DateTime.Now.Date));

        }

        private void Addpl_close(object sender, EventArgs e)
        {
            Load();
        }

        public async void UpdatePedido(Pedido p)
        {
            await dataPedido.UpdatePedido(p);
        }

        public async Task SetVistoProveedor()
        {
            await dataPedido.SetVistoPedidoProveedor(this.Solicitud._id, DateTime.Now);
        }

        Command enviarEspera;
        public Command TestCommand
        {
            get
            {
                return enviarEspera ?? (enviarEspera = new Command(f => SetSolicitados(), f => PuedeSolicitar()));
            }
        }
        public bool PuedeSolicitar()
        {
            return true;
        }
        public void SetSolicitados()
        {
            try
            {
               
            }
            catch (Exception ex)
            {

            }
        }



    }
}
