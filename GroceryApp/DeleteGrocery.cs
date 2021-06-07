// Created by Team 4: Chase Christiansen, Jackson Newland, Darrik Teller
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
        private ImageButton backButton;
        private Button deleteButton, deleteAllButton;
        private List<string> Items, selectedItems;
        private ListView listViewDelGro;
        private TextView numSelectedDelGro;
        private GroceryAppDB _db;
        private IEnumerable<Grocery> glist;
        private string currentList;
        private int selectCounter = 0;

        protected override void OnCreate(Bundle savedInstanceState) // creates the DeleteGrocery screen
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DeleteGroceryScreen);
            _db = new GroceryAppDB();
            currentList = Intent.GetStringExtra("currentList");
            selectedItems = new List<string>();

            backButton = FindViewById<ImageButton>(Resource.Id.deleteGroceryBackButton);
            backButton.Click += OpenMain;

            deleteButton = FindViewById<Button>(Resource.Id.deleteButtonDeleteGrocery);
            deleteButton.Click += Delete;

            deleteAllButton = FindViewById<Button>(Resource.Id.deleteAllButtonDeleteGrocery);
            deleteAllButton.Click += DeleteAll;

            DisplayList();
        }

        private void OpenMain(object sender, EventArgs e) // returns the main screen
        {
            Intent data = new Intent();
            SetResult(Result.Canceled, data);
            Finish();
        }

        private void Delete(object sender, EventArgs e) // deletes selected grocery                                 
        {
            if (selectedItems != null)
            {
                foreach (Grocery g in glist)
                {
                    if (selectedItems.Contains(g.ToString()))
                    {
                        _db.DeleteGrocery(g.ID);
                    }
                }
                numSelectedDelGro = FindViewById<TextView>(Resource.Id.itemsSelectedNumberDeleteGrocery);
                numSelectedDelGro.Text = "0";
                selectCounter = 0;
                selectedItems.Clear();
                DisplayList();
            }
        }

        private void DeleteAll(object sender, EventArgs e) // deletes all grocery items
        {
            _db.DeleteAllGrocery(currentList);
            DisplayList();
        }

        private void DisplayList() // displays the current grocery list's groceries.
        {
            listViewDelGro = FindViewById<ListView>(Resource.Id.listViewDeleteGrocery);
            Items = new List<string>();
            glist = _db.GetGroceries(currentList);                                          //This method calls the current list and converts everything into a Ienumberable list.
            foreach (Grocery g in glist)                                                //Goes through the list and adds each grocery in format to the list.
            {
                Items.Add(g.ToString());
            }
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Items);
            listViewDelGro.Adapter = adapter;
            listViewDelGro.ItemClick += ListViewDelGro_ItemClick;
        }

        private void ListViewDelGro_ItemClick(object sender, AdapterView.ItemClickEventArgs e) // On user input, selects the current grocery item
        {
            string selectedGroc = Items[e.Position];
            numSelectedDelGro = FindViewById<TextView>(Resource.Id.itemsSelectedNumberDeleteGrocery);               //Textview for items selected count is increased or decreased inside click event.

            if (selectedItems.Contains(selectedGroc))
            {
                selectedItems.Remove(selectedGroc);
                selectCounter--;
                numSelectedDelGro.Text = selectCounter.ToString();
                e.View.SetBackgroundColor(Android.Graphics.Color.ParseColor("#F8F8FC"));                    //Changes background color back to the original whitish color.
            }
            else
            {
                selectedItems.Add(selectedGroc);
                selectCounter++;
                numSelectedDelGro.Text = selectCounter.ToString();
                e.View.SetBackgroundColor(Android.Graphics.Color.ParseColor("#B6CDFA"));                    //Changes background color to light blue for selected.
            }
        }

    }
}