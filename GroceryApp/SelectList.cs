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
    [Activity(Label = "SelectList")]
    public class SelectList : Activity
    {

        private ImageButton backButton;
        private Button addButton, deleteButton, setButton;
        private TextView currentList;
        private List<string> Items;
        private List<GroceryLists> GroceryList;
        private ListView listSelectList;
        private GroceryAppDB _db;
        private int requestCodeSetList = 7;
        private string mainList;

        protected override void OnCreate(Bundle savedInstanceState) // creates the SelectList screen
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SelectListScreen);
            _db = new GroceryAppDB();
            Items = new List<string>();
            GroceryList = new List<GroceryLists>();
            mainList = Intent.GetStringExtra("mainList");

            backButton = FindViewById<ImageButton>(Resource.Id.slBackButton);
            backButton.Click += OpenMain;

            addButton = FindViewById<Button>(Resource.Id.slAddButton);
            addButton.Click += AddList;

            deleteButton = FindViewById<Button>(Resource.Id.slDeleteButton);
            deleteButton.Click += DeleteList;

            setButton = FindViewById<Button>(Resource.Id.slSetButton);
            setButton.Click += SetButton_Click;

            listSelectList = FindViewById<ListView>(Resource.Id.slLists);
            listSelectList.ItemClick += ListSelectList_ItemClick;

            currentList = FindViewById<TextView>(Resource.Id.slCurrentListInfo);

            if (mainList != null) // checks if the grocery list from the main screen is null
            {
                currentList.Text = mainList;
            }

            DisplayList();
        }

        private void ListSelectList_ItemClick(object sender, AdapterView.ItemClickEventArgs e) // on user input, selects the current grocery list
        {
            currentList.Text = GroceryList[e.Position].Name;
        }

        private void SetButton_Click(object sender, EventArgs e) // sets the current grocery list
        {
            Intent data = new Intent();
            if (currentList.Text == "Select A List")
            {
                data.SetData(Android.Net.Uri.Parse("Select A Date Range"));
                SetResult(Result.FirstUser, data);
            }
            else
            {
                data.SetData(Android.Net.Uri.Parse(currentList.Text.ToString()));
                SetResult(Result.Ok, data);
            }
            Finish();
        }

        private void OpenMain(object sender, EventArgs e) // returns to main screen
        {
            Intent data = new Intent();
            if (currentList.Text == "Select A List" || mainList == null)
            {
                data.SetData(Android.Net.Uri.Parse("Select A Date Range"));
            }
            else
            {
                data.SetData(Android.Net.Uri.Parse(mainList));
            }

            SetResult(Result.Canceled, data);
            Finish();
        }

        private void AddList(object sender, EventArgs e) // opens SetList screen
        {
            StartActivityForResult(typeof(SetList), requestCodeSetList);
        }

        private void DeleteList(object sender, EventArgs e) // deletes selected grocery list
        {
            _db.DeleteList(currentList.Text.ToString());
            currentList.Text = "Select A List";
            DisplayList();
        }

        private void DisplayList() // displays all grocery lists
        {
            Items.Clear();
            GroceryList.Clear();
            IEnumerable<GroceryLists> list = _db.GetList();
            foreach (GroceryLists t in list)
            {
                Items.Add(t.ToString());
                GroceryList.Add(t);
            }

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Items);
            listSelectList.Adapter = adapter;
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data) // data from SetList screen
        {
            if (requestCode == requestCodeSetList)
            {
                if (resultCode == Result.Ok)
                {
                    currentList.Text = data.Data.ToString();
                    DisplayList();
                }
                else if (resultCode == Result.Canceled)
                {
                    DisplayList();
                }
            }
        }
    }
}