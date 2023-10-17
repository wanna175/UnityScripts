using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class SceneManager_parent : MonoBehaviour
{
    #region 프로퍼티
    public abstract string SceneName { get; }
    #endregion // 프로퍼티

    #region 함수
    /*초기화*/
    public virtual void Awake() {
        PlayerManager.Create();
        ResultStorage.Create();
    }
    public virtual void Start() {
    }
    /*상태를 리셋한다.*/
    public virtual void Reset() { }
    /*제거되었을 경우*/
    public virtual void OnDestroy() { }
    /*상태를 갱신한다.*/
    public virtual void Update() {
        if (Time.timeScale == 0)//일시정지를 TimeScale 로 했으므로 
            return;
    }
    #endregion
}
