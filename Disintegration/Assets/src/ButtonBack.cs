using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBack : MonoBehaviour {

    public Button btn;
    public Animator anim;
    public CanvasGroup canvasGroup;

    public bool On;

    void Awake()
    {
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        anim.SetBool("On", !On);
        canvasGroup.blocksRaycasts = !On;
        On = !On; 
    }
}
