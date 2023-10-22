using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileScreenManager : Singleton<TileScreenManager>
{
    [SerializeField]
    private Button _startButton;

    [SerializeField]
    private Button _quitButton;

    // Start is called before the first frame update
    void Start()
    {
        _startButton.onClick.AddListener(() => LoadingSceneManager.Instance.LoadScene(SceneName.GamePlay));
        _quitButton.onClick.AddListener(() => Application.Quit());
    }
}
