using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableClick : MonoBehaviour
{

    #region �Լ�
    private void OnDisable()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
    }
    #endregion
}
