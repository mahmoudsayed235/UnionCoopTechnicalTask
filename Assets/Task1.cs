using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task1 : MonoBehaviour
{
    public int[] arr;
    public void  NumberOfSubArrays(Text numberOfSubArraysText)
    {
        if (arr.Length == 0)
        {
            numberOfSubArraysText.text = "please fill Array data";
            return;
        }
        int count = 0, j;
        bool flag = false;
        for (int i = 0; i < arr.Length; i++)
        {
            flag = false;
            for (j = i; j < arr.Length - 1; j++)
            {
                if (arr[j + 1] != (arr[j] + 1))
                {
                    if (flag)
                    {
                        flag = false;
                        count++;
                    }
                    break;
                }
                else
                {
                    if (j + 1 == arr.Length - 1)
                    {
                        count++;
                    }
                    flag = true;
                }
            }
            i = j;
        }
        numberOfSubArraysText.text ="Number of Sub Arrays : "+ count;
    }
}
