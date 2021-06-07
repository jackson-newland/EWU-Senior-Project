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

    [Table("Groceries")]
    class Grocery // the grocery item
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Coupon { get; set; }
        public string Store { get; set; }

        [ForeignKey(typeof(GroceryLists))] // With sql.net extension, this works but I couldn't get the tables to join. Basically does nothing currently
        public string ListName { get; set; }

        public Grocery()
        {
        }

        public override string ToString()
        {
            return Name.PadRight(10, ' ') + "\n" + Store + "\n$" + String.Format("{0:0.00}", Price);
        }
    }
}