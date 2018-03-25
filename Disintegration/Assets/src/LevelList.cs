using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelList : MonoBehaviour {

    public List<GameObject> levels; 

    public void NextLevel(int lvlNum)
    {
        Instantiate(levels[lvlNum - 1], transform.parent); 
    }

    public void DeleteLevel(GameObject level)
    {
        Destroy(level); 
    }
}
