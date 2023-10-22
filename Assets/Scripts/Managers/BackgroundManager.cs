using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : Singleton<BackgroundManager>
{
    void Update()
    {
        var player = PlayerManager.Instance.transform.position + new Vector3(0, 150, 0);
        transform.position = player;
    }
}
