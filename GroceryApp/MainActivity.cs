using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroceryApp
{
    [Activity(Label = "Home Screen", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button setListButton, setStoreButton, deleteCouponsButton, selectListButton, deleteGroceryButton, addGroceryButton;
        List<string> Items;
        ListView ListViewMain;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            DisplayList();                                                                              //Calls ListView displaying method.

            setListButton = FindViewById<Button>(Resource.Id.setListButtonMain);
            setListButton.Click += OpenSelectList;

            setStoreButton = FindViewById<Button>(Resource.Id.setStoreButtonMain);
            setStoreButton.Click += OpenSetSore;

            deleteCouponsButton = FindViewById<Button>(Resource.Id.couponsButtonMain);
            deleteCouponsButton.Click += OpenDeleteCoupon;

            deleteGroceryButton = FindViewById<Button>(Resource.Id.deleteButtonMain);
            deleteGroceryButton.Click += OpenDeleteGrocery;

            addGroceryButton = FindViewById<Button>(Resource.Id.addButtonMain);
            addGroceryButton.Click += OpenAddGrocery;
           
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OpenSelectList(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SelectList));
           // SetContentView(Resource.Layout.SetListScreen);
            StartActivity(intent);
        }

        public void OpenSetSore(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SetStore));
            // SetContentView(Resource.Layout.SetListScreen);
            StartActivity(intent);
        }

        public void OpenDeleteCoupon(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(DeleteCoupon));            
            // SetContentView(Resource.Layout.SetListScreen);
            StartActivity(intent);
        }

        public void OpenDeleteGrocery(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(DeleteGrocery));
            // SetContentView(Resource.Layout.SetListScreen);
            StartActivity(intent);
        }

        public void OpenAddGrocery(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AddGrocery));
            // SetContentView(Resource.Layout.SetListScreen);
            StartActivity(intent);
        }

        public void DisplayList()                                               //Placeholder string content (duh). We will use this method to add the strings we make from the database to our ListView.
        {
            ListViewMain = FindViewById<ListView>(Resource.Id.listViewMain);

            Items = new List<string>();
            Items.Add("Item 1");
            Items.Add("Item 2");
            Items.Add("Item 3");
            Items.Add("Item 4");
            Items.Add("Item 5");
            Items.Add("Item 6");

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Items);
            ListViewMain.Adapter = adapter;
        }
    }
}