using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Track : MonoBehaviour
{
    public AudioSource aud;
    public TrackMutes mutes;
    public State currentState = State.Muted;

    public enum State
    {
        Muted,
        UnMuted,
        Waiting
    }

    void Awake()
    {
        aud = this.GetComponent<AudioSource>();
        mutes = this.GetComponentInParent<TrackMutes>();

    }

    void Update()
    {
        if (currentState == State.Waiting)
        {
            //print("Waiting");
            if (mutes.PhraseStart())
            {
                if (aud.isPlaying)
                    UnMute();
                else
                {
                    aud.Play();
                    currentState = State.UnMuted;
                    Mute();
                }

            }
            
        }
    }

    public void WaitToUnmute()
    {
        currentState = State.Waiting;
    }

    public void Mute()
    {
        currentState = State.Muted;
        aud.mute = true;
    }

    private void UnMute()
    {
        currentState = State.UnMuted;
        aud.mute = false;
    }


}
