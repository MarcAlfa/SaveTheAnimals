using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAnalyzer : MonoBehaviour
{
    void Update()
    {
        float[] xSpectrum = new float[1024];
        AudioListener.GetSpectrumData(xSpectrum, 0, FFTWindow.Rectangular);
        float l1 = xSpectrum[0] + xSpectrum[2] + xSpectrum[4];
        float l2 = xSpectrum[10] + xSpectrum[11] + xSpectrum[12];
        float l3 = xSpectrum[20] + xSpectrum[21] + xSpectrum[22];
        float l4 = xSpectrum[40] + xSpectrum[41] + xSpectrum[42] + xSpectrum[43];
        float l5 = xSpectrum[80] + xSpectrum[81] + xSpectrum[82] + xSpectrum[83];
        float l6 = xSpectrum[160] + xSpectrum[161] + xSpectrum[162] + xSpectrum[163];
        float l7 = xSpectrum[320] + xSpectrum[321] + xSpectrum[322] + xSpectrum[323];
        Debug.Log("VeryLow="+l1);
        Debug.Log("Low=" + l2);


        /*
        for (int i = 1; i < cubes.Length; i++)
        {
            switch (i)
            {
                case 1:
                    cubes[i].gameObject.transform.localScale = new Vector3(1, l1 * 100, 0.5f); // base drum
                    break;
                case 2:
                    cubes[i].gameObject.transform.localScale = new Vector3(1, l2 * 200, 0.5f); // base guitar
                    break;
                case 3:
                    cubes[i].gameObject.transform.localScale = new Vector3(1, l3 * 400, 0.5f);
                    break;
                case 4:
                    cubes[i].gameObject.transform.localScale = new Vector3(1, l4 * 800, 0.5f);
                    break;
                case 5:
                    cubes[i].gameObject.transform.localScale = new Vector3(1, l5 * 1600, 0.5f);
                    break;
                case 6:
                    cubes[i].gameObject.transform.localScale = new Vector3(1, l6 * 3200, 0.5f);
                    break;
                case 7:
                    cubes[i].gameObject.transform.localScale = new Vector3(1, l7 * 6400, 0.5f); //*tsk tsk tsk
                    break;
            }
        }
        */

    }
}
