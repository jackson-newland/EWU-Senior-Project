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
    [Activity(Label = "DeleteCoupon")]
    public class DeleteCoupon : Activity
    {
        ImageButton backButton;
        Button deleteButton, deleteAllButton;
        List<string> Items;
        ListView ListViewDelCoup;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DeleteCouponScreen);

            DisplayList();

            backButton = FindViewById<ImageButton>(Resource.Id.dcBackButton);
            backButton.Click += OpenMain;

            deleteButton = FindViewById<Button>(Resource.Id.dcDeleteButton);
            deleteButton.Click += DeleteSelectedCoupon;

            deleteAllButton = FindViewById<Button>(Resource.Id.dcDeleteAllButton);
            deleteAllButton.Click += DeleteAllCoupons;

        }

        public void OpenMain(object sender, EventArgs e)
        {
            Intent data = new Intent();
            SetResult(Result.Canceled, data);
            Finish();
        }

        public void DeleteSelectedCoupon(object sender, EventArgs e)
        {
            //SQL command to remove coupon with corresponding ID.

        }

        public void DeleteAllCoupons(object sender, EventArgs e)
        {
            //SQL command to remove all coupons saved. Won't the web scraper pick them up again later though?

        }

        public void DisplayList()
        {
            ListViewDelCoup = FindViewById<ListView>(Resource.Id.dcCouponList);

            Items = new List<string>();                                         //The code that populated the string list will change to concat strings using data from the database. Then it will be added
            Items.Add("Item 1");                                                //in the same way, except perhaps using a for loop or something to add all the items in a list to be displayed. May need to have
            Items.Add("Item 2");                                                //scrolling funtionality, but I will figure that out later.
            Items.Add("Item 3");
            Items.Add("Item 4");
            Items.Add("Item 5");
            Items.Add("Item 6");

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Items);
            ListViewDelCoup.Adapter = adapter;
        }
    }
}
