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
        List<string> Items, SelectedCoupons;
        List<Grocery> GroceryItem;
        ListView ListViewDelCoup;
        TextView selectCounter;
        GroceryAppDB _db;
        GroceryData _GDDB;
        string currentList, selectedItem;
        int counter = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DeleteCouponScreen);

            _db = new GroceryAppDB();
            _GDDB = new GroceryData();

            ListViewDelCoup = FindViewById<ListView>(Resource.Id.dcCouponList);
            ListViewDelCoup.ItemClick += ListViewDelCoup_ItemClick;

            Items = new List<string>();
            SelectedCoupons = new List<string>();
            GroceryItem = new List<Grocery>();

            currentList = Intent.GetStringExtra("currentList");          

            backButton = FindViewById<ImageButton>(Resource.Id.dcBackButton);
            backButton.Click += OpenMain;

            deleteButton = FindViewById<Button>(Resource.Id.dcDeleteButton);
            deleteButton.Click += DeleteSelectedCoupon;

            deleteAllButton = FindViewById<Button>(Resource.Id.dcDeleteAllButton);
            deleteAllButton.Click += DeleteAllCoupons;

            selectCounter = FindViewById<TextView>(Resource.Id.dcItemsSelectedNumber);

            DisplayList();
        }

        private void ListViewDelCoup_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            selectedItem = GroceryItem[e.Position].Name;

            if (SelectedCoupons.Contains(selectedItem))
            {
                SelectedCoupons.Remove(selectedItem);
                counter--;
                selectCounter.Text = counter.ToString();
                e.View.SetBackgroundColor(Android.Graphics.Color.ParseColor("#F8F8FC"));
            }
            else
            {
                SelectedCoupons.Add(selectedItem);
                counter++;
                selectCounter.Text = counter.ToString();
                e.View.SetBackgroundColor(Android.Graphics.Color.ParseColor("#B6CDFA"));
            }
          
        }

        public void OpenMain(object sender, EventArgs e)
        {
            Intent data = new Intent();
            SetResult(Result.Canceled, data);
            Finish();
        }

        public void DeleteSelectedCoupon(object sender, EventArgs e)
        {
            foreach(string s in SelectedCoupons)
            {
                _db.DeleteCoupon(currentList, s);
            }
            SelectedCoupons.Clear();
            selectCounter.Text = "0";
            DisplayList();
        }

        public void DeleteAllCoupons(object sender, EventArgs e)
        {
           foreach(Grocery g in GroceryItem)
            {
                _db.DeleteCoupon(currentList, g.Name);
            }
            DisplayList();
        }

        public void DisplayList()
        {                                          
            Items.Clear();
            GroceryItem.Clear();
            IEnumerable<Grocery> gList = _db.GetCouponGroceries(currentList, "Club Card");
            foreach (Grocery g in gList)
            {
                string couponInfo = g.Name + "\nCoupon: " + g.Coupon + "\nRegular Price: $" + g.Price.ToString().PadRight(15, ' ') + "Sale Price: $" + _GDDB.GetCurrentPrice(g.Name);
                Items.Add(couponInfo);
                GroceryItem.Add(g);
            }
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Items);
            ListViewDelCoup.Adapter = adapter;
        }

    }
}