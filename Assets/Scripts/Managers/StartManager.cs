using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitUntil(() => LoadingSceneManager.Instance != null);
        LoadingSceneManager.Instance.LoadScene(SceneName.TitleScreen);
    }

    void Update()
    {
        
    }
}
