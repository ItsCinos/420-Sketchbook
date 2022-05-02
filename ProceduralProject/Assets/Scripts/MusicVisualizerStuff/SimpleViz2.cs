using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleViz2 : MonoBehaviour
{
    static public SimpleViz2 viz { get; private set; }

    public float radius = 500;
    public float amp = 4;

    public float orbHeight = 10;
    public int numBands = 512;
    public Transform prefabOrb;

    private AudioSource player;
    private LineRenderer line;

    private List<Transform> orbs = new List<Transform>();


    void Start()
    {
        player = GetComponent<AudioSource>();
        line = GetComponent<LineRenderer>();

        // spawn 1 orb for each frequency band:
        Quaternion q = Quaternion.identity;
        for (int i = 0; i < numBands; i++)
        {
            Vector3 p = new Vector3(50, i * orbHeight / numBands, 50);
            orbs.Add(Instantiate(prefabOrb, p, q, transform));
        }
    }

    void OnDestroy()
    {
        if (viz == this) viz = null;
    }

    void Update()
    {
        UpdateWaveform();
        UpdateFreqBands();
    }
    private void UpdateFreqBands()
    {
        float[] bands = new float[numBands];
        player.GetSpectrumData(bands, 0, FFTWindow.BlackmanHarris);

        for (int i = 0; i < orbs.Count; i++)
        {
            float p = (i + 1) / (float)numBands;
            orbs[i].localScale = Vector3.one * bands[i] * 500 * p;
            orbs[i].position = new Vector3(45, i * orbHeight / numBands, 40);
        }
        
    }
    private void UpdateWaveform()
    {
        int samples = 1024;
        float[] data = new float[samples];
        player.GetOutputData(data, 0);

        Vector3[] points = new Vector3[samples];

        for (int i = 0; i < data.Length; i++)
        {
            float sample = data[i];

            float rads = Mathf.PI * 2 * i / samples;

            float x = Mathf.Cos(rads) * radius;
            float z = Mathf.Sin(rads) * radius;

            float y = (sample * amp) + 75;

            points[i] = new Vector3(x, y, z);
        }

        line.positionCount = points.Length;
        line.SetPositions(points);
    }
}