using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SceneManager_parent : MonoBehaviour
{
    #region ������Ƽ
    public abstract string SceneName { get; }

    #endregion

    #region ������
    public static bool isMoveMap_num;
    #endregion
    #region �Լ�
    public virtual void Awake() {
        isMoveMap_num = false;

    }
    public virtual void Start() {
    }
    public virtual void Update() { }

    #endregion
}
