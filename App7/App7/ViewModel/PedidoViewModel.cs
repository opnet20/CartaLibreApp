using App7.Interfaces;
using App7.Views;
using App7.ServiceReference1;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;

namespace App7.ViewModel
{
    public class PedidoViewModel : ViewModelBase
    {
        readonly IDataStore dataPedido;
        public FoodLine PlatoPedidoModal { get; set; }
        public string DetallePedido { get; set; }
        public string detalleDireccion;
        public string DetalleDireccion
        {
            get { return detalleDireccion; }
            set
            {
                detalleDireccion = value;
                OnPropertyChanged("DetalleDireccion");
            }
        }
        int cantidad;
        public int Cantidad
        {
            get
            {
                return cantidad;
            }

            set
            {
                cantidad = value;
               
                OnPropertyChanged("Cantidad");
                if (this.PlatoPedidoModal != null)
                    Total = cantidad * this.PlatoPedidoModal.CPlato.Precio;
            
            }
        }

        double total;
        public double Total
        {
            get
            {
                return total;
            }

            set
            {
                total = value;
                OnPropertyChanged("Total");
            }
        }
        private ObservableRangeCollection<Pedido> espera;
        private ObservableRangeCollection<Pedido> pedidos;
        private ObservableRangeCollection<Pedido> procesados;

        public ObservableRangeCollection<Pedido> Espera
        {
            get
            {
                return espera;
            }

            set
            {
                espera = value;
                OnPropertyChanged("Espera");
            }
        }
        public ObservableRangeCollection<Pedido> Pedidos
        {
            get
            {
                return pedidos;
            }

            set
            {
                pedidos = value;
                OnPropertyChanged("Pedidos");
            }
        }
        //public ObservableRangeCollection<Pedido> Procesados
        //{
        //    get
        //    {
        //        return procesados;
        //    }

        //    set
        //    {
        //        procesados = value;
        //        OnPropertyChanged("Procesados");
        //    }
        //}
        private ObservableRangeCollection<Comentario> comentarios;
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
                        //page.Navigation.PushModalAsync(pa);
                        //var id = bt.CommandParameter.ToString();
                        //var plato = ViewModelLocator.PedidoUsuarioViewModel.Pedidos.FirstOrDefault(p => p._id == solicitud._id);
                        //PedidoPageConfirm pg = new PedidoPageConfirm(plato);
                        //pg.confirmEvent += Pg_confirmEvent;
                        //ShowPedido(pg);
                    }
                    catch (Exception ex)
                    {

                    }
                }

                OnPropertyChanged("Solicitud");
            }
        }

        private double totalEspera;
        public double TotalEspera
        {
            get
            {
                return totalEspera;
            }

            set
            {
                SetProperty(ref totalEspera, value);
            }
        }

        double totalPedidos;
        public double TotalPedidos
        {
            get
            {
                return totalPedidos;
            }

            set
            {
                SetProperty(ref totalPedidos, value);
            }
        }

        private DateTime fechaentrega;
        public DateTime Fechaentrega
        {
            get
            {
                return fechaentrega;
            }

            set
            {
                fechaentrega = value;
                OnPropertyChanged("Fechaentrega");
            }
        }
        //public PedidoPage pedidoPage;

        public ServiceReference1.Point punto;

        public PedidoViewModel(Page page) : base(page)
        {
            dataPedido = DependencyService.Get<IDataStore>();
            //this.pedidoPage = page as PedidoPage;
            Title = "Mis Pedidos";
            Espera = new ObservableRangeCollection<Pedido>();
            Pedidos = new ObservableRangeCollection<Pedido>();
            //Procesados = new ObservableRangeCollection<Pedido>();
            Cantidad = 1;
            DetalleDireccion = Settings.User.Direccion;
            if (String.IsNullOrEmpty(this.DetalleDireccion))
            {
                this.DetalleDireccion = String.Empty;
            }
            if (String.IsNullOrEmpty(this.DetallePedido))
            {
                this.DetallePedido = String.Empty;
            }
            //Traer los pedidos actuales
            //Load();

        }

        public async void Load()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            var pedidos = await dataPedido.GetPedidoUsuario(Settings.User._id);

            this.Pedidos = new ObservableRangeCollection<Pedido>(pedidos.Where(p => p.Estado == "Por Confirmar" || p.Estado == "Confirmado" || p.Estado == "Cancelado" || p.Estado == "Aceptado" || p.Estado == "Rechazado").OrderBy(p=>p.Estado));
            //this.Procesados = new ObservableRangeCollection<Pedido>(pedidos.Where(p => p.Estado == "Pagados"));
            IsBusy = false;
        }
        
        public async void AddPedido()
        {
            var existente = Espera.FirstOrDefault(p => p.idPlato == PlatoPedidoModal.CPlato._id.ToString());
            if (existente == null)
            {
                Pedido pp = new Pedido();
                pp.Cantidad = this.Cantidad;
                pp.Plato = PlatoPedidoModal.CPlato.Nombre;
                pp.Total = this.Total;
                pp.idPlato = PlatoPedidoModal.CPlato._id.ToString();
                pp.Usuario = Settings.User.Nombres + " " + Settings.User.Apellidos;
                pp.idProv = PlatoPedidoModal.CPlato.idProv;
                pp.idUsu = Settings.User._id.ToString();
                pp.Detalle = this.DetallePedido;
                pp.Estado = "Por Confirmar";
                pp.FechaPedido = DateTime.Now;
                pp.FechaEntrega = this.Fechaentrega;
                pp.Proveedor = PlatoPedidoModal.CProveedor.Nombre;
                pp.Categoria = PlatoPedidoModal.CPlato.Categoria;
                pp.Select = true;
                pp.Location = this.punto;
                pp.Direccion = this.DetalleDireccion;
                this.Pedidos.Add(pp);
                await dataPedido.SetPedido(pp);
                this.Pedidos = new ObservableRangeCollection<Pedido>(this.Pedidos);
            }
            else
            {
                //this.Espera.Remove(existente);
                existente.Cantidad++;
                existente.Total = existente.Total + PlatoPedidoModal.CPlato.Precio;
                //this.Espera.Add(existente);
            }
            //SumarTotal();
        }

        public async Task TraerNumComentarios()
        {
            foreach (var i in this.Pedidos)
            {
                i.Msgs = await dataPedido.GetComentariosNuevos(i._id, i.FechaVistaUsuario);
                if (i.Msgs > 0)
                {
                    i.AltoMs = 30;
                }
            }
        }

        public async Task UpdatePedido(Pedido p)
        {
            await dataPedido.UpdatePedido(p);
        }

        public async Task SetVistoUsuario()
        {
            await dataPedido.SetVistoPedidoUsuario(this.Solicitud._id, DateTime.Now);
        }

        Command enviarEspera;
        public Command EnviarPedidosEspera
        {
            get
            {
                return enviarEspera ?? (enviarEspera = new Command( f => SetSolicitados(), f => PuedeSolicitar() ));
            }
        }
        public bool PuedeSolicitar()
        {
            return true;
        }
        public async void SetSolicitados()
        {
            try
            {
                foreach (var i in this.Espera)
                {
                    if (i.Select)
                    {
                        if (String.IsNullOrEmpty(i._id))
                        {
                            i.Estado = "Por Confirmar";
                            await dataPedido.SetPedido(i);
                        }
                        else
                        {
                            i.Estado = "Por Confirmar";
                            await dataPedido.UpdatePedido(i);
                        }
                        this.Pedidos.Add(i);
                       // this.Espera.Remove(i);
                    }
                }

                var eliminadosEspera = this.Espera.Where(p => p.Select).ToList();
                foreach (var i in eliminadosEspera)
                {
                    this.Espera.Remove(i);
                }
            }
            catch (Exception ex)
            {

            }            
        }

        private Command getPedidosCommand;
        public Command GetPedidosCommand
        {
            get
            {
                return getPedidosCommand ??
                    (getPedidosCommand = new Command(async () => await ExecuteGetPedidosCommand(), () => { return !IsBusy; }));
            }
        }
        
        public async Task ExecuteGetPedidosCommand()
        {
            if (IsBusy)
                return;
            
            IsBusy = true;
            GetPedidosCommand.ChangeCanExecute();
            var showAlert = false;
            try
            {
                this.Pedidos.Clear();
                //this.Procesados.Clear();

                var pedidos = await dataPedido.GetPedidoUsuario(Settings.User._id);
                var ofertas = this.Espera.Where(p => p.Estado == "Oferta").ToList();
                foreach (var i in ofertas)
                {
                    this.Espera.Remove(i);
                }

                this.Espera.AddRange(new ObservableRangeCollection<Pedido>(pedidos.Where(p => p.Estado == "Oferta")));

                this.Pedidos.ReplaceRange(new ObservableRangeCollection<Pedido>(pedidos.Where(p => p.Estado == "Por Confirmar" || p.Estado == "Confirmado" || p.Estado == "Rechazado" || p.Estado == "Declinado" || p.Estado == "Aceptado").OrderBy(p=>p.FechaEntrega).OrderBy(p=>p.Estado)));

                await TraerNumComentarios();
                //this.Procesados.ReplaceRange(new ObservableRangeCollection<Pedido>(pedidos.Where(p => p.Estado == "Pagados")));
                //Sort();
            }
            catch (Exception ex)
            {
                showAlert = true;

            }
            finally
            {
                IsBusy = false;
                GetPedidosCommand.ChangeCanExecute();
            }

            if (showAlert)
                await page.DisplayAlert("Uh Oh :(", "Ocurrio un error.", "OK");
        }

      
    }
}
