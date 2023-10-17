using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class SceneManager_parent : MonoBehaviour
{
    #region ������Ƽ
    public abstract string SceneName { get; }
    #endregion // ������Ƽ

    #region �Լ�
    /*�ʱ�ȭ*/
    public virtual void Awake() {
        PlayerManager.Create();
        ResultStorage.Create();
    }
    public virtual void Start() {
    }
    /*���¸� �����Ѵ�.*/
    public virtual void Reset() { }
    /*���ŵǾ��� ���*/
    public virtual void OnDestroy() { }
    /*���¸� �����Ѵ�.*/
    public virtual void Update() {
        if (Time.timeScale == 0)//�Ͻ������� TimeScale �� �����Ƿ� 
            return;
    }
    #endregion
}
