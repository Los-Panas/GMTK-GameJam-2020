using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class DynamicAudio : MonoBehaviour
{

    int num_soundtrack;
    int num_sub_track;


    public static DynamicAudio instance;

    void Awake ()
    {
        // Don't Destroy on load another scene, and deletes if there's another AudioManager
        if (instance == null)
            instance = this;
        else
        {
            DestroyObject(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);


        // Initial soundtrack
        num_soundtrack = 1;
        num_sub_track = Random.Range(1, 4);

        FindObjectOfType<AudioManager>().Play("Soundtrack" + num_soundtrack.ToString() + "." + num_sub_track.ToString());
    }

    private void Update()
    {
        if (!FindObjectOfType<AudioManager>().IsPlaying("Soundtrack" + num_soundtrack.ToString() + "." + num_sub_track.ToString()))
            NextSoundtrack();
        
    }

    void NextSoundtrack()
    {
        // Next sountrack
        if (num_soundtrack >= 4)
            num_soundtrack = 1;
        else
            num_soundtrack++;

        // Random sub_track
        if(num_soundtrack == 1)
            num_sub_track = Random.Range(1, 4);
        if (num_soundtrack == 2)
            num_sub_track = Random.Range(1, 5);
        if (num_soundtrack == 3)
            num_sub_track = Random.Range(1, 4);
        if (num_soundtrack == 4)
            num_sub_track = 1;

        // Play track
        FindObjectOfType<AudioManager>().Play("Soundtrack" + num_soundtrack + "." + num_sub_track);
    }
}
