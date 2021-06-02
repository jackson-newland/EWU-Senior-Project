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
        ImageButton backButton;
        Button setButton, deleteButton, deleteAllButton;
        List<string> Items;
        List<Store> StoresList;
        ListView listSetStore;
        TextView currentStore;
        string currentAddress;
        GroceryAppDB _db = new GroceryAppDB();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SetStoreScreen);
            Items = new List<string>();
            StoresList = new List<Store>();

            //_db.AddStore("Fred Meyer", "400 S Thor St, Spokane, WA 99202");
            //_db.AddStore("Walmart", "5025 E Sprague Ave, Spokane Valley, WA 99212");
            //_db.AddStore("Safeway", "1616 W Northwest Blvd, Spokane, WA 99205");

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

            DisplayList();
        }

        private void DeleteAllButton_Click(object sender, EventArgs e)
        {
            _db.DeleteAllStores();
            currentStore.Text = "Select A Store";
            Items.Clear();
            StoresList.Clear();
            DisplayList();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if(currentAddress != "")
            {
                _db.DeleteStore(currentAddress);
                currentStore.Text = "Select A Store";
                Items.Clear();
                StoresList.Clear();
                DisplayList();
            }
     
        }

        private void ListSetStore_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            currentStore.Text = StoresList[e.Position].Name;
            currentAddress = StoresList[e.Position].Address;
        }


        private void SetButton_Click(object sender, EventArgs e)
        {
            if (currentStore.Text.ToString() == "Select A Store")
            {
                OpenMain(sender, e);
            }
            else
            {
                Intent data = new Intent();
                data.SetData(Android.Net.Uri.Parse(currentStore.Text.ToString()));
                SetResult(Result.Ok, data);
                Finish();
            }
        }

        public void OpenMain(object sender, EventArgs e)
        {
            Intent data = new Intent();
            SetResult(Result.Canceled, data);
            Finish();
        }

        public void DisplayList()
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

    }
}