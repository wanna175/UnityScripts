using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableClick : MonoBehaviour
{

    #region ÇÔ¼ö
    private void OnDisable()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
    }
    #endregion
}
