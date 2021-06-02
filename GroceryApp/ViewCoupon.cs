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
        Button addButton, deleteButton;
        List<string> Items;
        ListView ListViewViewCoupon;
        int requestCodeDeleteCoupon = 8;
        TextView currentStoreName, currentStoreAddress;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ViewCouponScreen);

            currentStoreName = FindViewById<TextView>(Resource.Id.storeNameViewCoupon);



            // var store = savedInstanceState.GetBundle("currentStore");
            currentStoreName.Text = Intent.GetStringExtra("currentStore");

          //  currentStoreAddress = FindViewById<TextView>(Resource.Id.storeAddressViewCoupon);
           
            backButton = FindViewById<ImageButton>(Resource.Id.viewCouponBackButton);                       //Setting view to activity_main.xml when back arrow button clicked on view coupon screen.
            backButton.Click += OpenMain;

            addButton = FindViewById<Button>(Resource.Id.vcAddButton);
            addButton.Click += AddButton_Click;

            deleteButton = FindViewById<Button>(Resource.Id.vcDeleteButton);
            deleteButton.Click += DeleteButton_Click;

            DisplayList();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            StartActivityForResult(typeof(DeleteCoupon), requestCodeDeleteCoupon);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            
        }

        public void OpenMain(object sender, EventArgs e)
        {
            Intent data = new Intent();
            SetResult(Result.Canceled, data);
            Finish();
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

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (requestCode == requestCodeDeleteCoupon)
            {
                if (resultCode == Result.Ok)
                {
                    DisplayList();
                }
                else if (resultCode == Result.Canceled)
                {
                    DisplayList();
                }
            }
        }

    }
}