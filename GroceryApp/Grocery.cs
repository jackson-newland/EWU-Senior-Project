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

namespace GroceryApp
{

    //  [Table("Grocery")]
    class Grocery
    {
        //  [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime CreatedOn { get; set; }

        public Grocery()
        {
        }

        public override string ToString()
        {
            return Name.ToString() + "    " +Price.ToString() + CreatedOn.ToString();
        }
    }
}