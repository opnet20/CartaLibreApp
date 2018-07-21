using App7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace App7.Views.Tablet
{
    public class ProveedoresTabletPage : MasterDetailPage
    {
        public ProveedoresTabletPage()
        {
            Title = "Proveedorees";

            Master = new ProveedoresPage();

            Detail = new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    Children =
                    {
                        new Label { Text = "Select a Proveedor", FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) }
                    }
                }
            };

            ((ProveedoresPage)Master).ItemSelected = (proveedor) =>
            {
                //Detail = new StorePage(store);
                if (Device.OS != TargetPlatform.Windows)
                    IsPresented = false;
            };

            IsPresented = true;
        }

        protected override bool OnBackButtonPressed()
        {
            if (IsPresented)
            {
                return base.OnBackButtonPressed();
            }
            else
            {
                IsPresented = true;
                return true;
            }
        }
    }
}
