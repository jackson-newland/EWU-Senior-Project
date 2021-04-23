using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;

namespace GroceryApp
{
    [Activity(Label = "Home Screen", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main); 

            FindViewById<Button>(Resource.Id.addButtonMain).Click += (o, e) => SetContentView(Resource.Layout.AddGroceryScreen);                //Setting view to AddGroceryScreen.xml when add button clicked on menu screen.
            FindViewById<Button>(Resource.Id.couponsButtonMain).Click += (o, e) => SetContentView(Resource.Layout.ViewCouponScreen);            //Setting view to ViewCouponScreen.xml when coupons button clicked on menu screen.




            //FindViewById<Button>(Resource.Id.deleteButtonMain).Click += (o, e) => SetContentView(Resource.Layout.DeleteGroceryScreen);          //Setting view to DeleteGroceryScreen.xml when delete button clicked on menu screen.
            FindViewById<Button>(Resource.Id.deleteButtonMain).Click += delegate { StartActivity(typeof(DeleteGrocery)); };
            //line 27 is checking if StartActivity is the proper way to navigate screens.


            FindViewById<Button>(Resource.Id.setStoreButtonMain).Click += (o, e) => SetContentView(Resource.Layout.SetStoreScreen);             //Setting view to SetStoreScreen.xml when set store button clicked on menu.
            FindViewById<Button>(Resource.Id.setListButtonMain).Click += (o, e) => SetContentView(Resource.Layout.SelectListScreen);            //Setting view to SelectListScreen.xml when set list button is clicked on menu.

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
       
    }
}