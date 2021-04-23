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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DeleteGroceryScreen);

            FindViewById<ImageButton>(Resource.Id.deleteGroceryBackButton).Click += delegate { StartActivity(typeof(MainActivity)); };    //Setting view to activity_main.xml when back arrow button clicked on delete grocery screen.
        }

    }
}