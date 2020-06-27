using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sickCheck
{
    public float baseSickChance = .05f;
    public float baseCureChance = .1f;
    public float baseKillChance = .0f;
    public float baseSpreadChance = .0f;

    //these all return 2d strings as a borrow from how we do trail events
    //this way we can use the method call in CAR to do a call to the UI manager to show the event window
    //very swank if I do say so myself
    public List<string[]> sickRoll(CAR targetCar)
    {
        int rationScore = targetCar.getRationScore();
        float rationMod = .0f;
        List<string[]> sickList = new List<string[]>();

        if (rationScore <= 2)
        {
             rationMod = .2f;
              
        }

        else if (rationScore <= 4)
        {
            rationMod = .15f;
        }

        else if (rationScore <=6)
        {
            rationMod = .1f;
        }

        else if (rationScore <= 8)
        {
            rationMod = .075f;
        }

        else if (rationScore <= 10)
        {
            rationMod = .05f;
        }

        else if (rationScore <= 12)
        {
            rationMod = .0f;
        }

        else
        {
            rationMod = .0f;
            Debug.Log("Error in ration math, you somehow have a score above 12");
        }




        
        //if getSettler(i) !isSick
        for(int i = 0; i < targetCar.getSettlerList().Count; i ++)
        {
            if (!targetCar.getSettlerFromList(i).getIsSick())
            {
                float roll = rolld100();
                if (rolld100() < (baseSickChance + rationMod)) //also make sure to add .05 per land mark
                {
                    targetCar.getSettlerFromList(i).setIsSick(true);
                    sickList.Add(new string[]{ "Settler sick!", targetCar.getSettlerFromList(i).getName() + " got sick!" });
                }
            }
        }
        return sickList;
    }


    public List<string[]> killRoll(Settler targetSettler, CAR targetCar)
    {
        float rationMod;
        //switch ration mod: 20, 10, 0, -5 : 0, 1, 2, 3
        //do a d100 roll
        //kill settler if roll loses to  ration mod + settker perdays sick
        return new List<string[]>();
    }

    public List<string[]> spreadRoll(Settler targetSettler,  CAR targetCar)
    {
        float rationMod;
        //switch case ration mod: 20, 10, 0, -5 : 0, 1, 2, 3
        //do a d100 roll
        //if settler fails a + per day moving(+5) + ration mod,
        //then get a list of all of the settlers, and try and find one that is sick
        //once they are sick, return a string explaining who got infected by who
        //if they don't spread it, then just bottom out the case and return a void string
        //then make sure car is checking for something void, if it's void tell it to not run it, SPECIFICALLY at this step

        return new List<string[]>();
    }

    public List<string[]> cureRoll(Settler targetSettler, CAR targetCar)
    {
        float rationMod;
        //switch case ration mod: -20, -10, 0, 5 : 0, 1, 2, 3
        //do a d100 roll
        //if settler fails a + per day stopped(+5) + ration mod, + 10base, they are cured
        //once they are cured, return a string explaining it
        //also reset that villagers sick day counters
        return new List<string[]>();
    }

    //dice
    private float rolld100()
    {
        return Random.Range(0f,1f);
    }

}
