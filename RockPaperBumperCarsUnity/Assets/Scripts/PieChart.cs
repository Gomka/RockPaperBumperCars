using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieChart : MonoBehaviour
{
    public Image[] imagesPieChart;
    public float[] values;

    // Start is called before the first frame update
    void Start()
    {
        SetValues(values);
    }

    public void SetValues(float[] valuesToSet) {
        float totalValues = 0;

        for (int i = 0; i < imagesPieChart.Length; i++)
        {
            totalValues += FindPercentage(valuesToSet, i);
            imagesPieChart[i].fillAmount = totalValues;
        }
    }

    private float FindPercentage (float[] valueToSet, int index) {
        float totalAmount = 0;
        for (int i = 0; i < valueToSet.Length; i++)
        {
            totalAmount += valueToSet[i];
        }

        return valueToSet[index] / totalAmount;
    }
}
