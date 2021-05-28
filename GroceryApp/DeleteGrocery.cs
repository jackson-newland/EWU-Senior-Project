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
        List<string> selectedItems;
        IEnumerable<Grocery> glist;
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

        public void Delete(object sender, EventArgs e)                                  //Delete Grocery method. Takes list of strings created from the ItemClick method below that is
        {                                                                               //populated with strings equal to the groceries (in ToString form) that are clicked/tapped by 
            foreach (Grocery g in glist)                                                //user. If the user taps the grocery again and the list already contains it, it is removed.              
            {                                                                           //This foreach loop activates upon the delete button being clicked and goes through all of the
                if (selectedItems.Contains(g.ToString()))                               //groceries in the list and sees if they appear in their ToString form in the aformention list
                {                                                                       //that keeps track of which items have been selected in the ListView.
                    _db.DeleteGrocery(g.ID);
                }
            }
        }

        public void DeleteAll(object sender, EventArgs e)
        {   
            _db.DeleteAllGrocery(currentList);                                          //Delete All Groceries. Changed it to use Jackson's helper method in the GroceryAppDB class.
            DisplayList();
        }

        public void DisplayList()                                   
        {
            ListViewDelGro = FindViewById<ListView>(Resource.Id.listViewDeleteGrocery);

            Items = new List<string>();                                           

            glist = _db.GetGroceries("List1");                                    //This method calls the current list and converts everything into a Ienumberable list.
            foreach (Grocery g in glist)                                          //Goes through the list and adds each grocery in format to the list.
            {
                Items.Add(g.ToString());
            }

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Items);
            ListViewDelGro.Adapter = adapter;
            ListViewDelGro.ItemClick += ListViewDelGro_ItemClick;                                                //This line and below is being written and test currently.
        }

        private void ListViewDelGro_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            selectedItems = new List<string>();
            String selectedGroc = Items[e.Position];
            if (selectedItems.Contains(selectedGroc))
            {
                selectedItems.Remove(selectedGroc);
            }
            else
            {
                selectedItems.Add(selectedGroc);
            }

        }
    }
}
