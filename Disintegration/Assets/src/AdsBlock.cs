using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.Advertisements;

public class AdsBlock : MonoBehaviour {
    private Image img; 

    private void Awake()
    {
        img = GetComponent<Image>(); 
    }

    // Update is called once per frame
    void Update () {
		if (Advertisement.isShowing)
        {
            img.raycastTarget = false; 
        }
        else
        {
            img.raycastTarget = true; 
        }
	}
}
