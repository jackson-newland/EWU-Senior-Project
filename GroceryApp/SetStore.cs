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
    [Activity(Label = "SetStore")]
    public class SetStore : Activity
    {
        private ImageButton backButton;
        private Button setButton, deleteButton, deleteAllButton;
        private List<string> Items;
        private List<Store> StoresList;
        private ListView listSetStore;
        private TextView currentStore;
        private string currentAddress, mainStore;
        private GroceryAppDB _db = new GroceryAppDB();
        protected override void OnCreate(Bundle savedInstanceState) // creates the the SetStore screen
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SetStoreScreen);
            Items = new List<string>();
            StoresList = new List<Store>();

            mainStore = Intent.GetStringExtra("mainStore");

            listSetStore = FindViewById<ListView>(Resource.Id.ssStoreInfoList);
            listSetStore.ItemClick += ListSetStore_ItemClick;

            backButton = FindViewById<ImageButton>(Resource.Id.ssBackButton);
            backButton.Click += OpenMain;

            currentStore = FindViewById<TextView>(Resource.Id.ssCurrentStoreInfoText);

            setButton = FindViewById<Button>(Resource.Id.ssSetButton);
            setButton.Click += SetButton_Click;

            deleteButton = FindViewById<Button>(Resource.Id.ssDeleteButton);
            deleteButton.Click += DeleteButton_Click;

            deleteAllButton = FindViewById<Button>(Resource.Id.ssDeleteAllButton);
            deleteAllButton.Click += DeleteAllButton_Click;

            if (mainStore != null)
            {
                currentStore.Text = mainStore;
            }

            CreateStores();
            DisplayList();
        }

        private void DeleteAllButton_Click(object sender, EventArgs e) // deletes all stores
        {
            _db.DeleteAllStores();
            currentStore.Text = "Select A Store";
            Items.Clear();
            StoresList.Clear();
            DisplayList();
        }

        private void DeleteButton_Click(object sender, EventArgs e) // deletes selected store
        {
            if (currentAddress != "")
            {
                _db.DeleteStore(currentAddress);
                currentStore.Text = "Select A Store";
                Items.Clear();
                StoresList.Clear();
                DisplayList();
            }
        }

        private void ListSetStore_ItemClick(object sender, AdapterView.ItemClickEventArgs e) // on user input, selects the current store
        {
            currentStore.Text = StoresList[e.Position].Name;
            currentAddress = StoresList[e.Position].Address;
        }

        private void SetButton_Click(object sender, EventArgs e) // sets the selected store
        {
            Intent data = new Intent();
            if (currentStore.Text == "Select A Store")
            {
                data.SetData(Android.Net.Uri.Parse("Select A Store"));
                SetResult(Result.FirstUser, data);
            }
            else
            {
                data.SetData(Android.Net.Uri.Parse(currentStore.Text.ToString()));
                SetResult(Result.Ok, data);
            }
            Finish();

        }

        private void OpenMain(object sender, EventArgs e) // returns to the main screen
        {

            Intent data = new Intent();
            if (currentStore.Text == "Select A Store" || mainStore == null)
            {
                data.SetData(Android.Net.Uri.Parse("Select A Store"));
            }
            else
            {
                data.SetData(Android.Net.Uri.Parse(mainStore));
            }

            SetResult(Result.Canceled, data);
            Finish();
        }

        private void DisplayList() // displays all stores
        {
            //Displays list of stored stores (selects which database is used basically.)
            IEnumerable<Store> sList = _db.GetStores();
            foreach (Store s in sList)
            {
                Items.Add(s.ToString());
                StoresList.Add(s);
            }
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Items);
            listSetStore.Adapter = adapter;
        }

        private void CreateStores() // creates the stores
        {
            _db.AddStore("Fred Meyer", "400 S Thor St, Spokane, WA 99202");
            _db.AddStore("Walmart", "5025 E Sprague Ave, Spokane Valley, WA 99212");
            _db.AddStore("Safeway", "1616 W Northwest Blvd, Spokane, WA 99205");
        }

    }
}
