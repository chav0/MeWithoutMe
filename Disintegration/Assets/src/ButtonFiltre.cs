using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonFiltre : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public AudioSource sound;
    [SerializeField]
    public int _count;
    [SerializeField]
    public GameObject _filtrePrefab;
    [SerializeField]
    public Text _lblCount;

    public GameObject _blockGO;
    private Block _block;
    Vector3 startPosition;
    Transform startParent;
    private Transform _tableGO;
    private Table _table;
    private RectTransform _blockRT;

    private int oldCellX = 0;
    private int oldCellY = 0;
    bool haveNoNaibours; 

    private void Awake()
    {
        _lblCount.text = _count.ToString();
        _tableGO = transform.parent.parent.GetChild(0);
    }

    public void increment()
    {
        _count++;
        _lblCount.text = _count.ToString();
        GetComponent<CanvasGroup>().blocksRaycasts = (_count > 0);
    }

    public void decrement()
    {
        _count--;
        _lblCount.text = _count.ToString();
        GetComponent<CanvasGroup>().blocksRaycasts = (_count > 0);
    }

    #region IBeginDragHandler implementation

    public void OnBeginDrag(PointerEventData eventData)
    {
        _blockGO = Instantiate(_filtrePrefab, _tableGO);
        _blockGO.transform.position = eventData.position;
        _block = _blockGO.GetComponent<Block>();
        startPosition = _blockGO.transform.position;
        startParent = _blockGO.transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        _blockRT = _blockGO.GetComponent<RectTransform>();
    }

    #endregion

    #region IDragHandler implementation

    public void OnDrag(PointerEventData eventData)
    {
        _block.transform.position = eventData.position; 
        _blockRT.anchoredPosition += new Vector2(-50f, _block.size * 100f);
        Vector2 blockPos = _blockRT.anchoredPosition;
        if (blockPos.x > 451f && blockPos.x < 2349f && blockPos.y > (-900f + 100f * _block.size) && blockPos.y < 0f)
        {
            int cellX = (int)Mathf.Round(blockPos.x / 100f);
            int cellY = -(int)Mathf.Round(blockPos.y / 100f);

            haveNoNaibours = true;

            for (int i = 0; i < _block.size; i++)
            {
                haveNoNaibours = haveNoNaibours && Table._table[cellY + i, cellX - 1]._block == null;
                haveNoNaibours = haveNoNaibours && Table._table[cellY + i, cellX + 1]._block == null;
            }


            if ((cellX != oldCellX || cellY != oldCellY) && haveNoNaibours)
            {
                for (int i = 0; i < _block.size; i++)
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
            for (int i = 0; i < _block.size; i++)
            {
                Table._table[oldCellY + i, oldCellX].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    #endregion

    #region IEndDragHandler implementation

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 blockPos = _blockGO.GetComponent<RectTransform>().anchoredPosition;

        if (blockPos.x > 451f && blockPos.x < 2349f && blockPos.y > (-900f + 100f * _block.size) && blockPos.y < 0f && haveNoNaibours)
        {
            sound.Play();
            int cellX = (int)Mathf.Round(blockPos.x / 100f);
            int cellY = (int)Mathf.Round(blockPos.y / 100f);
            _block.colomn = cellX;
            _block.row = -cellY;
            _blockGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(100f * cellX, 100f * cellY);
            _block._button = gameObject;
            decrement();
            _block.TableUpdate();

            for (int i = 0; i < _block.size; i++)
            {
                Table._table[-cellY + i, cellX].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < _block.size; i++)
            {
                Table._table[oldCellY + i, oldCellX].transform.GetChild(0).gameObject.SetActive(false);
            }
            Destroy(_blockGO);
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

    }
    #endregion
}

