using App7.ViewModel.Proveedores;
using App7.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Rg.Plugins.Popup.Extensions;
using App7.Interfaces;
using MvvmHelpers;

namespace App7.Views.Proveedor
{
    public partial class ConfirmarPedidoModal : ContentPage
    {
        readonly IDataStore dataWCF;
        public ConfirmarPedidoModal()
        {
            try
            {
                InitializeComponent();
                dataWCF = DependencyService.Get<IDataStore>();
                BindingContext = ViewModelLocator.PedidosViewModel;
                if (ViewModelLocator.PedidosViewModel.Solicitud.Estado == "Confirmado" || 
                    ViewModelLocator.PedidosViewModel.Solicitud.Estado == "Aceptado" || 
                        ViewModelLocator.PedidosViewModel.Solicitud.Estado == "Oferta")
                {
                    //botonesGrid.IsEnabled = false;
                    confirmarBt.IsEnabled = false;
                    confirmarBt.Icon = null;
                    cancelarBt.IsEnabled = false;
                    cancelarBt.Icon = null;
                    envioEn.IsEnabled = false;
                    adicionalEn.IsEnabled = false;
                    totalEn.IsEnabled = false;
                    comentList.Refreshing += ComentList_Refreshing;
                }
                Load();
            }
            catch (Exception ex)
            {

            }
        }

        private void ComentList_Refreshing(object sender, EventArgs e)
        {
            Load();
        }

        public async void Load()
        {
            var items = await dataWCF.GetComentario(ViewModelLocator.PedidosViewModel.Solicitud._id);
            ViewModelLocator.PedidosViewModel.Comentarios = new ObservableRangeCollection<Comentario>(items.OrderBy(p => p.Fecha));
        }
        void vermapa_Click(object sender, EventArgs e)
        {
            this.Navigation.PushPopupAsync(new ConfirmarPedidoModalMap());
        }
        void confirmar_Click(object sender, EventArgs e)
        {
            //ViewModelLocator.PedidoUsuarioViewModel.AddPedido();
            Pedido t = ViewModelLocator.PedidosViewModel.Solicitud;
            t.Total = t.MTotal;
            t.Estado = "Confirmado";
            ViewModelLocator.PedidosViewModel.UpdatePedido(t);
            ViewModelLocator.PedidosViewModel.Confirmados.Add(t);
            ViewModelLocator.PedidosViewModel.Solicitudes.Remove(t);
            this.Navigation.PopAsync();
        }

        void cancelar_Click(object sender, EventArgs e)
        {
            Pedido t = ViewModelLocator.PedidosViewModel.Solicitud;
            t.Estado = "Rechazado";
            ViewModelLocator.PedidosViewModel.UpdatePedido(t);
            ViewModelLocator.PedidosViewModel.Solicitudes.Remove(t);
            this.Navigation.PopAsync();
        }

        public async void Guardar_Click(object sender, EventArgs e)
        {
            enviarBT.IsEnabled = false;
            Comentario c = new Comentario();
            c.Contenido = comentarioET.Text;
            c.Fecha = DateTime.Now;
            c.idPlato = ViewModelLocator.PedidosViewModel.Solicitud._id;
            c.idUsuario = Settings.User._id;
            if (String.IsNullOrEmpty(Settings.User.Nombres))
                c.NombreUsuario = Settings.UserName;
            else
                c.NombreUsuario = Settings.User.Nombres;
            c.ImagenUsuario = Settings.User.Imagen;

            await dataWCF.SetComentario(c);

            ViewModelLocator.PedidosViewModel.Comentarios.Add(c);
            enviarBT.IsEnabled = true;
            comentarioET.Text = "";
            //comentList.HeightRequest = ViewModelLocator.PedidosViewModel.Comentarios.Count * 98;
        }
        
        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            await ViewModelLocator.PedidosViewModel.SetVistoProveedor();
        }
    }
}
