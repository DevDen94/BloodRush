using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class VolumeControlerInMultiplayer : MonoBehaviour
{
    public AudioSource BgMusic;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MusicCouroutine());
    }

    // Update is called once per frame
   
    public IEnumerator MusicCouroutine()
    {
        
        yield return new WaitForSeconds(0.0001f);
        foreach (AudioSource a in FindObjectsOfType<AudioSource>())
        {
            a.volume = PlayerPrefs.GetFloat("Sounds");
            if (a.volume == 0)
            {
                a.mute = true;
            }
            else
            {
                a.mute = false;
            }
            BgMusic.volume= PlayerPrefs.GetFloat("Music");
           
        }
        StartCoroutine(MusicCouroutine());
    }
}
