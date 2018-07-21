using App7.Interfaces;
using App7.ServiceReference1;
using App7.ViewModel;
using MvvmHelpers;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App7.Views
{
    public partial class PedidoPageConfirm : ContentPage
    {
        public event EventHandler confirmEvent;
        readonly IDataStore dataWCF;
        public PedidoPageConfirm(Pedido p)
        {
            try
            {
                InitializeComponent();
                dataWCF = DependencyService.Get<IDataStore>();
                if (ViewModelLocator.PedidoUsuarioViewModel == null)
                    ViewModelLocator.PedidoUsuarioViewModel = new PedidoViewModel(this);
                ViewModelLocator.PedidoUsuarioViewModel.Solicitud = p;
                BindingContext = ViewModelLocator.PedidoUsuarioViewModel;

                envioLb.Text = p.Envio.ToString();
                adicionalLb.Text = p.Adicional.ToString();
                totalLb.Text = p.Total.ToString();
                comentList.Refreshing += ComentList_Refreshing;
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
            var items = await dataWCF.GetComentario(ViewModelLocator.PedidoUsuarioViewModel.Solicitud._id);
            ViewModelLocator.PedidoUsuarioViewModel.Comentarios = new ObservableRangeCollection<Comentario>(items.OrderBy(p => p.Fecha));
            comentList.IsRefreshing = false;
        }
        async void cancelar_Click(object sender, EventArgs e)
        {
            ViewModelLocator.PedidoUsuarioViewModel.Solicitud.Estado = "Declinado";
            confirmEvent(ViewModelLocator.PedidoUsuarioViewModel.Solicitud, null);
            await this.Navigation.PopPopupAsync();
        }

        async void confirmar_Click(object sender, EventArgs e)
        {
            ViewModelLocator.PedidoUsuarioViewModel.Solicitud.Estado = "Aceptado";
            confirmEvent(ViewModelLocator.PedidoUsuarioViewModel.Solicitud, null);
            await this.Navigation.PopPopupAsync();
        }

        public async void Guardar_Click(object sender, EventArgs e)
        {
            enviarBT.IsEnabled = false;
            Comentario c = new Comentario();
            c.Contenido = comentarioET.Text;
            c.Fecha = DateTime.Now;
            c.idPlato = ViewModelLocator.PedidoUsuarioViewModel.Solicitud._id;
            c.idUsuario = Settings.User._id;
            if (String.IsNullOrEmpty(Settings.User.Nombres))
                c.NombreUsuario = Settings.UserName;
            else
                c.NombreUsuario = Settings.User.Nombres;
            c.ImagenUsuario = Settings.User.Imagen;

            await dataWCF.SetComentario(c);

            ViewModelLocator.PedidoUsuarioViewModel.Comentarios.Add(c);
            enviarBT.IsEnabled = true;
            comentarioET.Text = "";
            //comentList.HeightRequest = ViewModelLocator.PedidosViewModel.Comentarios.Count * 98;
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            await ViewModelLocator.PedidoUsuarioViewModel.SetVistoUsuario();
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
            }
            catch (Exception ex)
            {

            }
        }

    }
}
