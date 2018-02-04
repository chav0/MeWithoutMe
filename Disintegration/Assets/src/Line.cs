using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Line : MonoBehaviour {
    [SerializeField]
    private GameObject _circlePrefab;
    [SerializeField]
    private GameObject _group; 
     
    public int _startCell;
    public int _endCell;
    public int _row;
    public List<SignColor> _sign;
    private List<GameObject> _circles;
    //private float speed = 50f; 

    private float _cellSize = 100f;
    private float width;

    private RectTransform lineRT;
    private RectTransform groupRT; 

    void Awake()
    {
        _circles = new List<GameObject>();
        lineRT = GetComponent<RectTransform>();
        groupRT = _group.GetComponent<RectTransform>(); 
    }

    public void Refresh()
    {
        foreach(GameObject circl in _circles)
        {
            Destroy(circl); 
        }
        _circles = new List<GameObject>();
        lineRT.anchoredPosition = new Vector2(_cellSize * _startCell, -_cellSize * _row - 50f);
        lineRT.sizeDelta = new Vector2(_cellSize * (_endCell - _startCell + 1), 172f);
        width = lineRT.sizeDelta.x;
        foreach (SignColor s in _sign)
        {
            GameObject circle = Instantiate(_circlePrefab, _group.transform);
            _circles.Add(circle);
            circle.GetComponent<Image>().color = circle.GetComponent<Circle>()._colors[(int)s]; 
        } 
    }

    private void Update()
    {
        if (groupRT.anchoredPosition.x < width + groupRT.sizeDelta.x / 2)
        {
            groupRT.anchoredPosition = groupRT.anchoredPosition + new Vector2(250f, 0f) * Time.deltaTime;
        } else
        {
            groupRT.anchoredPosition = new Vector2(-groupRT.sizeDelta.x / 2, 0f); 
        }

    }
}
