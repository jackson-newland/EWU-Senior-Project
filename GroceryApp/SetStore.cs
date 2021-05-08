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
        List<string> Items;
        ListView listSetStore;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SetStoreScreen);

            DisplayList();

            backButton = FindViewById<ImageButton>(Resource.Id.ssBackButton);
            backButton.Click += OpenMain;

        }

        public void OpenMain(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        public void DisplayList()
        {
            listSetStore = FindViewById<ListView>(Resource.Id.ssStoreInfoList);     //Displays list of stored stores (selects which database is used basically.)

            Items = new List<string>();                                         //The code that populated the string list will change to concat strings using data from the database. Then it will be added
            Items.Add("Item 1");                                                //in the same way, except perhaps using a for loop or something to add all the items in a list to be displayed. May need to have
            Items.Add("Item 2");                                                //scrolling funtionality, but I will figure that out later.
            Items.Add("Item 3");
            Items.Add("Item 4");
            Items.Add("Item 5");
            Items.Add("Item 6");

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Items);
            listSetStore.Adapter = adapter;
        }

    }
}
