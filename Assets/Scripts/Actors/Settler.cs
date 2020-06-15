using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settler : MonoBehaviour
{
    private string characterName;
    private string profession;

    private string[] traits = new string[2];

    //statBoard (this is universal: food/day, batSpend, batReahcrge, repairChance, healChance, animal handeling)
    private float[] stats = new float[6];

    //disease
    private bool isSick;
    private int daysSick;

    //injury
    //note, the current game doesn't have injury
    private bool isInjured;
    private int daysInjured;

    private void Awake()
    {
        //default assignments
        isSick = false;
        daysSick = 0;

        isInjured = false;
        daysInjured = 0;

        for (int i = 0; i < 6; i++)
        {
            stats[i] = 0;
        }

        //hey so a really good case for ini integration is right here
        //anywho
        //pick a settler
        //roll for his profession (1 of 4) incriment their trates accordingly.

        //(for ini implimentation, load up the custom professions into an array, use that for a roll size, 
        //then directly pull from that index to store the profession as opposed to doing the if/elses
        int prof = Random.Range(0, 3);
        switch (prof)
        {
            case 0:
             profession = "Ecologist";
            //sets food/day to plus 4
            //(for the ini, you could feed values here)
            //maybe this should modify animal handeling more?
            stats[0] += 4;
            break;

            case 1:
            profession = "Electrician";
            //bat recharge increased for group
            stats[2] += 2;
            break;

            case 2:
            profession = "Mechanic";
            //repair chance for group increased by %
            stats[3] += (float)0.1;
            break;

            case 3:
            profession = "Doctor";
            //heal chance for group increased by %
            stats[4] += (float)0.1;
            break;

            default:
            Debug.Log("1st switch f'd up in settler awake method");
            break;
        }

        //roll for each trait which will come from an instance of settler traits so it doesn't have to exist all the time here
        for (int i = 0; i < 2; i++)
        {
            int stat = Random.Range(0, 7);
            switch (stat)
            {
                case 0:
                    traits[i] = "Eats a lot";
                    //food expend up +1/day
                    stats[0] += 1;
                    break;

                case 1:
                    traits[i] = "Eats little";
                    //food expend down +1/day
                    stats[0] -= 1;
                    break;

                case 2:
                    traits[i] = "Energy Concious";
                    //hey note, if this is making negative enery expendatures,
                    //add logic in the spending loop to make the battery spend atleast
                    //1 charge. remember, speed changes expendature, so squirrling around with this value
                    //means you get other issues later on. 
                    stats[1] -= 1;
                    break;

                case 3:
                    traits[i] = "Leaves Light On";
                    //energy expendature up +1/day
                    stats[1] += 1;
                    break;

                case 4:
                    traits[i] = "Handyman";
                    //repait chance group up %
                    stats[3] += .05f;
                    break;

                case 5:
                    traits[i] = "Bad with tools";
                    //repair chance down %
                    stats[3] -= 05f;
                    break; 

                case 6:
                    traits[i] = "First-Aid Certified";
                    //heal chance for group up %
                    stats[4] += .05f;
                    break;

                case 7:
                    traits[i] = "Hates Needles";
                    //heal chance for group down %
                    stats[4] -= .05f;
                    break;

                default:
                Debug.Log("2nd switch f'd up in settler awake method");
                break;
            }

        }

    }

    //sick logic (not sure how I wanna process this just yet)
    public void setIsSick(bool sick)
    {
        isSick = sick;
    }

    public void addDaysSick()
    {
        daysSick++;
    }
}
