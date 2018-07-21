using App7;
using App7.ViewModel;
using App7.Views.Tablet;
using App7.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App7.Views
{
    public partial class HomePage : TabbedPage
    {
        // PlatosViewModel viewModel;

        //public Action<Plato> ItemSelected
        //{
        //    get { return viewModel.ItemSelected; }
        //    set { viewModel.ItemSelected = value; }
        //}
        HomeViewModel viewModel;

        public HomePage()
        {
            try
            {
                InitializeComponent();
                
                viewModel = new HomeViewModel(this);
                BindingContext = viewModel;

                this.drawerList.ItemsSource = new List<string>() { "Inbox", "Drafts", "Sent Items" };
                //ToolbarItems.Add(new ToolbarItem("Ofertas", "ofertas.png", () =>
                //{
                //}));

                

                //ToolbarItems.Add(new ToolbarItem("Store", "tienda.png", async () =>
                //{
                //    if (Device.Idiom == TargetIdiom.Tablet || Device.Idiom == TargetIdiom.Desktop)
                //        await Navigation.PushAsync(new ProveedoresTabletPage());
                //    else
                //        await Navigation.PushAsync(new ProveedoresPage());

                //}));
                //ToolbarItems.Add(new ToolbarItem("Location", "location.png", () =>
                //{
                //}));

                //ToolbarItems.Add(new ToolbarItem("Perfil", "profile.png", () =>
                //{
                //}));

                /*
                if (Device.Idiom == TargetIdiom.Tablet)
                {
                    HeroImage.Source = ImageSource.FromFile("herotablet.jpg");
                }
                */
                //if (Device.OS == TargetPlatform.WinPhone || (Device.OS == TargetPlatform.Windows && Device.Idiom == TargetIdiom.Phone))
                //{
                //    PlatoList.IsGroupingEnabled = false;
                //    PlatoList.ItemsSource = viewModel.Platos;
                //}

                /*
                ButtonLeaveFeedback.Clicked += async (sender, e) =>
                {
                    await Navigation.PushAsync(new FeedbackPage());
                };
                */
            }
            catch (Exception ex)
            {
                throw ex;   
            }
        }
        void OnToolbarButtonClick(object sender, EventArgs e)
        {
            drawer.IsOpen = !drawer.IsOpen;
        }
       
    }
}