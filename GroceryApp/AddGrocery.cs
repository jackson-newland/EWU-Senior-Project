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
        private ImageButton backButton;
        private Button addButton;
        private SearchView searchBar;
        private List<string> Items;
        private List<CategoryItem> Cate;
        private ListView ListViewAddGro;
        private GroceryData _db;
        private GroceryAppDB _GADB;
        private string currentList, currentStore;
        private IEnumerable<CategoryItem> categoryList;
        private CategoryItem selectedGrocery;

        protected override void OnCreate(Bundle savedInstanceState) // creates the add grocery screen
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
            //searchBar.QueryTextChange += SearchBar_QueryTextChange;

            addButton = FindViewById<Button>(Resource.Id.addButtonAddGrocery);
            addButton.Click += AddButton_Click;

            //Calls ListView displaying method.
            backButton = FindViewById<ImageButton>(Resource.Id.addGroceryBackButton);                       //Setting view to activity_main.xml when back arrow button clicked on add grocery screen.
            backButton.Click += OpenMain;

            AddCategories();
        }

        private void ListViewAddGro_ItemClick(object sender, AdapterView.ItemClickEventArgs e) // creates a grocery item on list selection
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
        // different version of searching
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

        private void SearchBar_QueryTextSubmit(object sender, SearchView.QueryTextSubmitEventArgs e) // on search click, searches for the inputed text
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

        private void AddButton_Click(object sender, EventArgs e) // adds the selected grocery to the grocery list
        {
            try
            {
                _GADB.AddGrocery(currentList, selectedGrocery.itemName, selectedGrocery.regPrice, selectedGrocery.coupon, selectedGrocery.store);
                AddedGroceryAlert();
            }
            catch (NullReferenceException)
            {
                SelectGroceryAlert();
            }

        }

        private void OpenMain(object sender, EventArgs e) // returns to main screen
        {
            Intent data = new Intent();
            SetResult(Result.Canceled, data);
            Finish();
        }

        private void DisplayList() // displays the current list
        {
            foreach (CategoryItem i in categoryList)
            {
                Items.Add(i.itemName);
            }
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Items);
            ListViewAddGro.Adapter = adapter;
        }

        private void AddedGroceryAlert() // alert for a list that already exists
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

        private void SelectGroceryAlert() // alert for an invalid selection
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Invalid Selection!");
            alert.SetMessage("Please select a grocery to add");
            alert.SetButton("OK", (c, ev) =>
            {

            });
            alert.Show();
        }

        private void AddCategories() // Since the webscraper isn't operational, this will manually add categorey items to the database
        {
            //Safeway
            _db.AddCategory("Lucerne Mile Whole 1 Gallon", 4.96, 4.96, "", "Milk", "Safeway");
            _db.AddCategory("Fairlife Superkids Milk Ultra-Filtered Reduced Fat", 4.99, 4.99, " ", " Milk", "Safeway");
            _db.AddCategory("Value Corner Milk Reduced Fat 2% - 1 Gallon", 3.49, 3.49, " ", "Milk", "Safeway");
            _db.AddCategory("fairlife Milk Ultra Filtered Reduced Fat 2% - 11.5 Fl. Oz.", 1.99, 1.99, " ", "Milk", "Safeway");
            _db.AddCategory("Lucerne Milk - Half Gallon (container may vary)", 2.89, 2.89, " ", "Milk", "Safeway");
            _db.AddCategory("Fairlife Milk Ultra-Filtered Whole - 52 Fl. Oz.", 3.99, 4.99, "Club Card", "Milk", "Safeway");
            _db.AddCategory("Natures Own Perfectly Crafted Thick White - 22 Oz", 2.79, 4.49, "Club Card", "Bread", "Safeway");
            _db.AddCategory("Signature SELECT Bread 100% Whole Wheat - 24 Oz", 2.49, 3.49, "Club Card", "Bread", "Safeway");
            _db.AddCategory("Oroweat Whole Grains Bread Oatnut - 24 Oz", 4.99, 4.99, " ", "Bread", "Safeway");
            _db.AddCategory("Natures Own Bread Honey - 20 Oz", 4.49, 4.49, " ", "Bread", "Safeway");
            _db.AddCategory("Daves Killer Bread Organic 21 Whole Grains - 27 Oz", 6.49, 6.49, " ", "Bread", "Safeway");
            _db.AddCategory("Ground Beef 80% Lean 20% Fat Value Pack - 3.50 Lbs.", 20.97, 20.97, " ", "Beef", "Safeway");
            _db.AddCategory("Beyond Meat Beyond Burger Plant Based Beef Patties - 8 Oz", 4.99, 4.99, " ", "Beef", "Safeway");
            _db.AddCategory("USDA Choice Beef Boneless Chuck Roast - 3 Lbs", 17.97, 17.97, " ", "Beef", "Safeway");
            _db.AddCategory("USDA Choice Beef For Stew - 1.50 Lbs", 11.24, 11.24, " ", "Beef", "Safeway");
            _db.AddCategory("Hebrew National Beef Franks - 10.3 Oz", 3.5, 5.99, "", "Beef", "Safeway");
            _db.AddCategory("Strawberries Prepacked - 1 Lb", 3.99, 4.99, "", "Fruit", "Safeway");
            _db.AddCategory("Craisins Original - 6 Oz", 2, 2.99, "Club Card", "Fruit", "Safeway");
            _db.AddCategory("Mandarins Clementine Prepacked Bag - 3 Lb", 6.49, 6.49, "Club Card", "Fruit", "Safeway");
            _db.AddCategory("Banana", 0.32, 0.32, " ", "Fruit", "Safeway");
            _db.AddCategory("Blueberries Prepackaged - 6 Oz", 3.99, 3.99, " ", "Fruit", "Safeway");
            _db.AddCategory("Value Corner Large Shell Eggs - 12 Count", 2.89, 2.89, " ", "Eggs", "Safeway");
            _db.AddCategory("O Organics Organic Eggs Large Brown - 12 Count", 4.99, 4.99, " ", "Eggs", "Safeway");
            _db.AddCategory("Egglands Best Eggs Cage Free Large Brown - 12 Count", 3.99, 4.99, "Club Card", "Eggs", "Safeway");
            //Walmart
            _db.AddCategory("Lucerne Mile Whole 1 Gallon", 4.50, 4.50, "", "Milk", "Walmart");
            _db.AddCategory("Fairlife Superkids Milk Ultra-Filtered Reduced Fat", 4.99, 4.99, " ", " Milk", "Walmart");
            _db.AddCategory("Value Corner Milk Reduced Fat 2% - 1 Gallon", 2.80, 2.80, " ", "Milk", "Walmart");
            _db.AddCategory("Natures Own Perfectly Crafted Thick White - 22 Oz", 2.69, 4.49, "Club Card", "Bread", "Walmart");
            _db.AddCategory("Signature SELECT Bread 100% Whole Wheat - 24 Oz", 2.39, 3.49, "Club Card", "Bread", "Walmart");
            _db.AddCategory("Ground Beef 80% Lean 20% Fat Value Pack - 3.50 Lbs.", 20.97, 20.97, " ", "Beef", "Walmart");
            _db.AddCategory("Beyond Meat Beyond Burger Plant Based Beef Patties - 8 Oz", 4.99, 4.99, " ", "Beef", "Walmart");
            _db.AddCategory("All Natural* 80% Lean/20% Fat Ground Beef Chuck Tray, 2.25 lb", 8.68, 8.68, " ", "Beef", "Walmart");
            _db.AddCategory("Orange", 0.72, 0.72, " ", "Fruit", "Walmart");
            _db.AddCategory("Strawberries Prepackaged - 6 Oz", 4.50, 4.99, "Club Card", "Fruit", "Walmart");
            _db.AddCategory("O Organics Organic Eggs Large Brown - 12 Count", 4.99, 4.99, " ", "Eggs", "Walmart");
            //Fred Meyer
            _db.AddCategory("Lucerne Mile Whole 1 Gallon", 4.50, 4.50, "", "Milk", "Fred Meyer");
            _db.AddCategory("Fairlife Superkids Milk Ultra-Filtered Reduced Fat", 4.99, 4.99, " ", " Milk", "Fred Meyer");
            _db.AddCategory("Value Corner Milk Reduced Fat 2% - 1 Gallon", 3.00, 3.00, " ", "Milk", "Fred Meyer");
            _db.AddCategory("Natures Own Perfectly Crafted Thick White - 22 Oz", 2.69, 4.49, "Club Card", "Bread", "Fred Meyer");
            _db.AddCategory("Signature SELECT Bread 100% Whole Wheat - 24 Oz", 2.39, 3.49, "Club Card", "Bread", "Fred Meyer");
            _db.AddCategory("Ground Beef 80% Lean 20% Fat Value Pack - 3.50 Lbs.", 20.97, 20.97, " ", "Beef", "Fred Meyer");
            _db.AddCategory("Beyond Meat Beyond Burger Plant Based Beef Patties - 8 Oz", 4.99, 4.99, " ", "Beef", "Fred Meyer");
            _db.AddCategory("All Natural* 80% Lean/20% Fat Ground Beef Chuck Tray, 2.25 lb", 8.68, 8.68, " ", "Beef", "Fred Meyer");
            _db.AddCategory("Apple", 0.67, 0.67, " ", "Fruit", "Fred Meyer");
            _db.AddCategory("Blackberries Prepackaged - 6 Oz", 3.50, 5.99, "Club Card", "Fruit", "Fred Meyer");
            _db.AddCategory("Value Corner Large Shell Eggs - 12 Count", 3.99, 4.99, "Club Card", "Eggs", "Fred Meyer");
        }

    }
}