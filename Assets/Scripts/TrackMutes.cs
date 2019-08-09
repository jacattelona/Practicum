using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackMutes : MonoBehaviour
{
    private Track[] tracks;

    private AudioSource aud;

    public int counter;
    private const int BEAT = 40;

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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (tracks[0].currentState == Track.State.Muted)
                tracks[0].WaitToUnmute();
            else
                tracks[0].Mute();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (tracks[1].currentState == Track.State.Muted)
                tracks[1].WaitToUnmute();
            else
                tracks[1].Mute();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (tracks[2].currentState == Track.State.Muted)
                tracks[2].WaitToUnmute();

            else
                tracks[2].Mute();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (tracks[3].currentState == Track.State.Muted)
            {
                tracks[3].WaitToUnmute();
                tracks[4].Mute();
            }

            else
                tracks[3].Mute();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (tracks[4].currentState == Track.State.Muted)
            {
                tracks[4].WaitToUnmute();
                tracks[3].Mute();
            }

            else
                tracks[4].Mute();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        counter++;
        if (counter == BEAT * 4)
        {
            counter = 0;
        }
    }

    public bool PhraseStart()
    {
        return (counter == 0 || counter == BEAT * 4);
    }

    public bool IsActive(int track)
    {
        Track t = tracks[track];

        if (t.currentState == Track.State.UnMuted)
            return true;

        else
            return false;
    }

    public bool WithinRange(int range)
    {
        int off = counter % BEAT;
        if (off < range || off > BEAT - range){
            return true;
        }
        return false;
    }

}
