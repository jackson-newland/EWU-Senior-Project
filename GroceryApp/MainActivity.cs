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
    [Activity(Label = "Grocery App", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button setListButton, setStoreButton, viewCouponsButton, deleteGroceryButton, addGroceryButton;
        private List<string> Items;
        private ListView ListViewMain;
        private TextView storeName, listName, budget, remBudget;
        private string currentList, currentStore;
        private int requestCodeList = 1, requestCodeStore = 2, requestCodeViewCoupon = 3, requestCodeDeleteGrocery = 4, requestCodeAddGrocery = 5;
        private GroceryAppDB _db;

        protected override void OnCreate(Bundle savedInstanceState) // creates the main screen
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            _db = new GroceryAppDB(); // Creates the connection to the database

            setListButton = FindViewById<Button>(Resource.Id.setListButtonMain);
            setListButton.Click += OpenSelectList;

            setStoreButton = FindViewById<Button>(Resource.Id.setStoreButtonMain);
            setStoreButton.Click += OpenSetStore;

            viewCouponsButton = FindViewById<Button>(Resource.Id.couponsButtonMain);
            viewCouponsButton.Click += OpenViewCoupon;

            deleteGroceryButton = FindViewById<Button>(Resource.Id.deleteButtonMain);
            deleteGroceryButton.Click += OpenDeleteGrocery;

            storeName = FindViewById<TextView>(Resource.Id.currentStoreMain);
            listName = FindViewById<TextView>(Resource.Id.dateRangeMain);

            addGroceryButton = FindViewById<Button>(Resource.Id.addButtonMain);
            addGroceryButton.Click += OpenAddGrocery;

            budget = FindViewById<TextView>(Resource.Id.budgetMainValue);
            remBudget = FindViewById<TextView>(Resource.Id.remBudgetMainValue);

            DisplayList();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void OpenSelectList(object sender, EventArgs e) // opens SelectList screen
        {
            Intent intent = new Intent(this, typeof(SelectList));
            intent.PutExtra("mainList", currentList);
            intent.PutExtra("mainStore", currentStore);
            StartActivityForResult(intent, requestCodeList);
        }

        private void OpenSetStore(object sender, EventArgs e) // opens SetStore screen
        {
            Intent intent = new Intent(this, typeof(SetStore));
            intent.PutExtra("mainStore", currentStore);
            StartActivityForResult(intent, requestCodeStore);
        }

        private void OpenViewCoupon(object sender, EventArgs e) // opens ViewCoupon screen
        {
            Intent intent = new Intent(this, typeof(ViewCoupon));
            intent.PutExtra("currentStore", currentStore);
            intent.PutExtra("currentList", currentList);
            StartActivityForResult(intent, requestCodeViewCoupon);
        }

        private void OpenDeleteGrocery(object sender, EventArgs e) // opens DeleteGrocery screen
        {
            Intent intent = new Intent(this, typeof(DeleteGrocery));
            intent.PutExtra("currentList", currentList);
            StartActivityForResult(intent, requestCodeDeleteGrocery);
        }

        private void OpenAddGrocery(object sender, EventArgs e) // open AddGrocery screen
        {
            Intent intent = new Intent(this, typeof(AddGrocery));
            intent.PutExtra("currentStore", currentStore);
            intent.PutExtra("currentList", currentList);
            StartActivityForResult(intent, requestCodeAddGrocery);
        }

        private void CurrentBudget() // gets the current grocery list's budget and calculates the budget of the sum of all grocery prices
        {
            double listBudget = _db.GetBudget(currentList);
            foreach (Grocery g in _db.GetGroceries(currentList))
            {
                listBudget -= g.Price;
            }
            remBudget.Text = "$" + listBudget.ToString();
            budget.Text = "$" + _db.GetBudget(currentList).ToString();

        }

        private bool ValidStore() // checks to see if the current store is valid and changes the visibility of buttons 
        {
            if (currentStore != "Select A Store" && currentStore != null)
            {
                setListButton.Visibility = 0;
                return true;
            }
            setListButton.Visibility = (ViewStates)4;
            viewCouponsButton.Visibility = (ViewStates)4;
            addGroceryButton.Visibility = (ViewStates)4;
            deleteGroceryButton.Visibility = (ViewStates)4;
            return false;
        }

        private bool ValidList() // checks to see if the current grocery list is valid and changes the visibility of buttons 
        {
            if (currentList != "Select A Date Range" && currentList != null)
            {
                viewCouponsButton.Visibility = 0;
                addGroceryButton.Visibility = 0;
                deleteGroceryButton.Visibility = 0;
                return true;
            }
            viewCouponsButton.Visibility = (ViewStates)4;
            addGroceryButton.Visibility = (ViewStates)4;
            deleteGroceryButton.Visibility = (ViewStates)4;
            return false;

        }

        private void DisplayList() // displays all grocery items from the selected grocery list                                              
        {
            ListViewMain = FindViewById<ListView>(Resource.Id.listViewMain);
            ValidStore();
            ValidList();

            Items = new List<string>();

            IEnumerable<Grocery> glist = _db.GetGroceries(currentList);           // This method calls the current list and converts everything into a Ienumberable list
            foreach (Grocery g in glist)                                          // Goes through the list and adds each grocery name to the list
            {
                Items.Add(g.ToString());
            }

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemActivated1, Items);
            ListViewMain.Adapter = adapter;

            CurrentBudget();
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data) // returns the data from the screens
        {
            switch (requestCode)
            {
                case 1: // set list screen data
                    if (resultCode == Result.Ok)
                    {
                        listName.Text = data.Data.ToString();
                        currentList = data.Data.ToString();
                    }
                    else if (resultCode == Result.FirstUser)
                    {
                        listName.Text = data.Data.ToString();
                        currentList = null;
                    }
                    else if (resultCode == Result.Canceled)
                    {
                        listName.Text = data.Data.ToString();
                        if (data.Data.ToString() == "Select A Date Range")
                        {
                            currentList = null;
                        }
                    }
                    DisplayList();
                    break;

                case 2: // set store screen data

                    if (resultCode == Result.Ok)
                    {
                        storeName.Text = data.Data.ToString();
                        if (data.Data.ToString() == "Select A Store")
                        {
                            currentList = null;
                            storeName.Text = "Select A Store";
                        }
                        currentStore = data.Data.ToString();
                    }
                    else if (resultCode == Result.FirstUser)
                    {
                        storeName.Text = data.Data.ToString();
                        currentStore = null;
                        currentList = null;
                        listName.Text = "Select A Date Range";
                    }
                    else if (resultCode == Result.Canceled)
                    {
                        storeName.Text = data.Data.ToString();
                        if (data.Data.ToString() == "Select A Store")
                        {
                            currentStore = null;
                            currentList = null;
                            listName.Text = "Select A Date Range";
                        }
                    }
                    DisplayList();
                    break;

                case 3:
                    DisplayList();
                    break;

                case 4:
                    DisplayList();
                    break;

                case 5:
                    DisplayList();
                    break;
            }

        }
    }
}