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
        List<string> Items;
        ListView ListViewAddGro;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddGroceryScreen);
            DisplayList();                                                                                  //Calls ListView displaying method.
            backButton = FindViewById<ImageButton>(Resource.Id.addGroceryBackButton);                       //Setting view to activity_main.xml when back arrow button clicked on add grocery screen.
            backButton.Click += OpenMain;

        }

        public void OpenMain(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        public void DisplayList()                                               //Placeholder string content (duh). We will use this method to add the strings we make from the database to our ListView.
        {
            ListViewAddGro = FindViewById<ListView>(Resource.Id.listViewAddGrocery);

            Items = new List<string>();
            Items.Add("Item 1");
            Items.Add("Item 2");
            Items.Add("Item 3");
            Items.Add("Item 4");
            Items.Add("Item 5");
            Items.Add("Item 6");

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Items);
            ListViewAddGro.Adapter = adapter;
        }

    }
}