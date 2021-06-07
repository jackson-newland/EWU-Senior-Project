// Created by Team 4: Chase Christiansen, Jackson Newland, Darrik Teller
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
        private ImageButton backButton;
        private Button deleteButton, deleteAllButton;
        private List<string> Items, SelectedCoupons;
        private List<Grocery> GroceryItem;
        private ListView ListViewDelCoup;
        private TextView selectCounter;
        private GroceryAppDB _db;
        private GroceryData _GDDB;
        private string currentList, currentStore, selectedItem;
        private int counter = 0;
        protected override void OnCreate(Bundle savedInstanceState) // creates the delete coupon screen
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
            currentStore = Intent.GetStringExtra("currentStore");

            backButton = FindViewById<ImageButton>(Resource.Id.dcBackButton);
            backButton.Click += OpenMain;

            deleteButton = FindViewById<Button>(Resource.Id.dcDeleteButton);
            deleteButton.Click += DeleteSelectedCoupon;

            deleteAllButton = FindViewById<Button>(Resource.Id.dcDeleteAllButton);
            deleteAllButton.Click += DeleteAllCoupons;

            selectCounter = FindViewById<TextView>(Resource.Id.dcItemsSelectedNumber);

            DisplayList();
        }

        private void ListViewDelCoup_ItemClick(object sender, AdapterView.ItemClickEventArgs e) // user input selects the coupon
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

        private void OpenMain(object sender, EventArgs e) // returns to previous screen
        {
            Intent data = new Intent();
            SetResult(Result.Canceled, data);
            Finish();
        }

        private void DeleteSelectedCoupon(object sender, EventArgs e) // deletes selected coupon
        {
            foreach (string s in SelectedCoupons)
            {
                _db.DeleteCoupon(currentList, s);
            }
            SelectedCoupons.Clear();
            selectCounter.Text = "0";
            DisplayList();
        }

        private void DeleteAllCoupons(object sender, EventArgs e) // deletes all coupons
        {
            foreach (Grocery g in GroceryItem)
            {
                _db.DeleteCoupon(currentList, g.Name);
            }
            DisplayList();
        }

        private void DisplayList() // displays coupons
        {
            Items.Clear();
            GroceryItem.Clear();
            IEnumerable<Grocery> gList = _db.GetCouponGroceries(currentList, currentStore);
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