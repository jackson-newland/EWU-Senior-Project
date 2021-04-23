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
            //Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Create your application here

            FindViewById<ImageButton>(Resource.Id.deleteGroceryBackButton).Click += (o, e) => SetContentView(Resource.Layout.activity_main);    //Setting view to activity_main.xml when back arrow button clicked on delete grocery screen.

            FindViewById<Button>(Resource.Id.deleteButtonDeleteGrocery).Click += (o, e) => SetContentView(Resource.Layout.activity_main);       //testing if issue with the imgbutton or the other screens.
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}