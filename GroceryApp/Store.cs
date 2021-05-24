using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroceryApp
{
    [Table("Stores")]
    class Store // store class that creates basic store information
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}