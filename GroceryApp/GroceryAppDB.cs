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
using Microsoft.Data.Sqlite;

namespace GroceryApp
{
    class GroceryAppDB // The commented out code is for the built in sqlite cmds but they I couldn't get them to work for custom tables
    {
        private SQLiteConnection _connection;
        private string dbPath;
        public GroceryAppDB()
        {
            dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),"GroceryAppDB.db3");
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTable<Grocery>(); // This creates a default list, but won't have any information
        }

        public void CreateTable(string table) // creates the table 
        {
            _connection.Execute("CREATE TABLE if not exists " + table + " (ID INTEGER PRIMARY KEY AUTOINCREMENT, Name varchar(100), Price DOUBLE(20), CreatedOn varchar(20));");
           // _connection.CreateTable<Grocery>();
        }

        public IEnumerable<Grocery> GetGroceries(string table) // takes all the items in the table and returns them into a ienumerable grocery type list
        {
        //    if (_connection.Execute("SELECT name FROM sqlite_master WHERE type='table' AND name='" + table + "';") > 0) // This stopped working so I commented it out
         //   {

                var groceryList = _connection.Query<Grocery>("SELECT * FROM " + table);
                return (from g in groceryList select g).ToList();

        //    }
        //    var groceryList2 = _connection.Table<Grocery>();
        //    return (from g in groceryList2 select g).ToList();

            //return (from t in _connection.Table<Grocery>()
            //      select t).ToList();
        }

        public Grocery GetGrocery(string table, int id) // returns current selected grocery
        {
            //    if (_connection.Execute("SELECT ID FROM " + table + " WHERE ID = " + id) == id) // just an edge case but we shouldn't need it
            //  {

            var selectedGrocery = new Grocery
            {
                ID = id,
                Name = _connection.Execute("SELECT Name FROM " + table + " WHERE ID = " + id).ToString(),
                Price = _connection.Execute("SELECT Price FROM " + table + " WHERE ID = " + id),
                CreatedOn = new DateTime(_connection.Execute("SELECT CreatedOn FROM " + table + " WHERE ID = " + id))
            };
            return selectedGrocery;


            //    } else
            //  {
            //    return null;
              //}
          //  return _connection.Table<Grocery>().FirstOrDefault(t => t.ID == id);
        }

        public void DeleteGrocery(string table, int id) // deletes selected grocery
        {
            _connection.Execute("DELETE FROM " + table + " WHERE ID = " + id);
            //_connection.Delete<Grocery>(id);
        }

        public void DeleteAllGrocery(string table) // deletes the table/list
        {
            _connection.Execute("DROP TABLE if exists " + table);
          //  _connection.DeleteAll<Grocery>();
        }

        public void AddGrocery(string table, string name, double price) // adds grocery to the current list
        {
            var newGrocery = new Grocery
            {
                Name = name,
                Price = price,
                CreatedOn = DateTime.Now
            };

            _connection.Execute("INSERT into " + table + " (Name, Price, CreatedOn) VALUES (?,?,?);", newGrocery.Name, newGrocery.Price, newGrocery.CreatedOn);

           // _connection.Insert(newGrocery);
        }
        
    }
}