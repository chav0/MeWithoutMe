using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutor : MonoBehaviour {

    public Button btn;

    void Awake()
    {
       btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        gameObject.SetActive(false);
    }
}
