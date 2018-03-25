using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutor : MonoBehaviour {

    public Button btn;

    [SerializeField]
    private GameObject[] pages; 

    private int i = 0; 


    void Awake()
    {
       btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        pages[i].SetActive(false);
        i++;
        if (i >= pages.Length)
        {
            gameObject.SetActive(false); 
        }
        else
        {
            pages[i].SetActive(true);
        }
    }
}
