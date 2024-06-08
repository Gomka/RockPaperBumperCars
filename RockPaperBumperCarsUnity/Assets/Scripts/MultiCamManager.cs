using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCamManager : MonoBehaviour
{
    [SerializeField] Camera [] cameras;

    void Awake()
    {

        for (int i = 1; i <= cameras.Length; i++)
        {

            if (i < Display.displays.Length)
            {
                Display.displays[i].Activate();
                cameras[i].targetDisplay = i;
            }
        }
    }
}
