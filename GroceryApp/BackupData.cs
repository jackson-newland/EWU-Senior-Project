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
using SQLiteNetExtensions.Extensions;
using Xamarin.Forms;
using System.IO;
using GroceryApp;
using Android.Util;

namespace GroceryApp
{
    class GroceryData // this the back up database that replaces the webscraper
    {
        private SQLiteConnection _connection;
        private string dbPath;
        public GroceryData()
        {
            dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "groceryData.db");
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTable<CategoryItem>();
        }

        public List<CategoryItem> selectTable() // selects all tables
        {
            var connection = new SQLiteConnection(dbPath);
            return connection.Table<CategoryItem>().ToList();
        }

        public IEnumerable<CategoryItem> GetGroceries(string store, string category) // takes all the items in the table and returns them into a ienumerable store item list
        {
            var groceryList = _connection.Query<CategoryItem>("SELECT * FROM storeItems WHERE store = '" + store + "'" + "and category = '" + category + "'");
            return (from g in groceryList select g).ToList();
        }

        public IEnumerable<CategoryItem> GetCategoryItems() // gets all category items
        {
            var groceryList = _connection.Query<CategoryItem>("SELECT * FROM storeItems");
            return (from g in groceryList select g).ToList();
        }

        public IEnumerable<CategoryItem> GetCoupons(string category) // gets the coupon items from the current store
        {
            var coupons = _connection.Query<CategoryItem>("SELECT * FROM storeItems WHERE coupon = 'Club Card' and category = '" + category + "'");
            return (from c in coupons select c).ToList();
        }

        public void AddCategory(string itemName, double curPrice, double regPrice, string coupon, string category, string store) // adds a category item to the database
        {
            try
            {
                var newCategory = new CategoryItem
                {
                    itemName = itemName,
                    currentPrice = curPrice,
                    regPrice = regPrice,
                    coupon = coupon,
                    category = category,
                    store = store
                };
                _connection.Insert(newCategory);
            }
            catch (SQLiteException e)
            {

            }

        }

        public double GetCurrentPrice(string itemName) // gets the current price of the category item
        {
            return _connection.Get<CategoryItem>(itemName).currentPrice;
        }
    }
}
