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
    private string[] monthNames = new string[16];
    private string[] dayNames   = new string[5];

    //for now I'm jsut giving them default names
    //but ultimately the player can change and set these whenever 
    //and it's this way because it's fun, makes sense, and creates investment
    private void Awake()
    {
        //assign those months (we could load this from an ini later)
        monthNames[0] = "January";
        monthNames[1] = "Febuary";
        monthNames[2] = "Vene";
        monthNames[3] = "March";
        monthNames[4] = "April";
        monthNames[5] = "May";
        monthNames[6] = "Helos";
        monthNames[7] = "June";
        monthNames[8] = "July";
        monthNames[9] = "August";
        monthNames[10] = "Demuary";
        monthNames[11] = "September";
        monthNames[12] = "October";
        monthNames[13] = "November";
        monthNames[14] = "Hade";
        monthNames[15] = "December";

        //asign the days (also ini loadable shhh)
        dayNames[0] = "Monsday";
        dayNames[1] = "Secondsday";
        dayNames[2] = "Midsday";
        dayNames[3] = "Fourthsday";
        dayNames[4] = "Satfriday";
    }
}
