using App7.Interfaces;
using App7.ServiceReference1;
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
    public partial class SolicitudPopUp : PopupPage
    {
        readonly IDataStore dataPlato;

        public SolicitudPopUp()
        {
            InitializeComponent();
            dataPlato = DependencyService.Get<IDataStore>();
            CategoriaPi.ItemsSource = PGlobal.categorias.Select(p => p.Categoria).ToList();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // Method for animation child in PopupPage
        // Invoced after custom animation end
        protected virtual Task OnAppearingAnimationEnd()
        {
            return Content.FadeTo(0.5);
        }

        // Method for animation child in PopupPage
        // Invoked before custom animation begin
        protected virtual Task OnDisappearingAnimationBegin()
        {
            return Content.FadeTo(1); ;
        }

        protected override bool OnBackButtonPressed()
        {
            // Prevent hide popup
            //return base.OnBackButtonPressed();
            return true;
        }

        // Invoced when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return default value - CloseWhenBackgroundIsClicked
            return base.OnBackgroundClicked();
        }

        public async void Guardar_Click(object sender, EventArgs e)
        {
            if (CategoriaPi.SelectedItem == null)
                return;

            loading.IsRunning = true;
            if (String.IsNullOrEmpty(nombreTB.Text))
                return;

            solicitudButton.IsEnabled = false;
            Plato Myplato = new Plato();
            Myplato.FechaCreacion = DateTime.Now;
            Myplato.Nombre = nombreTB.Text;
            Myplato.Descripcion = descTB.Text;
            Myplato.Estado = "Abierto";
            Myplato.Categoria = CategoriaPi.SelectedItem.ToString();
            Myplato.idUsuario = Settings.User._id;
            Myplato.Ciudad = Settings.User.Ciudad;
            Myplato.Direccion = direcTB.Text;
            Myplato.FechaEntrega = new DateTime(datepicker.Date.Year, 
                datepicker.Date.Month, 
                datepicker.Date.Day,timepicker.Time.Hours,timepicker.Time.Minutes,0);
            Myplato.ImagenUsuario = Settings.User.Imagen;
            if (String.IsNullOrEmpty(Settings.User.Nombres))
                Myplato.NombreUsuario = Settings.UserName;
            else
                Myplato.NombreUsuario = Settings.User.Nombres + " " + Settings.User.Apellidos;
            
            await dataPlato.SetPlato(Myplato);
            loading.IsRunning = false;
            solicitudButton.IsEnabled = true;
            //await this.DisplayAlert("¡Solicitud Agregada!", "Pronto un proveedor de nuestra red te respondera", "OK");
            await this.Navigation.PopPopupAsync();
        }

        async void cancelar_Click(object sender, EventArgs e)
        {
            await this.Navigation.PopPopupAsync();
        }

     
    }
 }
