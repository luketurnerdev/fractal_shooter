using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAnalyser : MonoBehaviour {

    public AudioSource audioSource;
    public int sampleRate;
    private int spawnTimer = 30;
    private float timer = 0;
    private OnsetDetector onset;
    private AmplitudeDetector amplitude;
    private EnemySpawner spawner;
    private int spawnCount;
    //public int spawnModifer = 10;
    // Use this for initialization
    void Start() {
        sampleRate = audioSource.clip.frequency;
        onset = FindObjectOfType<OnsetDetector>();
        amplitude = FindObjectOfType<AmplitudeDetector>();
        spawner = FindObjectOfType<EnemySpawner>();
    }

    // Update is called once per frame
    void Update() {
        timer = timer + Time.deltaTime;
        if (timer > 30)
        {
            timer = 0;
            onset.RefreshBeatCount();
            amplitude.RefreshAmplitude();
            //Debug.Log("Beats is " + onset.Beats);
            //Debug.Log("amp is " + amplitude.amplitude);
            spawnCount = (int)(onset.Beats * amplitude.amplitude);
            spawner.enemyCount = Mathf.Min( spawnCount,20);
            //Debug.Log("SpawnCount is " + spawnCount);
        }
    }

   


}
