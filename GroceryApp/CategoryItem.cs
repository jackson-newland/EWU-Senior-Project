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

    [Table("storeItems")]
    class CategoryItem
    {
        [PrimaryKey, AutoIncrement]
        public string itemName { get; set; }
        public double currentPrice { get; set; }
        public double regPrice { get; set; }
        public string coupon { get; set; }
        public string category { get; set; }
        public string store { get; set; }

        

        public CategoryItem()
        {
        }

        
    }
}