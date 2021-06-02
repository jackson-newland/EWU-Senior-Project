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
        [PrimaryKey]
        public string Address { get; set; }

        public override string ToString()
        {
            return Name.PadRight(10, ' ') + "\n" + Address;
        }
    }
}