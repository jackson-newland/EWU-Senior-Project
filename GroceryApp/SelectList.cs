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
        ListView listCollection;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SelectListScreen);

            backButton = FindViewById<ImageButton>(Resource.Id.slBackButton);
            backButton.Click += OpenMain;

            addButton = FindViewById<Button>(Resource.Id.slAddButton);
            addButton.Click += AddList;

            deleteButton = FindViewById<Button>(Resource.Id.slDeleteButton);
            deleteButton.Click += DeleteList;

            // FindViewById<Button>(Resource.Id.slAddButton).Click += (o, e) => 



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
          //  Intent intent = new Intent(this, typeof(SetList));

         //   StartActivity(intent);

        }


    }
}