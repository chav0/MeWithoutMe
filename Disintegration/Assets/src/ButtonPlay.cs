using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPlay : MonoBehaviour {

    public Button btn;
    public ButtonBack back;
    public Animator anim;
    public CanvasGroup canvasGroup;

    void Awake()
    {
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        anim.SetBool("On", !back.On);
        canvasGroup.blocksRaycasts = !back.On;
        back.On = !back.On;
    }
}
