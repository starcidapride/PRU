using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraManager : Singleton<PlayerCameraManager>
{
    void Update()
    {
        var player = PlayerManager.Instance.transform.position + new Vector3(0, 150, 0);
        transform.position = new Vector3(player.x, player.y, -90);
    }
}
