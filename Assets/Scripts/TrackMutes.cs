using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMutes : MonoBehaviour
{
    private Track[] tracks;

    private AudioSource aud;


    private const int BEAT = 40;

    public static int beat = 40;
    public static int counter;
    void Awake()
    {
        counter = -20;
        aud = this.GetComponent<AudioSource>();
        tracks = this.GetComponentsInChildren<Track>();
        foreach (Track track in tracks)
        {
            track.WaitToUnmute();
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (tracks[0].currentState == Track.State.Muted)
            {
                print("Start Wait Time");
                tracks[0].WaitToUnmute();
            }
            else
            {
                print("Muted");
                tracks[0].Mute();
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        counter++;
        if (counter == beat * 4)
        {
            counter = 0;
        }
    }

    public static bool PhraseStart()
    {
        return (counter == 0 || counter == beat * 4);
    }

}
