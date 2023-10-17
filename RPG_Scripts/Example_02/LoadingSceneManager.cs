using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : SceneManager_parent
{
    #region 프로퍼티
    public override string SceneName => Global.LOADING_SCENE;
    #endregion

    #region 변수
    private static string _next_scene_name;
    [SerializeField] private GameObject circle_bar = null;
    #endregion

    #region 함수
    public override void Start()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(LoadSceneAsync(_next_scene_name, LoadSceneMode.Single));
    }
    public override void Update()
    {
        base.Update();
        circle_bar.transform.Rotate(0, 0, 300 * Time.deltaTime);
    }
    public static void LoadScene(string sceneName)
    {
        _next_scene_name = sceneName;
        var data = PlayerManager.Instance.GetData();
        int idx = PlayerManager.Instance.GetIDX();
        DataManager.Instance._list[idx] = data;
        DataManager.Instance.SaveData();
        SceneManager.LoadScene(Global.LOADING_SCENE);
    }
    private IEnumerator LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode)
    {
        yield return new WaitForSeconds(1.0f);
        var op = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
        do
        {
         
            yield return new WaitForEndOfFrame();
        } while (!op.isDone);
        yield break;
    }

    #endregion

}
