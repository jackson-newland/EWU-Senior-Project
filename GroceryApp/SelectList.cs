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
        Button addButton, deleteButton;

        TextView currentList;
        List<string> Items;
        ListView listSelectList;
        GroceryAppDB _db;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SelectListScreen);
            _db = new GroceryAppDB();

            DisplayList();

            backButton = FindViewById<ImageButton>(Resource.Id.slBackButton);
            backButton.Click += OpenMain;

            addButton = FindViewById<Button>(Resource.Id.slAddButton);
            addButton.Click += AddList;

            deleteButton = FindViewById<Button>(Resource.Id.slDeleteButton);
            deleteButton.Click += DeleteList;
        }

        public void OpenMain(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));

            StartActivity(intent);
        }

        public void AddList(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SetList));

            StartActivity(intent);
        }

        public void DeleteList(object sender, EventArgs e)
        {
            _db.DeleteList("List1"); // testing for deletion of list
        }

        public void DisplayList()
        {
            listSelectList = FindViewById<ListView>(Resource.Id.slLists);


            Items = new List<string>();
            IEnumerable<GroceryLists> list = _db.GetList();
            foreach (GroceryLists t in list)
            {
                Items.Add(t.Name);

            }

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Items);
            listSelectList.Adapter = adapter;
        }

    }
}
