using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroceryApp
{
    [Activity(Label = "ViewCoupon")]
    public class ViewCoupon : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ViewCouponScreen);

            FindViewById<ImageButton>(Resource.Id.viewCouponBackButton).Click += delegate { StartActivity(typeof(MainActivity)); };       //Setting view to activity_main.xml when back arrow button clicked on view coupon screen.
        }
    }
}