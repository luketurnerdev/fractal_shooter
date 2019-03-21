using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmplitudeDetector : MonoBehaviour {

    public MusicAnalyser musicAnalyser;
    private AudioSource audioSource;
    public float updateStep = 0.8f;
    public int sampleDataLength;
    public float amplitude { get; private set; }
    private float currentUpdateTime = 0f;
    private List<float> loudnesses = new List<float>();
    private float clipLoudness;
    private float[] clipSampleData;

   
    void Start () {
		audioSource = musicAnalyser.audioSource;
        //Debug.Log(musicAnalyser.audioSource.clip.frequency);
        //Debug.Log(musicAnalyser.sampleRate);
        sampleDataLength = 44100;//musicAnalyser.sampleRate * 10;
        clipSampleData = new float[sampleDataLength];

    }
    public void RefreshAmplitude()
    {
        amplitude = 0;
        foreach (float level in loudnesses)
        {
            amplitude += level;
        }
        amplitude /= loudnesses.Count;
        loudnesses.Clear();
    }
	
	
	void Update () {
        currentUpdateTime += Time.deltaTime;
        if (currentUpdateTime >= updateStep)
        {
            currentUpdateTime = 0f;
            audioSource.clip.GetData(clipSampleData, audioSource.timeSamples); //analyses 1 sec of data at a time and avg the results
            clipLoudness = 0f;
            foreach (var sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for
            //Debug.Log(clipLoudness);
            loudnesses.Add( clipLoudness);
            
        }
    }
}
