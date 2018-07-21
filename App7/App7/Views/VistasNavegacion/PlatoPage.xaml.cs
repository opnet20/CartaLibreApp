using App7.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using App7.ServiceReference1;
using App7.ViewModel;
using App7.Interfaces;
using App7.Views.VistasNavegacion;
using Rg.Plugins.Popup.Extensions;

namespace App7.Views
{
    public partial class PlatoPage : ContentPage
    {
        PlatoViewModel viewModel;
        readonly IDataStore dataWCF;
        public PlatoPage(FoodLine plato)
        {
            InitializeComponent();
            dataWCF = DependencyService.Get<IDataStore>();
            viewModel = new PlatoViewModel(plato, this);
            viewModel.SetHeight += ViewModel_SetHeight;
            BindingContext = viewModel;
        }

        private void ViewModel_SetHeight(object sender, EventArgs e)
        {
            gridList.HeightRequest = Convert.ToDouble(sender) * 98;
        }
        
        public async void Guardar_Click(object sender, EventArgs e)
        {
            enviarBT.IsEnabled = false;
            Comentario c = new Comentario();
            c.Contenido = comentarioET.Text;
            c.Fecha = DateTime.Now;
            c.idPlato = viewModel.Plato.CPlato._id;
            c.idUsuario = Settings.User._id;
            if (String.IsNullOrEmpty(Settings.User.Nombres))
                c.NombreUsuario = Settings.UserName;
            else
                c.NombreUsuario = Settings.User.Nombres;
            c.ImagenUsuario = Settings.User.Imagen;

            await dataWCF.SetComentario(c);
            viewModel.Comentarios.Add(c);
            enviarBT.IsEnabled = true;

            comentarioET.Text = "";

            gridList.HeightRequest = viewModel.Comentarios.Count * 98;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void AgregarPedido_Click(object sender, EventArgs e)
        {
            try
            {
                Button bt = sender as Button;
                if (!String.IsNullOrEmpty(bt.CommandParameter.ToString()))
                {
                    var plato = bt.CommandParameter as Plato;

                    if (plato.Categoria != "Oferta" && plato.Categoria != "Evento")
                    {
                        var grupos = ViewModelLocator.PlatosViewModel.Platos.First(p => p.CProveedor != null &&
                                                                                   p.CProveedor._id == plato.idProv);

                        var pla = grupos.Lines.FirstOrDefault(p => p.CPlato._id == plato._id);

                        PedidoModalTipo pt = new PedidoModalTipo(pla);
                        this.Navigation.PushPopupAsync(pt);
                    }
                    else
                    {
                        var pa = ViewModelLocator.PlatosViewModel.Platos.First(p => p.CPlato != null &&
                                                                                      p.CProveedor != null &&
                                                                                      p.CPlato == plato &&
                                                                                      p.CProveedor._id == plato.idProv);
                        var pla = pa.Lines[0];
                        PedidoModalTipo pt = new PedidoModalTipo(pla);
                        this.Navigation.PushPopupAsync(pt);
                    }
                }
                else
                {
                    var tt = bt.CommandParameter.ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }

        async void CerrarOferta_Click(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Confirmación", "¿Quieres Cerrar las Ofertas?", "Si", "No");
            if (answer)
            {
                Button bt = sender as Button;
                var plato = bt.CommandParameter as Plato;
                await ViewModelLocator.PlatosViewModel.UpdatePlato(plato._id, "Cerrado");
            }
        }

        void Ofertar_Click(object sender, EventArgs e)
        {
            try
            {
                Button bt = sender as Button;
                if (!String.IsNullOrEmpty(bt.CommandParameter.ToString()))
                {
                    var plato = bt.CommandParameter as Plato;
                  
                    var grupos = ViewModelLocator.PlatosViewModel.Platos.First(p => p.Usuario == plato.NombreUsuario && p.CPlato._id == plato._id);
                    var pla = grupos.Lines[0];
                    var ped = new OfertarPopUp(pla);
                    this.Navigation.PushPopupAsync(ped);
                  
                    //else
                    //{
                    //    var pa = ViewModelLocator.PlatosViewModel.Platos.First(p => p.CPlato != null &&
                    //                                                                     p.CProveedor != null &&
                    //                                                                     p.CPlato == plato &&
                    //                                                                     p.CProveedor._id == plato.idProv);
                    //    var pla = pa.Lines[0];
                    //    var ped = new OfertarPopUp(pla);
                    //    this.Navigation.PushPopupAsync(ped);
                    //}
                }
                else
                {
                    var tt = bt.CommandParameter.ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async void Like_Click(object sender, EventArgs e)
        {
            try
            {
                Button bt = sender as Button;
                if (!String.IsNullOrEmpty(bt.CommandParameter.ToString()))
                {
                    var plato = bt.CommandParameter as Plato;

                    if (await ViewModelLocator.PlatosViewModel.Like(plato._id, Settings.User._id))
                    {
                        this.viewModel.Plato.CPlato.Valoracion++;
                        this.viewModel.Plato.LikeImg = false;
                    }

                    //if (plato.Categoria != "Oferta" && plato.Categoria != "Evento")
                    //{
                    //    var grupos = ViewModelLocator.PlatosViewModel.Platos.First(p => p.CProveedor._id == plato.idProv);
                    //    var pla = grupos.Lines.FirstOrDefault(p => p.CPlato._id == plato._id);

                    //    if (await ViewModelLocator.PlatosViewModel.Like(plato._id, Settings.User._id))
                    //    {
                    //        this.viewModel.Plato.CPlato.Valoracion++;
                    //        this.viewModel.Plato.LikeImg = false;
                    //    }
                    //}
                    //else
                    //{
                    //    var pa = ViewModelLocator.PlatosViewModel.Platos.First(p => p.CPlato != null &&
                    //                                                                p.CProveedor != null &&
                    //                                                                p.CPlato == plato &&
                    //                                                                p.CProveedor._id == plato.idProv);
                    //    var pla = pa.Lines[0];
                    //    if (await ViewModelLocator.PlatosViewModel.Like(plato._id, Settings.User._id))
                    //    {
                    //        this.viewModel.Plato.CPlato.Valoracion++;
                    //        this.viewModel.Plato.LikeImg = false;
                    //    }
                    //}
                }
                else
                {
                    var tt = bt.CommandParameter.ToString();
                }

                //Button bt = sender as Button;
                //if (!String.IsNullOrEmpty(bt.CommandParameter.ToString()))
                //{
                //    var id = bt.CommandParameter.ToString();
                    
                //    if (await ViewModelLocator.PlatosViewModel.Like(id, Settings.User._id))
                //    {
                //        this.viewModel.Plato.CPlato.Valoracion++;
                //        this.viewModel.Plato.LikeImg = false;

                //        if (ViewModelLocator.PlatosViewModel.Platos.FirstOrDefault(p => p.CPlato._id == id) != null)
                //        {
                //            ViewModelLocator.PlatosViewModel.Platos.FirstOrDefault(p => p.CPlato._id == id).CPlato.Valoracion = this.viewModel.Plato.CPlato.Valoracion;
                //            ViewModelLocator.PlatosViewModel.Platos.FirstOrDefault(p => p.CPlato._id == id).LikeImg = false;
                //        }
                //    }

                //}
                //else
                //{
                //    var tt = bt.CommandParameter.ToString();
                //}
            }
            catch (Exception ex)
            {

            }
        }
        
    }
}
