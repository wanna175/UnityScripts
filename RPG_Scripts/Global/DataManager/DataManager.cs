using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class PlayerFileData
{
    #region 변수
    [JsonProperty("ID")] public int _idx = 0;
    [JsonProperty("PREFAB")] public string _prefab = null;//플레이어 프리팹 경로
    [JsonProperty("INVENITEM")] public string _inven_item = null;//소비 아이템 배열=>id를 순서에 따라 저장(0000)
    [JsonProperty("EQUIPITEM")] public string _equip_item = null;//장비 아이템 배열
    [JsonProperty("MONEY")] public int _money = 0;
    [JsonProperty("LV")] public int _lv = 0;
    [JsonProperty("ATK")] public int _atk = 0;
    [JsonProperty("DEF")] public int _def = 0;
    [JsonProperty("EXP")] public float _exp = 0;
    #endregion
}
public class DataManager : MonoBehaviour
{
    #region 싱글톤 관련
    private static DataManager _instance = null;
    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject("DataManager").AddComponent<DataManager>();
                _instance = obj;
            }
            return _instance;
        }
    }
    public static DataManager Create()
    {
        if (_instance != null)
            return null;
        return DataManager.Instance;
    }
    private void InitSingleton()
    {
        Debug.Assert(_instance == null);
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    #region 변수
    private Dictionary<int, PlayerFileData> _playerInfoDict = new Dictionary<int, PlayerFileData>();
    public List<Player_data> _list = null;

    private Dictionary<int, ItemData> _itemDataDict = new Dictionary<int, ItemData>();//전체 아이템 데이터
    public Dictionary<int, ItemData> DictItem => _itemDataDict;
    #endregion

    #region 함수
    private void Awake()
    {
        InitSingleton();
        var item_data = Resources.LoadAll<ItemData>("Item_Data");
        for(int i = 0; i < item_data.Length; i++)
        {
            _itemDataDict.TryAdd(item_data[i].ID, item_data[i]);
        }
        _list = new List<Player_data>();
    }
    //데이터를 로드한다.
    public void LoadData()
    {
        this.LoadPlayerData("file/GameData");
    }
    public void LoadPlayerData(string filePath)
    {
        var gameData = Resources.Load<TextAsset>(filePath);
        var DataInfos = JObject.Parse(gameData.text);
        var PlayerInfos = DataInfos["player"] as JArray;

        for (int i = 0; i < PlayerInfos.Count; i++)
        {
            var Str = PlayerInfos[i].ToString();
            var PlayerInfo = JsonConvert.DeserializeObject<PlayerFileData>(Str);

            this._playerInfoDict.TryAdd(PlayerInfo._idx, PlayerInfo);
            
        }
        for(int i = 0; i < _playerInfoDict.Count; i++)//플레이어 데이터를 파싱한다.
        {
            _playerInfoDict.TryGetValue(i, out PlayerFileData data);
            if (data._prefab.Equals("null"))
                break;

            GameObject Prefab = Resources.Load<GameObject>(data._prefab);
            Player_Item PlayerItem = ProcessItemStr(data._inven_item, data._equip_item, data._money);
            Status PlayerStatus = new Status(data._lv, data._atk, data._def, data._exp);
            Player_data player_data = new Player_data(Prefab, PlayerItem, PlayerStatus);
            _list.Add(player_data);
        }
    }
    private Player_Item ProcessItemStr(string _inven,string _equip,int _money)
    {
        Player_Item items = new Player_Item();
        items.Init(_inven, _equip, _money);
        return items;
    }
    public void SaveData()
    {
        string[] Str = new string[4];
        for (int i = 0; i < _list.Count; i++)
        {
            PlayerFileData data = new();
            data._idx = i;
            data._prefab = "Prefabs/Player/Player_" + _list[i]._player.name.Substring(7);
            data._inven_item = _list[i]._item.SerializeInventory();
            data._equip_item = _list[i]._item.SerializeEquipment();
            data._money = _list[i]._item.Money;
            data._lv = _list[i]._status.LV;
            data._atk = _list[i]._status.Total_Attack;
            data._def = _list[i]._status.Total_Defend;
            data._exp = _list[i]._status.Current_Exp;
            if (_playerInfoDict.ContainsKey(i))
                _playerInfoDict[i] = data;
            string str = JsonConvert.SerializeObject(data);
            Str[i] = str;
        }
        for(int i = _list.Count; i < 4; i++)
        {
            PlayerFileData data = new();
            data._idx = i;
            data._prefab = "null";
            data._inven_item = "i";
            data._equip_item = "e";
            data._lv = 0;
            data._atk = 0;
            data._def = 0;
            data._exp = 0;
            string str = JsonConvert.SerializeObject(data);
            Str[i] = str;
        }
        JArray array = new JArray();
        for (int i = 0; i < 4; i++)
        {
            array.Add(Str[i]);
        }
        JObject _save_json = new JObject();
        _save_json.Add("player", array);

        File.WriteAllText("Assets/Resources/file/GameData.json", _save_json.ToString());
    }
    #endregion
}
