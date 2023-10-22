using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using static EnumUtils;

public enum SceneName : byte
{
    [Description("Bootstrap")]
    Bootstrap,

    [Description("GamePlay")]
    GamePlay,

    [Description("TitleScreen")]
    TitleScreen,

    [Description("Win")]
    Win,

    [Description("GameOver")]
    GameOver
};


public class LoadingSceneManager : SingletonPersistent<LoadingSceneManager>
{

    private SceneName sceneActive;
    public SceneName SceneActive => sceneActive;

    public static bool isInputBlocked = true;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneChange;
    }

    private void OnSceneChange(Scene scene, LoadSceneMode mode)
    {
    }

    public void LoadScene(SceneName sceneToLoad)
    {
        StartCoroutine(LoadSceneCoroutine(sceneToLoad));
    }

    private IEnumerator LoadSceneCoroutine(SceneName sceneToLoad)
    {
        LoadingFadeEffectManager.Instance.FadeIn();

        yield return new WaitUntil(() => LoadingFadeEffectManager.beginLoad);

            LoadSceneLocal(sceneToLoad);
 
        yield return new WaitForSeconds(1f);

        LoadingFadeEffectManager.Instance.FadeOut();

        yield return new WaitUntil(() => LoadingFadeEffectManager.endLoad);
    }

    private void LoadSceneLocal(SceneName sceneToLoad)
    {
        SceneManager.LoadScene(GetDescription(sceneToLoad));
    }
}
