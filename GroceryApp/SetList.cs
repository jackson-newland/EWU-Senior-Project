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
using static Android.App.DatePickerDialog;

namespace GroceryApp
{
    [Activity(Label = "SetList")]
    public class SetList : Activity
    {
        ImageButton backButton;
        Button startDate, endDate, set;
        TextView startDateText, endDateText;
        EditText budget;
        bool startFocus;
        DatePicker calendar;
        GroceryAppDB _db = new GroceryAppDB();
        private int sYear, eYear, sMonth, eMonth, sDay, eDay;
                
        protected override void OnCreate(Bundle savedInstanceState) // creates the set list screen
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SetListScreen);

            calendar = FindViewById<DatePicker>(Resource.Id.setlDatePicker);
            calendar.DateChanged += Calendar_DateChanged;

            backButton = FindViewById<ImageButton>(Resource.Id.setlBackButton);
            backButton.Click += OpenSelectList;

            startDateText = FindViewById<TextView>(Resource.Id.setlStartDateText);
            endDateText = FindViewById<TextView>(Resource.Id.setlEndDateText);

            startDate = FindViewById<Button>(Resource.Id.setlStartDateButton);
            startDate.Click += StartDateClick;

            endDate = FindViewById<Button>(Resource.Id.setlEndDateButton);
            endDate.Click += EndDate_Click;

            budget = FindViewById<EditText>(Resource.Id.setlBudgetBox);


            set = FindViewById<Button>(Resource.Id.setlSetButton);
            set.Click += Set_Click;

          

        }

        private void Set_Click(object sender, EventArgs e)
        {
            try // trys to create a list and checks if the budget input is valid
            {
                if (ValidDate() < 0) // checks if the date is valid
                {
                    try // trys to create a list and checks if all entry boxes are filled and valid
                    {

                        string listName = startDateText.Text.ToString() + " - " + endDateText.Text.ToString();
                        if (!_db.DoesListExist(listName)) // checks if the list already exists in the database, if not creates a new list and returns the user to Select List Screen
                        {
                            _db.AddGroceryList(listName, Double.Parse(budget.Text.ToString()));
                            Intent data = new Intent();
                           // data.PutExtra("newList", listName);
                            data.SetData(Android.Net.Uri.Parse(listName));
                            //   Intent intent = new Intent(this, typeof(SelectList));
                            SetResult(Result.Ok, data);
                            Finish();
                           // StartActivity(intent);
                        }
                        else
                        {
                            ListAlreadyExists();
                        }

                    }
                    catch (FormatException)
                    {
                        InvalidList();
                    }
                }
                else
                {
                    InvalidList();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                InvalidList();
            }

        }
        private void Calendar_DateChanged(object sender, DatePicker.DateChangedEventArgs e) // sets start date or end date depending on which button is clicked and then disable the calendar
        {
            calendar.Visibility = (ViewStates)4; // sets calendar visibility to invisible

            if (startFocus)
            {
                sYear = e.Year;
                sMonth = e.MonthOfYear + 1;
                sDay = e.DayOfMonth;
                startDateText.Text = sMonth + "/" + sDay + "/" + sYear;
            }
            else
            {
                eYear = e.Year;
                eMonth = e.MonthOfYear + 1;
                eDay = e.DayOfMonth;
                endDateText.Text = eMonth + "/" + eDay + "/" + eYear;
            }
        }

        public void OpenSelectList(object sender, EventArgs e) // opens select list screen
        {
            //Intent intent = new Intent(this, typeof(SelectList));
            Intent data = new Intent();
            SetResult(Result.Canceled, data);
            Finish();
         //   StartActivity(intent);
        }

        public void StartDateClick(object send, EventArgs e) // enables calendar after start date button is clicked
        {
            calendar.Visibility = 0; // sets calendar visibility to true
            startFocus = true;

        }

        private void EndDate_Click(object sender, EventArgs e) // enables calendar after end date button is clicked
        {
            calendar.Visibility = 0; // sets calendar visibility to true
            startFocus = false;
        }


        public int ValidDate() // checks if the start date and end date are a valid timeline
        {
            DateTime start = new DateTime(sYear, sMonth, sDay);
            DateTime end = new DateTime(eYear, eMonth, eDay);

            return start.CompareTo(end);
        }

        public void InvalidList() // alert for invalid list inputs
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Invalid List!");
            alert.SetMessage("All entry boxes need to filled" +
                             "\nStart date and End date need to be valid" +
                             "\nBudget needs to be valid");

            alert.SetButton("OK", (c, ev) =>
            {

            });
            alert.Show();

        }

        public void ListAlreadyExists() // alert for a list that already exists
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("List already Exists!");
            alert.SetMessage("List range already exists" +
                            "\nPlease enter a different start date and end date");
            alert.SetButton("OK", (c, ev) =>
            {

            });
            alert.Show();
        }
    }
}