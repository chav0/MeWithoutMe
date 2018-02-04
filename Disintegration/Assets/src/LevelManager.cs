using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public int level;
    public int nextLevel;
    public LevelList levels;

    private void Awake()
    {
        levels = transform.parent.GetChild(1).GetComponent<LevelList>(); 
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("level" + level, 1);
        if (nextLevel <= 15)
        {
            levels.NextLevel(nextLevel);
        }
        levels.DeleteLevel(gameObject); 
    }

    public void ReloadLevel()
    {
        levels.NextLevel(level);
        levels.DeleteLevel(gameObject);
    }
}
