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
    [Activity(Label = "AddGrocery")]
    public class AddGrocery : Activity
    {
        ImageButton backButton;
        Button addButton;
        SearchView searchBar;
        List<string> Items;
        List<CategoryItem> Cate;
        ListView ListViewAddGro;
        GroceryData _db;
        GroceryAppDB _GADB;
        string currentList, currentStore;
        IEnumerable<CategoryItem> categoryList;
        CategoryItem selectedGrocery;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddGroceryScreen);
            _db = new GroceryData();
            _GADB = new GroceryAppDB();
            currentList = Intent.GetStringExtra("currentList");
            currentStore = Intent.GetStringExtra("currentStore");

            Items = new List<string>();
            Cate = new List<CategoryItem>();
       

            ListViewAddGro = FindViewById<ListView>(Resource.Id.listViewAddGrocery);
            ListViewAddGro.ItemClick += ListViewAddGro_ItemClick;

            searchBar = FindViewById<SearchView>(Resource.Id.searchViewAddGrocery);
            searchBar.QueryTextSubmit += SearchBar_QueryTextSubmit;
          //  searchBar.QueryTextChange += SearchBar_QueryTextChange;

            addButton = FindViewById<Button>(Resource.Id.addButtonAddGrocery);
            addButton.Click += AddButton_Click;

                                                                                            //Calls ListView displaying method.
            backButton = FindViewById<ImageButton>(Resource.Id.addGroceryBackButton);                       //Setting view to activity_main.xml when back arrow button clicked on add grocery screen.
            backButton.Click += OpenMain;    
            


            //  DisplayList();
        }

        private void ListViewAddGro_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
          
            selectedGrocery = new CategoryItem
            {
                itemName = Cate[e.Position].itemName,
                currentPrice = Cate[e.Position].currentPrice,
                regPrice = Cate[e.Position].regPrice,
                coupon = Cate[e.Position].coupon,
                category = Cate[e.Position].category,
                store = Cate[e.Position].store
            };

        }

        //private void SearchBar_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        //{
        //    Items.Clear();
        //    Cate.Clear();
        //    foreach (CategoryItem i in _db.GetGroceries(currentStore,e.NewText))
        //    {
        //        Items.Add(i.itemName);
        //        Cate.Add(i);
        //    }
        //    ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, Items);
        //    adapter.Filter.InvokeFilter(e.NewText);
        //    ListViewAddGro.Adapter = adapter;
        //}

        private void SearchBar_QueryTextSubmit(object sender, SearchView.QueryTextSubmitEventArgs e)
        {
            Items.Clear();
            Cate.Clear();
            foreach (CategoryItem i in _db.GetGroceries(currentStore, e.Query))
            {
                Items.Add(i.ToString());
                Cate.Add(i);
            }
            ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, Items);
            ListViewAddGro.Adapter = adapter;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            _GADB.AddGrocery(currentList, selectedGrocery.itemName, selectedGrocery.regPrice, selectedGrocery.coupon, selectedGrocery.store);
            AddedGroceryAlert();
        }

        public void OpenMain(object sender, EventArgs e)
        {
            Intent data = new Intent();
            SetResult(Result.Canceled, data);
            Finish();
        }

        public void DisplayList()
        {
            foreach (CategoryItem i in categoryList)
            {
                Items.Add(i.itemName);
            }
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Items);
            ListViewAddGro.Adapter = adapter;
        }

        public void AddedGroceryAlert() // alert for a list that already exists
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Grocery Added!");
            alert.SetMessage(selectedGrocery.itemName + "\n$" + selectedGrocery.regPrice);
            alert.SetButton("OK", (c, ev) =>
            {

            });
            alert.Show();
        }

    }
}