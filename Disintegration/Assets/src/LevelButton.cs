using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {

    //public GameObject levelPrefab;
    public LevelList levels; 
    public Transform placeInit;
    public int level;
    public Image circle;
    public Color colorDone; 
    public Button btn;
    public bool isAccept;
    private bool updated; 

	// Use this for initialization
	void Awake () {
        updated = false;
        circle.raycastTarget = false; 
        if (level > 1)
        {
            isAccept = (PlayerPrefs.GetInt("level" + (level - 1)) > 0);
        }
        else
        {
            isAccept = true;
            updated = true; 
            circle.color = colorDone;
            circle.raycastTarget = true;
            btn.onClick.AddListener(TaskOnClick);
        }
    }

    void TaskOnClick()
    {
        levels.NextLevel(level); 
    }

    // Update is called once per frame
    void Update () {
        if (!updated && PlayerPrefs.GetInt("level" + (level - 1)) > 0)
        {
            isAccept = true;
            updated = true; 
            circle.color = colorDone;
            circle.raycastTarget = true;
            btn.onClick.AddListener(TaskOnClick);
        }
	}
}
