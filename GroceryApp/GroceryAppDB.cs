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
using SQLiteNetExtensions.Extensions;
using Xamarin.Forms;
using System.IO;


namespace GroceryApp
{
    class GroceryAppDB // main database for grocery lists and groceries
    {
        private SQLiteConnection _connection;
        private string dbPath;
        public GroceryAppDB()
        {
            dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "GroceryAppDB.db3");
            _connection = new SQLiteConnection(dbPath);
            CreateTable();
        }

        public void CreateTable() // creates the GroceryList table and Grocery table
        {
            _connection.CreateTable<GroceryLists>();
            _connection.CreateTable<Grocery>();
            _connection.CreateTable<Store>();
        }

        public IEnumerable<Grocery> GetGroceries(string listName) // takes all the items in the table and returns them into a ienumerable grocery type list
        {
            var groceryList = _connection.Query<Grocery>("SELECT * FROM Groceries WHERE ListName = '" + listName + "'");
            return (from g in groceryList select g).ToList();
        }

        public Grocery GetGrocery(int id) // returns current selected grocery
        {
            return _connection.Get<Grocery>(id);
        }

        public void DeleteGrocery(int id) // deletes selected grocery
        {
            _connection.Delete<Grocery>(id);
        }

        public void DeleteAllGrocery(string listName) // deletes all groceries with inputed list name
        {
            IEnumerable<Grocery> glist = GetGroceries(listName);
            foreach (Grocery g in glist)
            {
                DeleteGrocery(g.ID);
            }
        }

        public void DeleteList(string listName) // deletes all groceries and deletes the inputed list
        {
            DeleteAllGrocery(listName);
            _connection.Delete<GroceryLists>(listName);
        }

        public void AddGrocery(string listName, string name, double price, string coupon, string store) // adds grocery to the current list
        {
            var newGrocery = new Grocery
            {
                Name = name,
                Price = price,
                Coupon = coupon,
                Store = store,
                ListName = listName
            };

            _connection.Insert(newGrocery);
        }

        public void AddGroceryList(string listName, double budget) // Creates the grocery list
        {
            try
            {
                var newGroceryList = new GroceryLists
                {
                    Name = listName,
                    Budget = budget,
                };

                _connection.Insert(newGroceryList);
            }
            catch (SQLiteException e)
            {
                // we can add an exception later for when a list already exists
            }

        }

        public IEnumerable<GroceryLists> GetList() // Takes all the tables in the database and converts them into a list
        {
            return (from l in _connection.Table<GroceryLists>()
                    select l).ToList();
        }

        public IEnumerable<Store> GetStores() // Takes all the stores stored on the databaase and coverts them into a list
        {
            return (from s in _connection.Table<Store>()
                    select s).ToList();
        }

        public bool DoesListExist(string listName) // checks to see if the current list exist in the database
        {
            if (_connection.Find<GroceryLists>(listName) != null)
            {
                return true;
            }
            return false;
        }

        public void AddStore(string name, string address) // adds a store to the database
        {
            try
            {
                var newStore = new Store
                {
                    Name = name,
                    Address = address
                };

                _connection.Insert(newStore);
            }
            catch (SQLiteException e)
            {

            }

        }

        public void DeleteStore(string address) // deletes a store
        {
            _connection.Delete<Store>(address);
        }

        public void DeleteAllStores() // deletes all stores
        {
            _connection.DeleteAll<Store>();
        }

        public void UpdatePrice(string listName, string name, double price) // updates the grocery item price
        {
            _connection.Query<Grocery>("UPDATE Groceries SET Price = '" + price + "' WHERE ListName = '" + listName + "' and Name = '" + name + "'");
        }

        public IEnumerable<Grocery> GetCouponGroceries(string listName, string store) // gets all coupon grocery items
        {
            var couponList = _connection.Query<Grocery>("SELECT * FROM Groceries WHERE ListName = '" + listName + "' and Store = '" + store + "' and Coupon = 'Club Card'");
            return (from c in couponList select c).ToList();
        }

        public void DeleteCoupon(string listName, string name) // deletes coupon
        {
            _connection.Query<Grocery>("UPDATE Groceries SET Coupon = ' ' WHERE ListName = '" + listName + "' and Name = '" + name + "'");
        }

        public double GetBudget(string listName) // calculates the grocery list budget
        {
            try
            {
                return _connection.Get<GroceryLists>(listName).Budget;
            }
            catch (InvalidOperationException e)
            {
                return 0;
            }

        }

        // Work in progress method that would of worked with sql.net extensions, but couldn't get the extension to work
        //public Grocery GetGroceriesList(string listName)
        //{
        //    var gList = _connection.Get<Grocery>(listName).ToString();
        //    var storedInfo = _connection.GetWithChildren<Grocery>(gList.ListName);
        //    return storedInfo;
        //    //return storedInfo;
        //  // return (from g in _connection.Get<GroceryLists>(listName).Groceries select g).ToList();
        //  //  return _connection.Get<GroceryLists>(listName).Name;
        //   // return _connection.Get<GroceryLists>(listName).Groceries;
        //}
    }

}