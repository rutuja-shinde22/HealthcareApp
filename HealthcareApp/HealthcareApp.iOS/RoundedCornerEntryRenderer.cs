using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using HealthcareApp.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(RoundedCornerEntryRenderer))]
namespace HealthcareApp.iOS
{
    public class RoundedCornerEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {

                Control.BorderStyle = UITextBorderStyle.None;
                Control.Layer.CornerRadius = 10;
                Control.TextColor = UIColor.White;

            }
        }
    }
}