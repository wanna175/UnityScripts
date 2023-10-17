using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Damage_txt : MonoBehaviour
{
    #region 변수
    [SerializeField] private List<SpriteRenderer> _numTxt = null;
    private Color _color;
    private Vector3 _scale;
    #endregion

    #region 함수
    private void Awake()
    {
        _color = _numTxt[0].color;
        _scale = this.transform.localScale;
    }
    private void OnEnable()
    {
        this.transform.DOLocalMoveY(this.transform.localPosition.y + 100f,0.7f);
        StartCoroutine(timer());
    }
    private void OnDisable()
    {
        _numTxt[0].gameObject.SetActive(false);
        _numTxt[1].gameObject.SetActive(false);
        _numTxt[2].gameObject.SetActive(false);
        _numTxt[3].gameObject.SetActive(false);
        _numTxt[0].color = _color;
        _numTxt[1].color = _color;
        _numTxt[2].color = _color;
        _numTxt[3].color = _color;
        this.transform.localScale = _scale;
    }
    public void SetDamage(int value,bool isCritical,bool isPlayer=false)
    {
        
        if (value >= 10000)
        {
            SetDamage(9999,isCritical);
        }
        else
        {
            if (isCritical) this.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            _numTxt[0].sprite = setNum(value % 10,isCritical,isPlayer);
            _numTxt[0].gameObject.SetActive(true);
            _numTxt[0].DOFade(0, 0.7f);
            if (value / 10 == 0) return;
            _numTxt[1].sprite = setNum((value / 10) % 10,isCritical,isPlayer);
            _numTxt[1].gameObject.SetActive(true);
            _numTxt[1].DOFade(0, 0.7f);
            if (value / 100 == 0) return;
            _numTxt[2].sprite = setNum((value / 100) % 10,isCritical,isPlayer);
            _numTxt[2].gameObject.SetActive(true);
            _numTxt[2].DOFade(0, 0.7f);
            if (value / 1000 == 0) return;
            _numTxt[3].sprite = setNum(value / 1000,isCritical,isPlayer);
            _numTxt[3].gameObject.SetActive(true);
            _numTxt[3].DOFade(0, 0.7f);
        }
    }
    private Sprite setNum(int value,bool isCritical,bool isPlayer=false)
    {
        Sprite temp = null;
        if (!isPlayer)
        {
            switch (value)
            {
                case 0: temp = (isCritical) ? DamageManager.Instance._damage_sprites[20] : DamageManager.Instance._damage_sprites[0]; break;
                case 1: temp = (isCritical) ? DamageManager.Instance._damage_sprites[21] : DamageManager.Instance._damage_sprites[1]; break;
                case 2: temp = (isCritical) ? DamageManager.Instance._damage_sprites[22] : DamageManager.Instance._damage_sprites[2]; break;
                case 3: temp = (isCritical) ? DamageManager.Instance._damage_sprites[23] : DamageManager.Instance._damage_sprites[3]; break;
                case 4: temp = (isCritical) ? DamageManager.Instance._damage_sprites[24] : DamageManager.Instance._damage_sprites[4]; break;
                case 5: temp = (isCritical) ? DamageManager.Instance._damage_sprites[25] : DamageManager.Instance._damage_sprites[5]; break;
                case 6: temp = (isCritical) ? DamageManager.Instance._damage_sprites[26] : DamageManager.Instance._damage_sprites[6]; break;
                case 7: temp = (isCritical) ? DamageManager.Instance._damage_sprites[27] : DamageManager.Instance._damage_sprites[7]; break;
                case 8: temp = (isCritical) ? DamageManager.Instance._damage_sprites[28] : DamageManager.Instance._damage_sprites[8]; break;
                case 9: temp = (isCritical) ? DamageManager.Instance._damage_sprites[29] : DamageManager.Instance._damage_sprites[9]; break;
            }
        }
        else
        {
            switch (value)
            {
                case 0: temp = DamageManager.Instance._damage_sprites[30]; break;
                case 1: temp = DamageManager.Instance._damage_sprites[31]; break;
                case 2: temp = DamageManager.Instance._damage_sprites[32]; break;
                case 3: temp = DamageManager.Instance._damage_sprites[33]; break;
                case 4: temp = DamageManager.Instance._damage_sprites[34]; break;
                case 5: temp = DamageManager.Instance._damage_sprites[35]; break;
                case 6: temp = DamageManager.Instance._damage_sprites[36]; break;
                case 7: temp = DamageManager.Instance._damage_sprites[37]; break;
                case 8: temp = DamageManager.Instance._damage_sprites[38]; break;
                case 9: temp = DamageManager.Instance._damage_sprites[39]; break;
            }
        }
        return temp;
    }
    private IEnumerator timer()
    {
        yield return new WaitForSeconds(0.7f);
        DamageManager.Instance.Return_object(this);
    }
    #endregion
}
