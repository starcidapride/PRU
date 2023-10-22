
using System;
using System.Collections;
using UnityEngine;

public class SpawnerManager : Singleton<SpawnerManager>
{
    [SerializeField]
    private Transform chomper;

    [SerializeField]
    private Transform gunner;

    [SerializeField]
    private float spawnDistance;

    [SerializeField] 
    private int numMonster;

    [SerializeField]
    private float delay;

    private bool isDelay;
    private IEnumerator UpdateCoroutine()
    {
        if (!isDelay)
        {
            isDelay = true;
            var camera = PlayerCameraManager.Instance;
            var spawnPosition = camera.transform.position + new Vector3(spawnDistance, spawnDistance);

            for (int i = 0; i < numMonster; i++)
            {
                SpawnRandomEnemy(spawnPosition + new Vector3(UnityEngine.Random.Range(-500,500), 0));
            }

            yield return new WaitForSeconds(delay);
            isDelay = false;
        }

}
    private void Update()
    {
        StartCoroutine(UpdateCoroutine());
    }

    private void SpawnRandomEnemy(Vector3 spawnPosition)
    {
        // Randomly select between chomper and gunner prefabs
        Transform enemyPrefab = UnityEngine.Random.Range(0, 2) == 0 ? chomper : gunner;

        // Instantiate the randomly selected enemy prefab at the specified position
        Transform newEnemy = Instantiate(enemyPrefab);
        newEnemy.position = new Vector3(spawnPosition.x, spawnPosition.y, 0);
    }
}