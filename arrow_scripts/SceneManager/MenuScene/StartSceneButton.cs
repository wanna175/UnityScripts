using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartSceneButton : MonoBehaviour, IPointerClickHandler
{

    #region �Լ�
    public void OnPointerClick(PointerEventData eventData)
    {
        string name = eventData.pointerCurrentRaycast.gameObject.name;
        switch (name)
        {
            case Global.MENU_START: LoadingSceneManager.LoadScene(Global.G_SCENE_NAME_00); break;
            case Global.MENU_RESULT: break;
            case Global.MENU_SETTING: break;
            case Global.MENU_MARTKET: break;
            case Global.MENU_EXIT: Debug.Log("������ �����մϴ�."); break;
            case Global.MENU_SOCIAL: break;
        }
    }
    #endregion
}
