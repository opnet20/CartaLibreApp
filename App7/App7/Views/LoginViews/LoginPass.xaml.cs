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
    public partial class LoginPass : PopupPage
    {
        readonly IDataStore dataWCF;
        public event EventHandler close;

        public LoginPass()
        {
            InitializeComponent();
            dataWCF = DependencyService.Get<IDataStore>();
            guardarButton.IsEnabled = false;
        }

        public async void Guardar_Click(object sender, EventArgs e)
        {
            guardarButton.IsEnabled = false;
            loading.IsRunning = true;
            //suario us = Settings.User;
            //us.Ciudad = Ciudad1.SelectedItem.ToString();
            //await dataWCF.SetUsuario(us);
            //Settings.User.Ciudad = us.Ciudad;
            await this.Navigation.PopPopupAsync();

            var prove = new ProveedoresPage();
            
            await Navigation.PushModalAsync(prove);
        }
        

        public void Ciudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (Ciudad1.SelectedItem != null)
            //{
            //    guardarButton.IsEnabled = true;
            //}
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
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

    }
}
