using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplaySliderCount : MonoBehaviour
{
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = FindObjectOfType<Slider>().GetComponent<Slider>().value.ToString();
    }
}
