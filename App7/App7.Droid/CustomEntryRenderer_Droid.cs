using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Mobile.Droid.Renderers;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer_Droid))]
namespace Mobile.Droid.Renderers
{
    public class CustomEntryRenderer_Droid : EntryRenderer
    {
        //protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        //{
        //    base.OnElementChanged(e);
        //    Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
        //}
    }
}