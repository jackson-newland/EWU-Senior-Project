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
        Button applyButton, deleteButton;
        List<string> Items;
        ListView ListViewViewCoupon;
        int requestCodeDeleteCoupon = 8;
        TextView currentStoreName, currentStoreAddress;
        GroceryAppDB _db;
        GroceryData _GDDB;
        string currentList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ViewCouponScreen);

            _db = new GroceryAppDB();
            _GDDB = new GroceryData();
            Items = new List<string>();

            currentStoreName = FindViewById<TextView>(Resource.Id.storeNameViewCoupon);

            ListViewViewCoupon = FindViewById<ListView>(Resource.Id.listViewCoupons);

            // var store = savedInstanceState.GetBundle("currentStore");
            currentStoreName.Text = Intent.GetStringExtra("currentStore");
            currentList = Intent.GetStringExtra("currentList");
            //  currentStoreAddress = FindViewById<TextView>(Resource.Id.storeAddressViewCoupon);

            backButton = FindViewById<ImageButton>(Resource.Id.viewCouponBackButton);                       //Setting view to activity_main.xml when back arrow button clicked on view coupon screen.
            backButton.Click += OpenMain;

            applyButton = FindViewById<Button>(Resource.Id.vcApplyButton);
            applyButton.Click += ApplyButton_Click;

            deleteButton = FindViewById<Button>(Resource.Id.vcDeleteButton);
            deleteButton.Click += DeleteButton_Click;

            DisplayList();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(DeleteCoupon));
            intent.PutExtra("currentList", currentList);
            StartActivityForResult(intent, requestCodeDeleteCoupon);
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            IEnumerable<Grocery> gList = _db.GetCouponGroceries(currentList, "Club Card");
            List<string> coupons = new List<string>();
            foreach(Grocery g in gList)
            {
               // string price = _GDDB.GetCurrentPrice(g.Name);
                _db.UpdatePrice(currentList, g.Name, _GDDB.GetCurrentPrice(g.Name));
                coupons.Add(g.Name);
            }
            AppliedCouponAlert(coupons);
        }

        public void OpenMain(object sender, EventArgs e)
        {
            Intent data = new Intent();
            SetResult(Result.Canceled, data);
            Finish();
        }

        public void DisplayList()                                               //Placeholder string content (duh). We will use this method to add the strings we make from the database to our ListView.
        {
            Items.Clear();
            IEnumerable<Grocery> gList = _db.GetCouponGroceries(currentList, "Club Card");
            foreach(Grocery g in gList)
            {
                string couponInfo = g.Name + "\nCoupon: " + g.Coupon + "\nRegular Price: $" + g.Price.ToString().PadRight(15, ' ') + "Sale Price: $" + _GDDB.GetCurrentPrice(g.Name);
                Items.Add(couponInfo);
            }



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

        public void AppliedCouponAlert(List<string> couponList) // alert for a list that already exists
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Coupons Applied!");
            string coupons = "";
            foreach(string s in couponList)
            {
                coupons += s + "\n";
            }
            alert.SetMessage(coupons);
            alert.SetButton("OK", (c, ev) =>
            {

            });
            alert.Show();
        }

    }
}
