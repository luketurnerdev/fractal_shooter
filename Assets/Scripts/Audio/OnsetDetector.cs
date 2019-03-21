using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnsetDetector : MonoBehaviour {

    //OnsetDetector Based off tutorial by Giant Scam
    //https://medium.com/giant-scam/algorithmic-beat-mapping-in-unity-real-time-audio-analysis-using-the-unity-api-6e9595823ce4

    public MusicAnalyser musicAnalyser;
    private AudioSource audioSource;
    public float updateStep = 0.8f;
    public int sampleDataLength;
    public float amplitude { get; private set; }
    private float currentUpdateTime = 0f;
    private int frequencyWindow = 1024;
    private int thresholdWindowSize = 30;
    private int indexToProcess = 1;
    public int BPM = 0;
    private int lastBeats = 0;
    private float timer = 0;
    public int Beats { get; private set; }
    //1.5 multiplier from https://www.badlogicgames.com/wordpress/?cat=18
    private float thresholdMultiplier = 1.5f;

    private float clipLoudness;
    private float[] newSpectrum;
    private float[] currentSpectrum;
    private float[] lastSpectrum;
    private List<SpectralFluxInfo> spectralFluxSamples = new List<SpectralFluxInfo>();
    // Use this for initialization
    void Start () {
        audioSource = musicAnalyser.audioSource;
        sampleDataLength = musicAnalyser.sampleRate;
        currentSpectrum = new float[frequencyWindow];
        lastSpectrum = new float[frequencyWindow];
        newSpectrum = new float[frequencyWindow];
    }
	
	// Update is called once per frame
	void Update () {
        audioSource.GetSpectrumData(newSpectrum, 0, FFTWindow.BlackmanHarris);
        analyzeSpectrum(newSpectrum, audioSource.timeSamples);
       // Debug.Log(" BMP is " + BPM / Time.time*60);
    }

    public void RefreshBeatCount()
    {
        int beats = 0;
        foreach (SpectralFluxInfo sample in spectralFluxSamples)
        {
            if (sample.isPeak)
            {
                beats++;
            }
            
        }
        Beats = beats - lastBeats;
        lastBeats = beats;
        //spectralFluxSamples.Clear();
    }

    void UpdateSpectrum(float[] newSpectrum)
    {
        currentSpectrum.CopyTo(lastSpectrum, 0);
        newSpectrum.CopyTo(currentSpectrum, 0);
    }
    float calculateRectifiedSpectralFlux()
    {
        float sum = 0f;

        // Aggregate positive changes in spectrum data
        for (int i = 0; i < frequencyWindow; i++)
        {
            sum += Mathf.Max(0f, currentSpectrum[i] - lastSpectrum[i]);
        }
        return sum;
    }
    float getFluxThreshold(int spectralFluxIndex)
    {
        // How many samples in the past and future we include in our average
        int windowStartIndex = Mathf.Max(0, spectralFluxIndex - thresholdWindowSize / 2);
        int windowEndIndex = Mathf.Min(spectralFluxSamples.Count - 1, spectralFluxIndex + thresholdWindowSize / 2);

        // Add up our spectral flux over the window
        float sum = 0f;
        for (int i = windowStartIndex; i < windowEndIndex; i++)
        {
            sum += spectralFluxSamples[i].spectralFlux;
        }

        // Return the average multiplied by our sensitivity multiplier
        float avg = sum / (windowEndIndex - windowStartIndex);
        return avg * thresholdMultiplier;
    }
    float getPrunedSpectralFlux(int spectralFluxIndex)
    {
        return Mathf.Max(0f, spectralFluxSamples[spectralFluxIndex].spectralFlux - spectralFluxSamples[spectralFluxIndex].threshold);
    }
    bool isPeak(int spectralFluxIndex)
    {
        if (spectralFluxSamples[spectralFluxIndex].prunedSpectralFlux > spectralFluxSamples[spectralFluxIndex + 1].prunedSpectralFlux &&
            spectralFluxSamples[spectralFluxIndex].prunedSpectralFlux > spectralFluxSamples[spectralFluxIndex - 1].prunedSpectralFlux)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void analyzeSpectrum(float[] spectrum, float time)
    {
        // Set spectrum
        UpdateSpectrum(spectrum);

        // Get current spectral flux from spectrum
        SpectralFluxInfo curInfo = new SpectralFluxInfo();
        curInfo.time = time;
        curInfo.spectralFlux = calculateRectifiedSpectralFlux();
        spectralFluxSamples.Add(curInfo);

        // We have enough samples to detect a peak
        //Debug.Log("sample size is " + spectralFluxSamples.Count);
        if (spectralFluxSamples.Count >= thresholdWindowSize)
        {
            // Get Flux threshold of time window surrounding index to process
            spectralFluxSamples[indexToProcess].threshold = getFluxThreshold(indexToProcess);

            // Only keep amp amount above threshold to allow peak filtering
            spectralFluxSamples[indexToProcess].prunedSpectralFlux = getPrunedSpectralFlux(indexToProcess);

            // Now that we are processed at n, n-1 has neighbors (n-2, n) to determine peak
            int indexToDetectPeak = indexToProcess - 1;

            bool curPeak = isPeak(indexToDetectPeak);

            if (curPeak)
            {
                spectralFluxSamples[indexToDetectPeak].isPeak = true;
                BPM++;
               // Debug.Log("found a peak at time " +Time.time);

            }
            indexToProcess++;
        }
        else
        {
            Debug.Log(string.Format("Not ready yet.  At spectral flux sample size of {0} growing to {1}", spectralFluxSamples.Count, thresholdWindowSize));
        }
    }
}
public class SpectralFluxInfo
{
    public float time;
    public float spectralFlux;
    public float threshold;
    public float prunedSpectralFlux;
    public bool isPeak;
}