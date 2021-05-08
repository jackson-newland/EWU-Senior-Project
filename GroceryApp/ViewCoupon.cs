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
        ImageButton backButton;
        List<string> Items;
        ListView ListViewViewCoupon;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ViewCouponScreen);
            DisplayList();
            backButton = FindViewById<ImageButton>(Resource.Id.viewCouponBackButton);                       //Setting view to activity_main.xml when back arrow button clicked on view coupon screen.
            backButton.Click += OpenMain;

        }

        public void OpenMain(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        public void DisplayList()                                               //Placeholder string content (duh). We will use this method to add the strings we make from the database to our ListView.
        {
            ListViewViewCoupon = FindViewById<ListView>(Resource.Id.listViewCoupons);

            Items = new List<string>();
            Items.Add("Item 1");
            Items.Add("Item 2");
            Items.Add("Item 3");
            Items.Add("Item 4");
            Items.Add("Item 5");
            Items.Add("Item 6");

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Items);
            ListViewViewCoupon.Adapter = adapter;
        }

    }
}
