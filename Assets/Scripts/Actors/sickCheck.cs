﻿using System.Collections;
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
    //all of these sick check methods are essentially the same thing archetecturally so they should be easy to follow

    //every morning we check to see if a settler gets sick
    public List<string[]> sickRoll(CAR targetCar)
    {
        int rationScore = targetCar.getRationScore();
        float rationMod = .0f;
        List<string[]> sickList = new List<string[]>();

        if (rationScore <= 0)
        {
             rationMod = .2f;
              
        }

        else if (rationScore <= 2)
        {
            rationMod = .15f;
        }

        else if (rationScore <=4)
        {
            rationMod = .1f;
        }

        else if (rationScore <= 6)
        {
            rationMod = .075f;
        }

        else if (rationScore <= 8)
        {
            rationMod = .05f;
        }

        else if (rationScore <= 10)
        {
            rationMod = .025f;
        }
        
        else if (rationScore <= 12)
        {
            rationMod = .0f;
        }

        else
        {
            rationMod = .0f;
            Debug.Log("Error in ration math in sick, you somehow have a score above 12");
        }

    
        //if getSettler(i) !isSick
        for(int i = 0; i < targetCar.getSettlerList().Count; i ++)
        {
            if (!targetCar.getSettlerFromList(i).getIsSick() && !targetCar.getSettlerFromList(i).getIsDead())
            {
                //assigning roll and using it incase I want to check the rolls with debug astatements later
                float roll = rolld100();
                if (roll < (baseSickChance + rationMod)) //also make sure to add .05 per land mark
                {
                    targetCar.getSettlerFromList(i).setIsSick(true);
                    sickList.Add(new string[]{ "Settler sick!", targetCar.getSettlerFromList(i).getName() + " got sick!" });
                }
            }
        }
        return sickList;
    }

    //every morning (before sick roll) we check to see if a settler's disease kills them
    public List<string[]> killRoll(CAR targetCar)
    {
        int rationScore = targetCar.getRationScore();
        float rationMod = .0f;
        List<string[]> dieList = new List<string[]>();

        if(rationScore <= 0)
        {
            rationMod = .2f;
        }

        if (rationScore <= 2)
        {
            rationMod = .15f;
        }

        else if (rationScore <= 4)
        {
            rationMod = .10f;
        }

        else if (rationScore <= 6)
        {
            rationMod = .05f;
        }

        else if (rationScore <= 8)
        {
            rationMod = .0f;
        }

        else if (rationScore <= 10)
        {
            rationMod = -.025f;
        }

        else if (rationScore <= 12)
        {
            rationMod = -.05f;
        }

        else
        {
            rationMod = .0f;
            Debug.Log("Error in ration math in kill block, you somehow have a score above 12");
        }

        for (int i = 0; i < targetCar.getSettlerList().Count; i++)
        {
            if (targetCar.getSettlerFromList(i).getIsSick() && !targetCar.getSettlerFromList(i).getIsDead())
            {
                float roll = rolld100();
                if (roll < (baseKillChance + rationMod + targetCar.getSettlerFromList(i).getDaysSickMod())) //also make sure to add .05 per land mark
                {
                    targetCar.getSettlerFromList(i).setIsDead(true);
                    dieList.Add(new string[] { "Settler dead!", targetCar.getSettlerFromList(i).getName() + " died from disease" });
                    targetCar.removeSettler(i);
                }
            }
        }
        return dieList;


    }

    //every morning (bfore sick roll) we check to see if a settler's disease spreads to another settler
    public List<string[]> spreadRoll(CAR targetCar)
    {
        int rationScore = targetCar.getRationScore();
        float rationMod = .0f;
        List<string[]> spreadList = new List<string[]>();

        if (rationScore <= 0)
        {
            rationMod = .2f;
        }

        if (rationScore <= 2)
        {
            rationMod = .15f;
        }

        else if (rationScore <= 4)
        {
            rationMod = .10f;
        }

        else if (rationScore <= 6)
        {
            rationMod = .05f;
        }

        else if (rationScore <= 8)
        {
            rationMod = .0f;
        }

        else if (rationScore <= 10)
        {
            rationMod = -.025f;
        }

        else if (rationScore <= 12)
        {
            rationMod = -.05f;
        }

        else
        {
            rationMod = .0f;
            Debug.Log("Error in ration math in spread block, you somehow have a score above 12");
        }
        

        for (int i = 0; i < targetCar.getSettlerList().Count; i++)
        {
            bool isHealthySettler = false;
            List<Settler> infectList = new List<Settler>();
            //make sure someone CAN get sick 
            for (int j = 0; j < targetCar.getSettlerList().Count; j++)
            {
                if(!targetCar.getSettlerFromList(j).getIsSick() && !targetCar.getSettlerFromList(i).getIsDead())
                {
                    isHealthySettler = true;
                    infectList.Add(targetCar.getSettlerFromList(j));
                }
            }
            //note, this will run if there is a dead settler in the party at all
            if(!isHealthySettler)
            {
                Debug.Log("There is nobody to infect so far!");
                Debug.Log("At settler: " + i);
            }

           if (targetCar.getSettlerFromList(i).getIsSick() && isHealthySettler && !targetCar.getSettlerFromList(i).getIsDead())
            {
                int infectIndex = Random.Range(0, (infectList.Count - 1));
                float roll = rolld100();
                if (roll < (baseSpreadChance + rationMod + targetCar.getSettlerFromList(infectIndex).getDaysSickMovingMod())) //also make sure to add .05 per land mark
                {
                    //pick a random settler of those that are healthy and infect them
                    
                    infectList[infectIndex].setIsSick(true);
                    spreadList.Add(new string[] { "Disease Spread!", targetCar.getSettlerFromList(infectIndex).getName() + " got sick from " + targetCar.getSettlerFromList(i).getName() });
                }
            }
        }
        return spreadList;
    }

    //every morning (before sickroll) we check to see if a settler is no longer sick
    public List<string[]> cureRoll(CAR targetCar)
    {
        int rationScore = targetCar.getRationScore();
        float rationMod = .0f;
        List<string[]> cureList = new List<string[]>();

        if (rationScore <= 0)
        {
            rationMod = -.2f;
        }

        if (rationScore <= 2)
        {
            rationMod = -.15f;
        }

        else if (rationScore <= 4)
        {
            rationMod = -.10f;
        }

        else if (rationScore <= 6)
        {
            rationMod = -.05f;
        }

        else if (rationScore <= 8)
        {
            rationMod = .0f;
        }

        else if (rationScore <= 10)
        {
            rationMod = .025f;
        }

        else if (rationScore <= 12)
        {
            rationMod = .05f;
        }

        else
        {
            rationMod = .0f;
            Debug.Log("Error in ration math in kill block, you somehow have a score above 12");
            Debug.Log(rationScore);
        }

        for (int i = 0; i < targetCar.getSettlerList().Count; i++)
        {
            if (targetCar.getSettlerFromList(i).getIsSick() && !targetCar.getSettlerFromList(i).getIsDead())
            {
                float roll = rolld100();
                if (roll < (baseCureChance + rationMod + .05* targetCar.getSettlerFromList(i).getDaysSickNotMovingMod())) 
                {
                    targetCar.getSettlerFromList(i).setIsSick(false);
                    cureList.Add(new string[] { "Settler cured!", targetCar.getSettlerFromList(i).getName() + "Is no longer sick from disease" });
                }
            }
        }
        return cureList;
    }

    //dice
    private float rolld100()
    {
        return Random.Range(0f,1f);
    }

}
