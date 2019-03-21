using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChordPlayer : MonoBehaviour {

    AudioSource source;
    public AudioClip[] clips;
	void Start ()
    {
        source = GetComponent<AudioSource>();
	}
	
	public IEnumerator Chord()
    {
        source.clip = clips[Random.Range(0, clips.Length)];
        source.Play();

        yield return new WaitForSeconds(2);
        source.Stop();
    }

    public void PlayChord()
    {
        StartCoroutine(Chord());
    }
	void Update () {
		
	}
}
