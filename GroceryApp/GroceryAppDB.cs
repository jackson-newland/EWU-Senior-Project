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
using Xamarin.Forms;
using System.IO;
using SQLiteNetExtensions.Attributes;

namespace GroceryApp
{
    class GroceryAppDB // The commented out code is for the built in sqlite cmds but they I couldn't get them to work for custom tables
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

            //previous database
            //  _connection.Execute("CREATE TABLE if not exists " + table + " (ID INTEGER PRIMARY KEY AUTOINCREMENT, Name varchar(100), Price DOUBLE(20), Coupon varchar(100), Store varchar(100), CreatedOn varchar(20));");
            // _connection.CreateTable<Grocery>();
        }



        public IEnumerable<Grocery> GetGroceries(string listName) // takes all the items in the table and returns them into a ienumerable grocery type list
        {
            var groceryList = _connection.Query<Grocery>("SELECT * FROM Groceries WHERE ListName = '" + listName + "'");
            return (from g in groceryList select g).ToList();

            // previous database
            //    if (_connection.Execute("SELECT name FROM sqlite_master WHERE type='table' AND name='" + table + "';") > 0) // This stopped working so I commented it out
            //   {

            //    }
            //    var groceryList2 = _connection.Table<Grocery>();
            //    return (from g in groceryList2 select g).ToList();

            //return (from t in _connection.Table<Grocery>()
            //      select t).ToList();
        }

        public Grocery GetGrocery(int id) // returns current selected grocery
        {
            return _connection.Get<Grocery>(id);

            // previous database

            //    if (_connection.Execute("SELECT ID FROM " + table + " WHERE ID = " + id) == id) // just an edge case but we shouldn't need it
            //  { //    if (_connection.Execute("SELECT ID FROM " + table + " WHERE ID = " + id) == id) // just an edge case but we shouldn't need it
            //  {
            //var selectedGrocery = new Grocery
            //{
            //    //ID = id,
            //   // Name = _connection.Execute("SELECT Name FROM " + table + " WHERE ID = " + id).ToString(),
            //   // Price = _connection.Execute("SELECT Price FROM " + table + " WHERE ID = " + id),
            //};
            //return selectedGrocery;


            //    } else
            //  {
            //    return null;
            //}
            //  return _connection.Table<Grocery>().FirstOrDefault(t => t.ID == id);
        }

        public void DeleteGrocery(int id) // deletes selected grocery
        {
            _connection.Delete<Grocery>(id);

            // previous database
            // _connection.Execute("DELETE FROM " + table + " WHERE ID = " + id);
            // _connection.Delete<Grocery>(id);
        }

        public void DeleteAllGrocery(string listName) // deletes all groceries with inputed list name
        {
            IEnumerable<Grocery> glist = GetGroceries(listName);
            foreach (Grocery g in glist)
            {
                DeleteGrocery(g.ID);
            }

            // previous database
            //  _connection.DeleteAllIds<Grocery>(glist);
            //  _connection.Delete<Grocery>(listName);

            // previous database
            //_connection.Execute("DROP TABLE if exists " + table);
            //  _connection.DeleteAll<Grocery>();
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

            // previous database

            //  var currentList = _connection.Get<GroceryLists>(listName);
            // List<Grocery> currentList = new List<Grocery>();
            //foreach (Grocery g in _connection.Get<GroceryLists>(listName).Groceries){
            //    currentList.Add(g);
            //}

            // currentList.Add(newGrocery);
            // currentList.Groceries = new List<Grocery> { newGrocery };
            // _connection.UpdateWithChildren(newGrocery);
            // _connection.Get<GroceryLists>(listName);
            // currentList.Groceries.Add(newGrocery);
            // currentList.Add(newGrocery);

            // currentList.Add(newGrocery);
            // _connection.UpdateWithChildren(_connection.Get<GroceryLists>(listName).Groceries);
            //  _connection.Execute("INSERT into " + table + " (Name, Price, Coupon, Store, CreatedOn) VALUES (?,?,?,?,?);", newGrocery.Name, newGrocery.Price, newGrocery.Coupon, newGrocery.Store, newGrocery.CreatedOn);

            // _connection.Insert(newGrocery);
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

            // previous database
            // var list = _connection.Query<TableName>("SELECT name FROM sqlite_master WHERE type ='table'");
            //return (from l in list select l).ToList();
        }

        public IEnumerable<Store> GetStores() // Takes all the stores stored on the databaase and coverts them into a list
        {
            return (from s in _connection.Table<Store>()
                    select s).ToList();

            // previous database
            // var stores = _connection.Query<Store>("SELECT * FROM Stores"); // Note the table name for the stores is "Stores", this table needs to be created before calling this method
            // return (from s in stores select s).ToList();
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