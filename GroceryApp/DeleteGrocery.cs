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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DeleteGroceryScreen);

            backButton = FindViewById<ImageButton>(Resource.Id.deleteGroceryBackButton);                //Setting view to activity_main.xml when back arrow button clicked on delete grocery screen.
            backButton.Click += OpenMain;

            deleteButton = FindViewById<Button>(Resource.Id.deleteButtonDeleteGrocery);
            deleteButton.Click += Delete;

            deleteAllButton = FindViewById<Button>(Resource.Id.deleteAllButtonDeleteGrocery);
            deleteAllButton.Click += DeleteAll;

            //Code for ListView here!
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
            //Delete All Groceries
        }

    }
}