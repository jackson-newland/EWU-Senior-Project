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
        TextView storeName, listName, budget;
        string currentList, currentStore;
        int requestCodeList = 1, requestCodeStore = 2, requestCodeDeleteCoupon = 3, requestCodeDeleteGrocery = 4, requestCodeAddGrocery = 5, remaining;
        GroceryLists current;
        IEnumerable<Grocery> glist;
        GroceryAppDB _db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            _db = new GroceryAppDB(); // Creates the connection to the database

            

            // Used for testing lists and groceries
            _db.AddGroceryList("List1", 150);
            _db.AddGroceryList("List2", 120);
            _db.AddGrocery("List1", "Milk", 5, "Coupon", "Safeway");
            _db.AddGrocery("List1", "Apple", 3, "Coupon", "Safeway");
            _db.AddGrocery("List1", "Orange", 2, "Coupon", "Safeway");
            _db.AddGrocery("List2", "Milk", 5, "Coupon", "Safeway");
            _db.AddGrocery("List2", "Eggs", 4, "Coupon", "Safeway");                                                                               

            setListButton = FindViewById<Button>(Resource.Id.setListButtonMain);
            setListButton.Click += OpenSelectList;

            setStoreButton = FindViewById<Button>(Resource.Id.setStoreButtonMain);
            setStoreButton.Click += OpenSetStore;

            deleteCouponsButton = FindViewById<Button>(Resource.Id.couponsButtonMain);
            deleteCouponsButton.Click += OpenDeleteCoupon;

            deleteGroceryButton = FindViewById<Button>(Resource.Id.deleteButtonMain);
            deleteGroceryButton.Click += OpenDeleteGrocery;

            addGroceryButton = FindViewById<Button>(Resource.Id.addButtonMain);
            addGroceryButton.Click += OpenAddGrocery;

            storeName = FindViewById<TextView>(Resource.Id.currentStoreMain);
            listName = FindViewById<TextView>(Resource.Id.dateRangeMain);

            addGroceryButton = FindViewById<Button>(Resource.Id.addButtonMain);
            addGroceryButton.Click += OpenAddGrocery;

            GetBudget();
            DisplayList();

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OpenSelectList(object sender, EventArgs e)
        {
          //  Intent intent = new Intent(this, typeof(SelectList));
              
              StartActivityForResult(typeof(SelectList), requestCodeList);
          //  StartActivity(intent);
        }

        public void OpenSetStore(object sender, EventArgs e)
        {
         //   Intent intent = new Intent(this, typeof(SetStore));
          //  StartActivity(intent);

            StartActivityForResult(typeof(SetStore), requestCodeStore);
        }

        public void OpenDeleteCoupon(object sender, EventArgs e)
        {
            // Intent intent = new Intent(this, typeof(DeleteCoupon));
            //  StartActivity(intent);
            Intent intent = new Intent(this, typeof(ViewCoupon));
            intent.PutExtra("currentStore", currentStore);
            StartActivityForResult(intent, requestCodeDeleteCoupon);
        }

        public void OpenDeleteGrocery(object sender, EventArgs e)
        {
            //Intent intent = new Intent(this, typeof(DeleteGrocery));
            //StartActivity(intent);
            Intent intent = new Intent(this, typeof(DeleteGrocery));
            intent.PutExtra("currentStore", currentStore);
            StartActivityForResult(intent, requestCodeDeleteGrocery);

        }

        public void OpenAddGrocery(object sender, EventArgs e)
        {
            //Intent intent = new Intent(this, typeof(AddGrocery));
            //StartActivity(intent);
            Intent intent = new Intent(this, typeof(AddGrocery));
            intent.PutExtra("currentStore", currentStore);
            StartActivityForResult(intent, requestCodeAddGrocery);
        }

        public void GetBudget()
        {
            budget = FindViewById<TextView>(Resource.Id.budgetMain);
            IEnumerable<GroceryLists> lists = _db.GetList();
            foreach (GroceryLists l in lists)                                  //Verifies the correct list is used by making sure the name of it matches the currentList string.
            {
                if (l.Name.Equals(currentList))
                {
                    current = l;
                }
            }
            double remaining = current.Budget;
                                                             
            glist = _db.GetGroceries(currentList);                            //Foreach loop subtracting cost of each item from remaining integer.
            foreach (Grocery g in glist)                                          
            {
                remaining -= g.Price;
            }
            budget.Text = "Budget: " + remaining.ToString();                 //Concats the budget textview with the Budget: text and the remaining amount.
        }

        public void DisplayList()                                               
        {
            ListViewMain = FindViewById<ListView>(Resource.Id.listViewMain);

            Items = new List<string>();                                         //The code that populated the string list will change to concat strings using data from the database. Then it will be added
                                   
                glist = _db.GetGroceries(currentList);                                // This method calls the current list and adds everything into the Ienumberable list
                foreach (Grocery g in glist)                                          // Goes through the list and adds each grocery name to the list
                {
                    Items.Add(g.ToString());
                }          

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemActivated1, Items);
            ListViewMain.Adapter = adapter;
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data) // returns the data from the screens
        {

            switch (requestCode)
            {
                case 1:
                    if (resultCode == Result.Ok)
                    {
                        listName.Text = data.Data.ToString();
                        currentList = data.Data.ToString();
                        DisplayList();
                    }
                    else if (resultCode == Result.Canceled)
                    {
                        DisplayList();
                    }
                    break;

                case 2:
                    if (resultCode == Result.Ok)
                    {
                        storeName.Text = data.Data.ToString();
                        currentStore = data.Data.ToString();
                        DisplayList();
                    }
                    else if (resultCode == Result.Canceled)
                    {
                        DisplayList();
                    }
                    break;


            }
        }
  
    }
}