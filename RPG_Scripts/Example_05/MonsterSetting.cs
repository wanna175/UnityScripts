using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSetting : MonoBehaviour
{
    //����Ʈ�� �ڽĿ�����Ʈ�� �ҷ��ͼ� �����ϰ� ��ġ���� ����������...�׸��� �� ������� ���� ������ ���� �ְ� ����..
    #region ����
    private BaseMonster[] _monster_list = null;
    public int _monster_count=-1;
    [SerializeField] public InformationPanel _infoPanel = null;
    #endregion

    #region �Լ�
    private void Awake()
    {
        _monster_list = this.transform.GetComponentsInChildren<BaseMonster>();
        _monster_count = _monster_list.Length;
    }
    private void Start()
    {
        for(int i = 0; i < _monster_list.Length; i++)
        {
            float x = Random.Range(Global.object_left_x+300f, Global.object_right_x-300f);
            float z = Random.Range(Global.object_down_z, Global.object_up_z);
            _monster_list[i].transform.localPosition = new Vector3(x, _monster_list[i].transform.localPosition.y, z);
        }
    }
    #endregion

}
