using DigitalRuby.SimpleLUT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brid : MonoBehaviour
{
    public static float Brightness = 0f;

    private SimpleLUT _proccesing;

    private void Awake()
    {
        _proccesing = GetComponent<SimpleLUT>();

        _proccesing.Brightness = Brightness;
    }

    public void SetBrightness(float value)
    {
        Brightness = value;

        _proccesing.Brightness = Brightness;
    }
}
