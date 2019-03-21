using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractalAudio : MonoBehaviour {

    private AudioSource audio;
    private OnsetDetector onset;
    private float modifier = 0.05f;
    private Color color1 = new Color(0, 1, 1);
    private Color color2 = new Color(1, 0.4f, 0.7f);
	// Use this for initialization
	void Start () {
        onset = FindObjectOfType<OnsetDetector>();
        audio = gameObject.AddComponent<AudioSource>();
        audio.clip=Resources.Load("sfx_aura") as AudioClip;
        audio.loop = true;
        audio.spatialBlend = 1.0f;
        audio.Play();
	}
	
	void Update () {

        //Tristan, I have added the null check here because it was throwing an error when shooting a bullet. 6/10/18

        if (onset != null)
        {
            if (onset.BPM % 5 == 1)
            {
                //Debug.Log("bpm%50 happened bpm = " + onset.BPM);
                modifier = -modifier;
            }
        }
		
        if ((audio.pitch >= 0.5 && modifier <0 )|| (audio.pitch <= 2.0 && modifier > 0))
        {
            audio.pitch += modifier;
        }
        if (modifier >0)
        {
            GetComponent<Renderer>().material.color = Color.Lerp(color1,color2,audio.pitch);
        }
        if (modifier < 0)
        {
            GetComponent<Renderer>().material.color = Color.Lerp(color2, color1, audio.pitch);
        }
        
	}
}
