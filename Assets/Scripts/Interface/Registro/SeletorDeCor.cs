using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeletorDeCor : MonoBehaviour
{
    [SerializeField] Slider sliderR, sliderG, sliderB;
    [SerializeField] Image image;
    float r, g, b;
    Color cor = Color.white;
    public void TrocarCor()
    {
        r = sliderR.value;
        g = sliderG.value;
        b = sliderB.value;
        cor = new Color(r, g, b, 1f);
        image.color = cor;
    }

    public Color GetCor() => cor;
}

