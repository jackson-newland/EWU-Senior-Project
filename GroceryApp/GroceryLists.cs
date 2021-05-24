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
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace GroceryApp
{
    [Table("Grocery_Lists")] // Grocery list table
    class GroceryLists
    {
        [PrimaryKey]
        public string Name { get; set; }
        public double Budget { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)] // this doesn't work at the moment.
        public List<Grocery> Groceries { get; set; } // this doesn't work at the moment.

        public GroceryLists() { }
    }

}