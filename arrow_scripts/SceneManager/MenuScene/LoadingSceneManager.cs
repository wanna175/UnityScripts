using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : SceneManager_parent
{
    #region 프로퍼티
    public override string SceneName => Global.G_SCENE_NAME_ROADING;
    #endregion

    #region 변수
    private static string _next_scene_name;
    #endregion

    #region 함수
    public override void Start()
    {
        StartCoroutine(LoadSceneAsync(_next_scene_name,LoadSceneMode.Single));
    }
    public static void LoadScene(string sceneName)
    {
        _next_scene_name = sceneName;

        SceneManager.LoadScene(Global.G_SCENE_NAME_ROADING);
    }
    private IEnumerator LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode)
    {
        WaitForEndOfFrame _wait = new WaitForEndOfFrame();
        yield return new WaitForSecondsRealtime(1.5f);
        var op = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
        do
        {
            yield return _wait;
        } while (!op.isDone);
        yield break;
    }
    #endregion

}
