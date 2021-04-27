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
    [Activity(Label = "SetList")]
    public class SetList : Activity
    {
        ImageButton backButton;
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SetListScreen);

            // backButton = FindViewById<Button>(Resource.Id.setlBackButton);

            //  FindViewById<ImageButton>(Resource.Id.setlBackButton).Click += (o, e) => SetContentView(Resource.Layout.activity_main);


            backButton = FindViewById<ImageButton>(Resource.Id.setlBackButton);
            backButton.Click += OpenSelectList;

            // FindViewById<Button>(Resource.Id.slAddButton).Click += (o, e) => 



        }

        public void OpenSelectList(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SelectList));
            
            StartActivity(intent);
            
        }
    }
}