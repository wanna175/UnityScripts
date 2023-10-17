using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SceneManager_parent : MonoBehaviour
{
    #region 프로퍼티
    public abstract string SceneName { get; }

    #endregion

    #region 변수ㅗ
    public static bool isMoveMap_num;
    #endregion
    #region 함수
    public virtual void Awake() {
        isMoveMap_num = false;

    }
    public virtual void Start() {
    }
    public virtual void Update() { }

    #endregion
}
