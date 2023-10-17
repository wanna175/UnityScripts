using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultStorage : MonoBehaviour
{
    #region 싱글톤 관련
    private static ResultStorage _instance = null;

    public static ResultStorage Instance
    {
        get
        {
            if(_instance == null)
            {
                var obj = new GameObject("ResultStorage").AddComponent<ResultStorage>();
                _instance = obj;
            }
            return _instance;
        }
    }
    public static ResultStorage Create()
    {
        if (_instance != null)
            return null;
        return ResultStorage.Instance;
    }
    private void InitSingleton()
    {
        Debug.Assert(_instance == null);
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    #region 변수
    public float Score { get; set; }
    public List<float> gamescore_list;//최고기록저장...
    public int _current_game_number;
    private int _game_total_number = 1;

    public int Coin { get; set; }
    #endregion

    #region 함수
    private void Awake()
    {
        InitSingleton();
        Coin = 0;
        _current_game_number = 0;
        gamescore_list = new List<float>(_game_total_number){ 0};//나중에는 파일로 받아오든지...
    }
    //현재 게임점수 처리...최고기록이면 저장등..
    public void Process_game_score()
    {
        if (gamescore_list[_current_game_number] < Score)
            gamescore_list[_current_game_number] = Score;
    }
    public void SelectGame(int idx)
    {
        _current_game_number = idx;
    }
    #endregion
}
