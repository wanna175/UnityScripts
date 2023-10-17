using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floor_move : MonoBehaviour
{
    #region 변수
    [SerializeField] private GameObject _select_DungeonUI = null;
    [SerializeField] private GameObject _clear_DungeonUI = null;
    #endregion
    #region 함수
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (this.gameObject.name.Equals("clearArea"))
            {
                _clear_DungeonUI.SetActive(true);
                return;
            }
            int.TryParse(this.gameObject.name, out int result);
            if (result == 0)
            {
                Select_Dungeon();
                return;
            }
            PlayerManager.Instance.map_num = result;
            PlayerManager.Instance.current_floor += result;
            SceneManager_parent.isMoveMap_num = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (this.gameObject.name.Equals("clearArea"))
            {
                _clear_DungeonUI.gameObject.SetActive(false);
                return;
            }
            int.TryParse(this.gameObject.name, out int result);
            if (result==0)
                _select_DungeonUI.gameObject.SetActive(false);
        }
    }
    private void Select_Dungeon()
    {
        _select_DungeonUI.gameObject.SetActive(true);
    }
    #endregion
}
