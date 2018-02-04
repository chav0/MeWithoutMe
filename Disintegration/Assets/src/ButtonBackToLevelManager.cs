using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBackToLevelManager : MonoBehaviour {

    public Button btn;
    public GameObject level; 

    void Awake()
    {
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Destroy(level); 
    }
}
