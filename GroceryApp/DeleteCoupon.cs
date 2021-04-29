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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DeleteCouponScreen);
            // Create your application here

            backButton = FindViewById<ImageButton>(Resource.Id.dcBackButton);
            backButton.Click += OpenMain;

            deleteButton = FindViewById<Button>(Resource.Id.dcDeleteButton);
            deleteButton.Click += DeleteSelectedCoupon;

            deleteAllButton = FindViewById<Button>(Resource.Id.dcDeleteAllButton);
            deleteAllButton.Click += DeleteAllCoupons;

            // FindViewById<Button>(Resource.Id.slAddButton).Click += (o, e) => 



        }

        public void OpenMain(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));

            StartActivity(intent);

        }

        public void DeleteSelectedCoupon(object sender, EventArgs e)
        {
      //      Intent intent = new Intent(this, typeof(MainActivity));

       //     StartActivity(intent);

        }

        public void DeleteAllCoupons(object sender, EventArgs e)
        {
       //     Intent intent = new Intent(this, typeof(MainActivity));

       //     StartActivity(intent);

        }
    }
}