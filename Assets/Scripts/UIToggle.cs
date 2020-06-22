using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggle : MonoBehaviour
{
    public void toggle(GameObject myObject)
    {
        if(myObject.activeSelf == true)
        {
            myObject.SetActive(false);
        }

        else
        {
            myObject.SetActive(true);
        }
    }
}
