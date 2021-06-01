﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Graphics;

namespace GroceryApp
{
    [Activity(Label = "DeleteGrocery")]
    public class DeleteGrocery : Activity
    {
        ImageButton backButton;
        Button deleteButton, deleteAllButton;
        List<string> Items, selectedItems;
        ListView listViewDelGro;
        TextView numSelectedDelGro;
        GroceryAppDB _db;
        IEnumerable<Grocery> glist;
        string currentList;
        int selectCounter = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DeleteGroceryScreen);
            _db = new GroceryAppDB();
            currentList = "List1";                                                                       //The currentList string is the variable that will change when the user changes list. This string will be updated by that.
            DisplayList();                                                                               //Calls ListView displaying method.

            selectedItems = new List<string>();
            backButton = FindViewById<ImageButton>(Resource.Id.deleteGroceryBackButton);                 //Setting view to activity_main.xml when back arrow button clicked on delete grocery screen.
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
            if (selectedItems != null)                                                  //user. If the user taps the grocery again and the list already contains it, it is removed.
            {                                                                           //This foreach loop activates upon the delete button being clicked and goes through all of the
                foreach (Grocery g in glist)                                            //groceries in the list and sees if they appear in their ToString form in the aformention list
                {                                                                       //that keeps track of which items have been selected in the ListView.
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

        public void DeleteAll(object sender, EventArgs e)
        {
            _db.DeleteAllGrocery(currentList);                                          //Delete All Groceries. Changed it to use Jackson's helper method in the GroceryAppDB class.
            DisplayList();
        }

        public void DisplayList()
        {
            listViewDelGro = FindViewById<ListView>(Resource.Id.listViewDeleteGrocery);
            Items = new List<string>();
            glist = _db.GetGroceries("List1");                                          //This method calls the current list and converts everything into a Ienumberable list.
            foreach (Grocery g in glist)                                                //Goes through the list and adds each grocery in format to the list.
            {
                Items.Add(g.ToString());
            }

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemActivated1, Items);
            listViewDelGro.Adapter = adapter;
            listViewDelGro.ItemClick += ListViewDelGro_ItemClick;
        }

        private void ListViewDelGro_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {                                                                                              

            string selectedGroc = Items[e.Position];
            numSelectedDelGro = FindViewById<TextView>(Resource.Id.itemsSelectedNumberDeleteGrocery);               

            if (selectedItems.Contains(selectedGroc))                                                       //If the item in the listview has already been selected.
            {
                selectedItems.Remove(selectedGroc);
                selectCounter--;
                numSelectedDelGro.Text = selectCounter.ToString();
                e.View.SetBackgroundColor(Android.Graphics.Color.ParseColor("#F8F8FC"));                    //Changes background color back to the original whitish color.
            }
            else                                                                                            //If the item isn't selected.
            {
                selectedItems.Add(selectedGroc);
                selectCounter++;
                numSelectedDelGro.Text = selectCounter.ToString();
                e.View.SetBackgroundColor(Android.Graphics.Color.ParseColor("#B6CDFA"));                    //Changes background color to light blue for selected.
            }
        }

    }
}