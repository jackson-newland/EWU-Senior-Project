using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using Android.Content;
using Android.Views;
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
        GroceryAppDB _db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            _db = new GroceryAppDB(); // Creates the connection to the database

                                                                                                        //This is a test for creating and adding items to the list
            _db.CreateTable("testList");                                                                // creates a list called testList
            _db.AddGrocery("testList", "Banana", 3.50);                                                 // adds a banana for $3.50 to the current list 

            DisplayList();                                                                              //Calls ListView displaying method.

            setListButton = FindViewById<Button>(Resource.Id.setListButtonMain);
            setListButton.Click += OpenSelectList;

            setStoreButton = FindViewById<Button>(Resource.Id.setStoreButtonMain);
            setStoreButton.Click += OpenSetSore;

            deleteCouponsButton = FindViewById<Button>(Resource.Id.couponsButtonMain);
            deleteCouponsButton.Click += OpenDeleteCoupon;

            deleteGroceryButton = FindViewById<Button>(Resource.Id.deleteButtonMain);
            deleteGroceryButton.Click += OpenDeleteGrocery;

           // FindViewById<Button>(Resource.Id.addButtonMain).Click += (o, e) => SetContentView(Resource.Layout.SetListScreen);
           
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OpenSelectList(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SelectList));
            StartActivity(intent);
        }

        public void OpenSetSore(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SetStore));
            StartActivity(intent);
        }

        public void OpenDeleteCoupon(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(DeleteCoupon));
            StartActivity(intent);
        }

        public void OpenDeleteGrocery(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(DeleteGrocery));
            StartActivity(intent);
        }

        public void OpenAddGrocery(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AddGrocery));
            StartActivity(intent);
        }

        public void DisplayList()                                               //Placeholder string content (duh). We will use this method to add the strings we make from the database to our ListView.
        {
            ListViewMain = FindViewById<ListView>(Resource.Id.listViewMain);

            Items = new List<string>();                                         //The code that populated the string list will change to concat strings using data from the database. Then it will be added
           
            IEnumerable<Grocery> list = _db.GetGroceries("testList");           // This method calls the current list and converts everything into a Ienumberable list
            foreach (Grocery g in list)                                          // Goes through the list and adds each grocery name to the list
            {
                Items.Add(g.Name + "            " + g.Price);                    //Will instead add a string withg the g.Name and g.Price
                                                                                //Already has scrolling functionality.
            }
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Items);
            ListViewMain.Adapter = adapter;
        }
    }
}
