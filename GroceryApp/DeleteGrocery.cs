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
    [Activity(Label = "DeleteGrocery")]
    public class DeleteGrocery : Activity
    {

        ImageButton backButton;
        Button deleteButton, deleteAllButton;
        List<string> Items;
        ListView ListViewDelGro;
        GroceryAppDB _db;
        String currentList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DeleteGroceryScreen);
            _db = new GroceryAppDB();
            currentList = "List1";                                                                       //The currentList string is the variable that will change when the user changes list. This string will be updated by that.
            DisplayList();                                                                               //Calls ListView displaying method.

            backButton = FindViewById<ImageButton>(Resource.Id.deleteGroceryBackButton);                //Setting view to activity_main.xml when back arrow button clicked on delete grocery screen.
            backButton.Click += OpenMain;

            deleteButton = FindViewById<Button>(Resource.Id.deleteButtonDeleteGrocery);
            deleteButton.Click += Delete;

            deleteAllButton = FindViewById<Button>(Resource.Id.deleteAllButtonDeleteGrocery);
            deleteAllButton.Click += DeleteAll;

        }

        public void OpenMain(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        public void Delete(object sender, EventArgs e)
        {
            //Delete Grocery
        }

        public void DeleteAll(object sender, EventArgs e)
        {
            //Delete All Groceries. Changed it to use Jackson's helper method in the GroceryAppDB class.
            _db.DeleteAllGrocery(currentList);
        }

        public void DisplayList()                                   //Originally from MainActivity.cs. Will revert back if it doesn't work for some reason.
        {
            ListViewDelGro = FindViewById<ListView>(Resource.Id.listViewDeleteGrocery);

            Items = new List<string>();                                           //The code that populated the string list will change to concat strings using data from the database. Then it will be added

            IEnumerable<Grocery> glist = _db.GetGroceries("List1");               //This method calls the current list and converts everything into a Ienumberable list
            foreach (Grocery g in glist)                                          // Goes through the list and adds each grocery name to the list
            {
                Items.Add(g.ToString());

            }

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Items);
            ListViewDelGro.Adapter = adapter;
        }


    }
}
