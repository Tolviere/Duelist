using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadDotDotDot : MonoBehaviour
{
    TextMeshProUGUI loadingText;
    public float timeBetweenDots = 1f;
    int dots = 0;
    // Start is called before the first frame update
    void Start()
    {
        loadingText = GetComponent<TextMeshProUGUI>();
        loadingText.text = "Loading";

        InvokeRepeating("DotTimer", timeBetweenDots, timeBetweenDots);
    }

    void DotTimer(){
        loadingText.text += ". ";
        dots++;
        if(dots > 3){
            loadingText.text = "Loading";
            dots = 0;
        }
    }
}
