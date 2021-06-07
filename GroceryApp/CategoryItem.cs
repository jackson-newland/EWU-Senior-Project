// Created by Team 4: Chase Christiansen, Jackson Newland, Darrik Teller
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
    class CategoryItem // since the webscraper isn't operational, this is the item that would of been pulled from the store
    {
        [PrimaryKey]
        public string itemName { get; set; }
        public double currentPrice { get; set; }
        public double regPrice { get; set; }
        public string coupon { get; set; }
        public string category { get; set; }
        public string store { get; set; }

        public CategoryItem()
        {
        }

        public override string ToString()
        {
            return itemName.PadRight(10, ' ') + "\n$" + String.Format("{0:0.00}", regPrice);
        }

    }
}