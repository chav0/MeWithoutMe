using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Block : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    public int size;
    [SerializeField]
    public List<bool> input;
    [SerializeField]
    public List<bool> output;
    [SerializeField]
    public BlockType type;
    [SerializeField]
    public AudioSource sound;

    public GameObject _button;
    public int colomn;
    public int row;

    private Table _grid;
    Vector2 startPosition;
    private GameObject oldBlockGO;
    public static Transform startParent;

    public void TableUpdate()
    {
        _grid = transform.parent.GetComponent<Table>();
        for (int i = 0; i < size; i++)
        {
            _grid._table[row + i, colomn]._block = this;
        }
        _grid.Refresh();
    }

    public void DeleteOldBlockPositions()
    {
        for (int i = 0; i < size; i++)
        {
            _grid._table[row + i, colomn]._block = null;
            _grid._table[row + i, colomn + 1]._line = null;
            if (_grid._table[row + i, colomn + 1]._lineGO != null)
            {
                Destroy(_grid._table[row + i, colomn + 1]._lineGO);
            }
            _grid._table[row + i, colomn + 1]._lineGO = null;
        }
    }

    public bool isOutput(int rowCell)
    {
        return output[rowCell - row];
    }

    #region IBeginDragHandler implementation

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = GetComponent<RectTransform>().anchoredPosition;
        oldBlockGO = Instantiate(gameObject, transform.parent);
        oldBlockGO.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
        for (int i = 0; i < oldBlockGO.transform.childCount; i++)
        {
            oldBlockGO.transform.GetChild(i).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
        }
    }

    #endregion

    #region IDragHandler implementation

    public void OnDrag(PointerEventData eventData)
    {

        transform.position = Input.mousePosition + new Vector3(-50f, size * 100f, 0f);
    }

    #endregion

    #region IEndDragHandler implementation

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 blockPos = GetComponent<RectTransform>().anchoredPosition;
        if (blockPos.x > 451f && blockPos.x < 2349f && blockPos.y > (-900f + 100f * size) && blockPos.y < 0f)
        {
            //sound.Play();
            DeleteOldBlockPositions();
            int cellX = (int)Mathf.Round(blockPos.x / 100f);
            int cellY = (int)Mathf.Round(blockPos.y / 100f);
            colomn = cellX;
            row = -cellY;
            GetComponent<RectTransform>().anchoredPosition = new Vector2(100f * cellX, 100f * cellY);
            TableUpdate();
        }
        else
        {
            GetComponent<RectTransform>().anchoredPosition = startPosition;
        }
        Destroy(oldBlockGO);
    }

    #endregion
}



public enum BlockType
{
    integration = 0,
    desintegration = 1
}
