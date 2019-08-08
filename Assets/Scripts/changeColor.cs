using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeColor : MonoBehaviour
{
    private Toggle[] toggles;
    public TrackMutes mutes;

    void Awake()
    {
        toggles = this.GetComponentsInChildren<Toggle>();
        toggles[0].isOn = true;
        toggles[1].isOn = true;
        toggles[2].isOn = true;
    }



    // Update is called once per frame
    void Update()
    {
        if (mutes.IsActive(0) == true)
        {
          toggles[0].isOn = true;
        }
        else
        {
          toggles[0].isOn = false;
        }
        if (mutes.IsActive(1) == true)
        {
            toggles[1].isOn = true;
        }
        else
        {
            toggles[1].isOn = false;
        }
        if (mutes.IsActive(2) == true)
        {
            toggles[2].isOn = true;
        }
        else
        {
            toggles[2].isOn = false;
        }
    }
}
