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
    bool haveNoNaibours;

    private int oldCellX = 0;
    private int oldCellY = 0;

    public void TableUpdate()
    {
        _grid = transform.parent.GetComponent<Table>();
        for (int i = 0; i < size; i++)
        {
            Table._table[row + i, colomn]._block = this;
        }
        _grid.Refresh();
    }

    public void DeleteOldBlockPositions()
    {
        for (int i = 0; i < size; i++)
        {
            Table._table[row + i, colomn]._block = null;
            Table._table[row + i, colomn + 1]._line = null;
            if (Table._table[row + i, colomn + 1]._lineGO != null)
            {
                Destroy(Table._table[row + i, colomn + 1]._lineGO);
            }
            Table._table[row + i, colomn + 1]._lineGO = null;
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
        Vector2 blockPos = GetComponent<RectTransform>().anchoredPosition;
        if (blockPos.x > 451f && blockPos.x < 2349f && blockPos.y > (-900f + 100f * size) && blockPos.y < 0f)
        {
            int cellX = (int)Mathf.Round(blockPos.x / 100f);
            int cellY = -(int)Mathf.Round(blockPos.y / 100f);

            haveNoNaibours = true;

            for (int i = 0; i < size; i++)
            {
                haveNoNaibours = haveNoNaibours && Table._table[cellY + i, cellX - 1]._block == null;
                haveNoNaibours = haveNoNaibours && Table._table[cellY + i, cellX + 1]._block == null;
            }


            if ((cellX != oldCellX || cellY != oldCellY) && haveNoNaibours)
            {
                for (int i = 0; i < size; i++)
                {
                    Table._table[cellY + i, cellX].transform.GetChild(0).gameObject.SetActive(true);
                    Table._table[oldCellY + i, oldCellX].transform.GetChild(0).gameObject.SetActive(false);
                }
                oldCellY = cellY;
                oldCellX = cellX;
            }
        }
        else
        {
            for (int i = 0; i < size; i++)
            {
                Table._table[oldCellY + i, oldCellX].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    #endregion

    #region IEndDragHandler implementation

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 blockPos = GetComponent<RectTransform>().anchoredPosition;
        if (blockPos.x > 451f && blockPos.x < 2349f && blockPos.y > (-900f + 100f * size) && blockPos.y < 0f && haveNoNaibours)
        {
            //sound.Play();
            DeleteOldBlockPositions();
            int cellX = (int)Mathf.Round(blockPos.x / 100f);
            int cellY = (int)Mathf.Round(blockPos.y / 100f);
            colomn = cellX;
            row = -cellY;
            GetComponent<RectTransform>().anchoredPosition = new Vector2(100f * cellX, 100f * cellY);
            TableUpdate();

            for (int i = 0; i < size; i++)
            {
                Table._table[-cellY + i, cellX].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        else
        {
            if (blockPos.y < (-900f + 100f * size))
            {
                DeleteOldBlockPositions();
                _grid = transform.parent.GetComponent<Table>();
                _grid.Refresh();
                _button.GetComponent<ButtonFiltre>().increment(); 
                Destroy(gameObject); 
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    Table._table[oldCellY + i, oldCellX].transform.GetChild(0).gameObject.SetActive(false);
                }
                GetComponent<RectTransform>().anchoredPosition = startPosition;
            }
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
