using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    private int capacity;
    private int charge;

    public void recharge(int amount)
    {
        charge += amount;
    }

    public void degrade(int amount)
    {
        capacity -= amount;
    }
}
