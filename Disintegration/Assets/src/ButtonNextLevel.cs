using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class ButtonNextLevel : MonoBehaviour {

    private Button btn;

    // Use this for initialization
    void Awake () {
        btn = GetComponent<Button>(); 

        if (Advertisement.isSupported)
            Advertisement.Initialize("1715105", false);
        btn.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        if (Advertisement.IsReady() && (LevelButton.ads + 1) % 3 == 0)
        {
            Advertisement.Show();

        }
        LevelButton.ads++;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
