using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    //Audio Source
    private AudioSource aud;

    //TrackMutes object
    //private TrackMutes mutes;

    //Current state of the track
    public State currentState = State.Muted;

    //Time in seconds that track takes to fade in
    public float fadeTime = 1.5f;

    //Enum describing possible states of the track
    public enum State
    {
        Muted,
        UnMuted,
        Waiting,
        FadingIn,
        FadingOut
    }

    /// <summary>
    /// Gets the audio source and trackmute objects upon creation
    /// </summary>
    void Awake()
    {
        aud = this.GetComponent<AudioSource>();
        //mutes = this.GetComponentInParent<TrackMutes>();

    }

    /// <summary>
    /// Calls different update functions depending on the current state of the track
    /// </summary>
    void Update()
    {
        //If waiting to be unmuted, call WaitUpdate
        if (currentState == State.Waiting)
        {
            WaitUpdate();
        }

        //If fading the track in, call FadeInUpdate
        if (currentState == State.FadingIn)
        {
            FadeInUpdate();
        }

        //If fading the track out, call FadeOutUpdate
        if (currentState == State.FadingOut)
        {
            FadeOutUpdate();
        }
    }

    /// <summary>
    /// Sets the state of the track to waiting
    /// Called by TrackMutes
    /// </summary>
    public void WaitToUnmute()
    {
        currentState = State.Waiting;
    }

    /// <summary>
    /// Sets the state of the track to fadingout
    /// Called by TrackMutes
    /// </summary>
    public void Mute()
    {
        print("Fading Out");
        currentState = State.FadingOut;
    }

    /// <summary>
    /// Sets the state of the track to fadingin
    /// Called by TrackMutes
    /// </summary>
    private void UnMute()
    {
        currentState = State.FadingIn;
        aud.mute = false;
        aud.volume = 0;
    }


    /// <summary>
    /// If the track is playing, waits until the start of a phrase to start unmuting the track
    /// If the track has never been played (i.e. start of the level), plays the track
    /// Called by Update if current state is Waiting
    /// </summary>
    private void WaitUpdate()
    {
        //If it's the start of a phrase
        if (TrackMutes.PhraseStart())
        {
            //If the track has never even been started (start of the level)
            if (!aud.isPlaying)
            {
                aud.Play();
                currentState = State.UnMuted;
            }

            //Otherwise, start the umute process
            else
            {
                print("Fading In");
                UnMute();
            }
        }
    }

    /// <summary>
    /// Fades in the audio of the track until it is at max volume
    /// Called by Update if current state is FadingIn
    /// </summary>
    private void FadeInUpdate()
    {
        //If the voluume is not at max, increase it based on time since the last frame
        if (aud.volume < 1.0)
        {
            aud.volume += Time.deltaTime / fadeTime;
        }

        //If the volume is at max, set state to UnMuted
        else
        {
            aud.volume = 1.0f;
            currentState = State.UnMuted;
        }
    }

    /// <summary>
    /// Fades out the audio of the track until it is at 0
    /// Called by Update if current state is FadingOut
    /// </summary>
    private void FadeOutUpdate()
    {
        //If the volume is not at 0, decrease it based on time since the last frame
        if (aud.volume > 0)
        {
            aud.volume -= Time.deltaTime / fadeTime;
        }

        //If the volume is at 0, set mute to true and set state to Muted
        else
        {
            aud.volume = 0;
            aud.mute = true;
            currentState = State.Muted;
        }
    }


}
