using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipScreen : MonoBehaviour
{
    public CAR selectedCar;

    public TMPro.TextMeshProUGUI moduleText;
    public TMPro.TextMeshProUGUI cargoText;

    // display for the settler-imposed stats of the car
    public TMPro.TextMeshProUGUI foodMod;
    public TMPro.TextMeshProUGUI battSpendMod;
    public TMPro.TextMeshProUGUI rechargeMod;
    public TMPro.TextMeshProUGUI repairMod;
    public TMPro.TextMeshProUGUI healMod;
    public TMPro.TextMeshProUGUI animalHandlingMod;

    //display the starting resources
    public TMPro.TextMeshProUGUI foodCount;
    public TMPro.TextMeshProUGUI battCapacity;
    public TMPro.TextMeshProUGUI mediCount;
    public TMPro.TextMeshProUGUI metalCount;

    //I know this is done totally different from the cargo stuff but that's due to the fact that
    //stats in a car are array based and resoruces are not. This is- NOT clean code.
    private string[] moduleNames;
    

    //this will get passed directly to the car
    private string selectedModuleName;
    private float[] selectedModuleStats;
    private int moduleSelected;

    //these are the amounts we will increase the resources by based on which cargo is selected
    //1 is food, 2 is battCharge, 3 is medi, 4 is metal
    public int[] cargoAmounts;
    public int cargoSelected; //this is toggled by the arrow buttons on the cargo select field

    public void Awake()
    {
        moduleNames = new string[4];
        moduleNames[0] = "Food Synth";
        moduleNames[1] = "Solar Array";
        moduleNames[2] = "Med Bay";
        moduleNames[3] = "Workshop";

        selectedModuleStats = new float[6];

        createSelectedModuleStats();
        refreshInfo();
    }

    public void refreshInfo()
    {
        float[] tempStats = new float[6];
        for(int i = 0; i < tempStats.Length; i++)
        {
            //since the temp stats array is localized to refreshInfo() we can just do addition like this everytime we refresh
            tempStats[i] = selectedModuleStats[i] + selectedCar.getStats()[i];
        }

        //module display
        moduleText.text = moduleNames[moduleSelected];
   
        //the car's stat for food consumption is just the modifiers. To show how much food the player will lose per turn, 
        //you need to combine that with -rationLevel*livingSettlercount
        foodMod.text = (tempStats[0].ToString());
        battSpendMod.text = "-" + (tempStats[1]);
        rechargeMod.text = "+" + (tempStats[2] + 1).ToString();
        repairMod.text = "+" + tempStats[3].ToString();
        healMod.text = "+" + tempStats[4].ToString();
        animalHandlingMod.text = "+" + tempStats[5].ToString();
        
        //so for this we add the settler modifiers 

        //##Resource Amount on Start Screen
        //the current count of resources isn't drawing from the car's actual inventory,
        //instead we're just representing what it WILL look like once the plyaer hits the "start" button
        //this way we don't fuss with adding and subtracting things unessisarily inside the car itself  
        //More specifically: we're adding the car's default amount to the amount we know we're giving it via the kit screen
        if(cargoSelected == 0)
        {
            foodCount.text = (selectedCar.defaultFood + cargoAmounts[cargoSelected]).ToString();
            cargoText.text = "Food Crate";

            battCapacity.text = selectedCar.defaultBatts.ToString();
            mediCount.text = selectedCar.defaultMedi.ToString();
            metalCount.text = selectedCar.defaultMetal.ToString();
            
        }

        if (cargoSelected == 1)
        {
            battCapacity.text = (selectedCar.defaultBatts + cargoAmounts[cargoSelected]).ToString();
            cargoText.text = "Large Battery";

            foodCount.text = selectedCar.defaultFood.ToString();
            mediCount.text = selectedCar.defaultMedi.ToString();
            metalCount.text = selectedCar.defaultMetal.ToString();
        }

        if (cargoSelected == 2)
        {
            mediCount.text = (selectedCar.defaultMedi + cargoAmounts[cargoSelected]).ToString();
            cargoText.text = "Medi Crate";

            foodCount.text = selectedCar.defaultFood.ToString();
            battCapacity.text = selectedCar.defaultBatts.ToString();
            metalCount.text = selectedCar.defaultMetal.ToString();
        }

        if (cargoSelected == 3)
        {
            metalCount.text = (selectedCar.defaultMetal + cargoAmounts[cargoSelected]).ToString();
            cargoText.text = "Metal Crate";

            foodCount.text = selectedCar.defaultFood.ToString();
            battCapacity.text = selectedCar.defaultBatts.ToString();
            mediCount.text = selectedCar.defaultMedi.ToString();
        }
    }

    //###Cargo Toggling for ui
    public void toggleCargoBack()
    {
        if(cargoSelected == 0)
        {
            cargoSelected = 3;
        }

        else
        {
            cargoSelected--;
        }

        refreshInfo();
    }

    public void toggleCargoForward()
    {
        if(cargoSelected == 3)
        {
            cargoSelected = 0;
        }

        else
        {
            cargoSelected++;
        }

        refreshInfo();
    }

    //###Module Toggling for ui
    public void toggleModuleForward()
    {
        if(moduleSelected == 3)
        {
            moduleSelected = 0;
        }

        else
        {
            moduleSelected++;
        }

        createSelectedModuleStats();
        refreshInfo();
    }

    public void toggleModuleBackwards()
    {
        if(moduleSelected == 0)
        {
            moduleSelected = 3;
        }

        else
        {
            moduleSelected--;
        }

        createSelectedModuleStats();
        refreshInfo();
    }

    //this method is very hamfisted and only accounts for four possible modules
    public void createSelectedModuleStats()
    {
        switch(moduleSelected)
        {
            case 0:
                selectedModuleStats[0] = 4;
                selectedModuleStats[1] = 0;
                selectedModuleStats[2] = 0;
                selectedModuleStats[3] = 0;
                selectedModuleStats[4] = 0;
                selectedModuleStats[5] = 0;
                selectedModuleName = "Food Synth";
                break;

            case 1:
                selectedModuleStats[0] = 0;
                selectedModuleStats[1] = 0;
                selectedModuleStats[2] = 2;
                selectedModuleStats[3] = 0;
                selectedModuleStats[4] = 0;
                selectedModuleStats[5] = 0;
                selectedModuleName = "Adv. Recharger";
                break;

            case 2:
                selectedModuleStats[0] = 0;
                selectedModuleStats[1] = 0;
                selectedModuleStats[2] = 0;
                selectedModuleStats[3] = 0;
                selectedModuleStats[4] = .1f;
                selectedModuleStats[5] = 0;
                selectedModuleName = "Med Bay";
                break;

            case 3:
                selectedModuleStats[0] = 0;
                selectedModuleStats[1] = 0;
                selectedModuleStats[2] = 0;
                selectedModuleStats[3] = .1f;
                selectedModuleStats[4] = 0;
                selectedModuleStats[5] = 0;
                selectedModuleName = "Workshop";
                break;

            default:
                Debug.Log("Error in module stat assignment switch case inside of start screen script.");
                break;

        }
    }

    //this is the method that actually applies the stats in our temporary blocks to the car
    public void applyKit()
    {
        //this init method only covers modules, atleast for now, I wanna change cargo to array storage
        selectedCar.startScreenInit(selectedModuleName, selectedModuleStats);

        //this handles the cargo
        //keep in mind our public array of cargo amounts has all the values we want set already
        if(cargoSelected == 0)
        {
            selectedCar.addFood(cargoAmounts[0]);
        }

        else if (cargoSelected == 1)
        {
            //the starting car will always have a starting charge congruent with its starting capacity
            //since the defaults are equal im the car script, applying the +4 (or whatever) to capacity to the charge works
            selectedCar.addBatteryCapacity(cargoAmounts[1]);
            selectedCar.addBatteryCharge(cargoAmounts[1]);
        }

        else if (cargoSelected == 2)
        {
            selectedCar.addMedi(cargoAmounts[2]);
        }

        else if (cargoSelected == 3)
        {
            selectedCar.addMetal(cargoAmounts[3]);
        }

        else
        {
            Debug.Log("Error in equip screen applyKit method for cargo: there is a cargo selected not between 3 and 0");
        }
    }
}
