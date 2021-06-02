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
    class GroceryData
    {
        private SQLiteConnection _connection;
        private string dbPath;
        public GroceryData()
        {
            dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "groceryData.db");
            _connection = new SQLiteConnection(dbPath);
        }

        public List<GroceryApp.CategoryItem> selectTable()
        {
            var connection = new SQLiteConnection(dbPath);
            return connection.Table<CategoryItem>().ToList();
        }

        public IEnumerable<CategoryItem> GetGroceries(string store, string category) // takes all the items in the table and returns them into a ienumerable store item list
        {
            var groceryList = _connection.Query<CategoryItem>("SELECT * FROM storeItems WHERE store = '" + store + "'" + "and category = '" + category + "'");
            return (from g in groceryList select g).ToList();
        }
    }
}
    