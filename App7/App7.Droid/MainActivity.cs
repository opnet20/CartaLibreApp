using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Telerik.XamarinForms.Common.Android;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services;
using System.Net;
using XLabs.Forms.Services;
using XLabs.Platform.Services.Geolocation;
using Com.OneSignal;

[assembly: ExportRenderer(typeof(Telerik.XamarinForms.Input.RadCalendar), typeof(Telerik.XamarinForms.InputRenderer.Android.CalendarRenderer))]
[assembly: ExportRenderer(typeof(Telerik.XamarinForms.Input.RadDataForm), typeof(Telerik.XamarinForms.InputRenderer.Android.DataFormRenderer))]
[assembly: ExportRenderer(typeof(Telerik.XamarinForms.DataControls.RadListView), typeof(Telerik.XamarinForms.DataControlsRenderer.Android.ListViewRenderer))]
[assembly: ExportRenderer(typeof(Telerik.XamarinForms.Primitives.RadSideDrawer), typeof(Telerik.XamarinForms.PrimitivesRenderer.Android.SideDrawerRenderer))]

namespace App7.Droid
{
    [Activity(Label = "App7", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]

    public class MainActivity : FormsAppCompatActivity
    {
        private void SetIoc()
        {
            var resolverContainer = new SimpleContainer();
            resolverContainer.Register<IGeolocator, Geolocator>();
            resolverContainer.Register(t => AndroidDevice.CurrentDevice);
            Resolver.SetResolver(resolverContainer.GetResolver());
            
        }

        protected override void OnCreate(Bundle bundle)
        {
            try
            {
                base.OnCreate(bundle);
                
                Forms.Init(this, bundle);
                TelerikForms.Init();


                global::Xamarin.Forms.Forms.Init(this, bundle);
                this.LoadApplication(new App());

                if (!Resolver.IsSet)
                    SetIoc();

                global::Xamarin.Forms.Forms.Init(this, bundle);
                Xamarin.FormsGoogleMaps.Init(this, bundle);

                OneSignal.Current.StartInit("855e791f-bcc6-4af3-a9e2-7e8aca4fed98")
                   .EndInit();

            }
            catch (Exception ex)
            {
                Toast.MakeText(ApplicationContext, "Error" + ex.Message, ToastLength.Long).Show();
            }
        }
    }

    //[assembly: ExportRenderer(typeof(Picker), typeof(CustomPickerRendererAndroid))]
    //public class CustomPickerRendererAndroid : PickerRenderer
    //{

    //    protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
    //    {
    //        base.OnElementChanged(e);

    //        if (e.OldElement != null || e.NewElement != null)
    //        {
    //            Control.TextSize *= 0.22f;
    //        }
    //    }
    //}
}



