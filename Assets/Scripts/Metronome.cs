using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    int counter;
    private AudioSource aud;

    //Calculate (60 * FixedTimestep) / x

    //FixedTimestep = 50
    //Beat = 25: 120bpm
    //Beat = 30: 100bpm
    //Beat = 31.25: 96bpm
    //Beat = 37.5: 80bpm
    //Beat = 40: 75bpm
    //Beat = 46.875: 64bpm
    //Beat = 50: 60bpm
    private int beat = 40;
    private decimal test = 1.0m;

    void Awake()
    {
        counter = 0;
        aud = this.GetComponent<AudioSource>();
    }

    void Start()
    {
        //for (int i = 60; i <= 120; i++)
        //{
        //    decimal b = 3600m / (decimal)i;
        //    print("BPM is "+i+", beat is "+b);
        //}
    }
    void FixedUpdate()
    {
        counter++;

        if (counter == beat * 4)
        {
            print("Fourth Beat");
            aud.Play();
        }

        else if (counter % beat == 0)
        {
            print("Beat");
            aud.Play();
        }

        if (counter == beat * 4)
            counter = 0;
    }
}
