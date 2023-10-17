using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class market_slot : MonoBehaviour, IPointerClickHandler
{
    #region 변수
    public int Slot_idx { get; set; }

    private Image _item_image = null;
    private Text _item_name = null;
    private Text _item_price = null;

    [SerializeField] private CountableItemData _item_data = null;
    public CountableItemData Data => _item_data;
    [SerializeField] private Transform _infoPanel = null;

    //_infoPanel 변수
    private Text _item_name_txt = null;
    private Text _item_grade_txt = null;
    private Text _item_info_txt = null;
    #endregion

    #region 함수
    private void Awake()
    {
        _item_image = this.transform.GetChild(0).GetComponent<Image>();
        _item_name = this.transform.GetChild(1).GetComponent<Text>();
        _item_price = this.transform.GetChild(2).GetComponent<Text>();

        _item_image.sprite = _item_data.IconSprite;
        _item_name.text = _item_data.Name;
        _item_price.text = (_item_data.Price*10).ToString();
    }
    private void Start()
    {
        _item_name_txt = _infoPanel.GetChild(0).GetComponent<Text>();
        _item_grade_txt = _infoPanel.GetChild(1).GetComponent<Text>();
        _item_info_txt = _infoPanel.GetChild(2).GetComponent<Text>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Item_info();
    }
    public void Item_info()
    {
        MarketUI._select_idx = Slot_idx;
        _item_name_txt.text = _item_data.Name;
        _item_grade_txt.text = _item_data.Grade;
        _item_info_txt.text = _item_data.Tooltip;
        _infoPanel.gameObject.SetActive(true);
    }
    #endregion
}
