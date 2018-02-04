using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour {

    public Button btn;
    public Animator anim;
    public CanvasGroup canvasGroup;

    public bool On; 

    void Start()
    {
        btn.onClick.AddListener(TaskOnClick);
        On = true;
    }

    void TaskOnClick()
    {
        anim.enabled = true;
        canvasGroup.blocksRaycasts = false; 
    }
}
