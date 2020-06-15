using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//And you may say to yourself: "Gourd why do you need this?"
//To wich I respond: because I have no idea how the fuck we're doing anything yet and it never hurts to be flexible
//...unless you don't stretch often.
public class Time : MonoBehaviour
{
    //the player can name the months and days. 
    //there are 4 months with 4 weeks with 5 days, 
    //the playr can name them all because why not.
    private string[] monthNames = new string[12];
    private string[] dayNames   = new string[7];

    //the actual time
    private int hour;
    private int day;
    private int month;
    private int date;

    private void Awake()
    {
        hour = 800;
        day = 0;
        date = 0;
        month = 3;
        
        //assign those months (we could load this from an ini later)
        monthNames[0] = "January";
        monthNames[1] = "Febuary";
        monthNames[2] = "March";
        monthNames[3] = "April";
        monthNames[4] = "May";
        monthNames[5] = "June";
        monthNames[6] = "July";
        monthNames[7] = "August";
        monthNames[8] = "September";
        monthNames[9] = "October";
        monthNames[10] = "November";
        monthNames[11] = "December";
 

        //asign the days (also ini loadable shhh)
        dayNames[0] = "Monday";
        dayNames[1] = "Tuesday";
        dayNames[2] = "Wendsday";
        dayNames[3] = "Thursday";
        dayNames[4] = "Friday";
        dayNames[5] = "Saturday";
        dayNames[6] = "Sunday";
     }

    private void calcTime()
    {
        //changing days
        if(hour == 2400)
        {
            hour = 0;

            //day name
            if(day == 6)
            {
                day = 0;
            }

            else
            {
                day++;
            }

            //change date, calculate the month stuff
            if (date == 30)
            {
                date = 1;

                if(month == 12)
                {
                    month = 1;
                }

                else
                {
                    month++;
                }
            }

            else
            {
                date++;
            }
        }
    }

    //hour is 0 thru 24
    public int getHour()
    {
        return hour;
    }

    //day is 0 thru 6
    public string getDay()
    {
       // Debug.Log("Attempting to return day: " + day);
        return dayNames[day];//.Substring(0, 2);
    }

    //month is 1 thru 12
    public string getMonth()
    {
       // Debug.Log("Attempting to return month: " + month);
        return monthNames[month - 1];//.Substring(0, 3);
    }

    public int getMonthNum()
    {
        return month;
    }

    //date is 1 thru 30
    public int getDate()
    {
        return date;
    }

    public void advanceTime()
    {
        hour += 400;
        calcTime();
    }


}
