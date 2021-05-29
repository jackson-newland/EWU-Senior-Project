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

        ImageButton backButton;
        Button addButton, deleteButton, setButton;
        TextView currentList;
        List<string> Items;
        ListView listSelectList;
        GroceryAppDB _db;
        int requestCodeSetList = 7;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SelectListScreen);
            _db = new GroceryAppDB();
            Items = new List<string>();

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

            DisplayList();
        }

        private void ListSelectList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            currentList.Text = Items[e.Position];
        }

        private void SetButton_Click(object sender, EventArgs e)
        {
            if(currentList.Text.ToString() == "Select A List")
            {
                OpenMain(sender, e);
            } else
            {
                Intent data = new Intent();
                data.SetData(Android.Net.Uri.Parse(currentList.Text.ToString()));
                SetResult(Result.Ok, data);
                Finish();
            }
           
        }

        public void OpenMain(object sender, EventArgs e)
        {
          
            Intent data = new Intent();
            data.SetData(Android.Net.Uri.Parse(currentList.Text.ToString()));
          
            SetResult(Result.Canceled, data);
            Finish();
        }

        public void AddList(object sender, EventArgs e)
        {
            StartActivityForResult(typeof(SetList), requestCodeSetList);
         
        }

        public void DeleteList(object sender, EventArgs e)
        {
            _db.DeleteList(currentList.Text.ToString());
            currentList.Text = "Select A List";
            DisplayList();
        }

        public void DisplayList()
        {
                    
            IEnumerable<GroceryLists> list = _db.GetList();
            foreach (GroceryLists t in list)
            {
                Items.Add(t.Name);

            }

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Items);
            listSelectList.Adapter = adapter;
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
           if(requestCode == requestCodeSetList)
            {
                if(resultCode == Result.Ok)
                {
                    currentList.Text = data.Data.ToString();
                    DisplayList();
                } 
                else if(resultCode == Result.Canceled)
                {
                    DisplayList();
                }
            }
        }
    }
}