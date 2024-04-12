
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour {

    [HideInInspector] public Vector3 lastNeastEnemyPosition = new Vector3(float.MaxValue, 0, 0);

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemyMaxConcurrentSpawnNumber;
    [SerializeField] private List<AimDirection> enemySpawnDirectionArray;

    private int currentEnemyCount;
    private int enemiesSpawnedSoFar;

    private float spawnIntervalTimeMax = 0.2f;
    private float spawnIntervalTimer;

    private GameObject nearestEnemy;




    private void Update() {
        spawnIntervalTimer -= Time.deltaTime;
        if (spawnIntervalTimer > 0) return;
        SpawnEnemies();
    }


    private void SpawnEnemies() {

        if (currentEnemyCount >= enemyMaxConcurrentSpawnNumber) return;

        ResetSpawnTimer();

        Vector2 playerPosition = GetRandomSpawnPosition();

        CreateEnemy(playerPosition);

    }


    private void ResetSpawnTimer() {
        spawnIntervalTimer = spawnIntervalTimeMax;
    }


    private void CreateEnemy(Vector2 position) {

        enemiesSpawnedSoFar++;
        currentEnemyCount++;
        GameObject enemyGameObject = Instantiate(enemyPrefab, position, Quaternion.identity, transform);

        Enemy enemy = enemyGameObject.GetComponent<Enemy>();
        enemy.EnemyInitialization(enemiesSpawnedSoFar);
        enemy.destroyedEvent.OnDestoryed += Enemy_OnDestoryed;
        enemy.movementToPositionEvent.OnMovementToPosition += Enemy_MovementToPositionEvent;

    }



    private void Enemy_OnDestoryed(DestroyedEvent destroyedEvent, DestroyedEventArgs destoryedEventArgs) {


        destroyedEvent.GetComponent<MovementToPositionEvent>().OnMovementToPosition -= Enemy_MovementToPositionEvent;
        destroyedEvent.OnDestoryed -= Enemy_OnDestoryed;


        currentEnemyCount--;

    }

    private void Enemy_MovementToPositionEvent(MovementToPositionEvent movementToPositionEvent, MovementToPositionArgs args) {



        Vector3 newEnemyPosition = movementToPositionEvent.transform.position;



        if (nearestEnemy != null && nearestEnemy.activeSelf) {

            Vector3 playerPosition = GameManager.Instance.GetPlayer().transform.position;
            float lastNearestDistance = (lastNeastEnemyPosition - playerPosition).magnitude;
            float newEnemyDistance = (newEnemyPosition - playerPosition).magnitude;

            if (lastNearestDistance > newEnemyDistance) {
                nearestEnemy = movementToPositionEvent.gameObject;
            }

        } else {
            nearestEnemy = movementToPositionEvent.gameObject;
        }

        lastNeastEnemyPosition = nearestEnemy.transform.position;

    }

    private Vector2 GetRandomSpawnPosition() {

        Vector3 lowerPoint = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 upperPoint = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));



        Vector3 spawnPosition = upperPoint;
        Vector3 randomOffsset = Vector3.zero;
        int index = Random.Range(0, enemySpawnDirectionArray.Count);
        AimDirection direction = enemySpawnDirectionArray[index];
        switch (direction) {
            case AimDirection.Left:
                spawnPosition.x = lowerPoint.x;
                spawnPosition.y = 0;
                randomOffsset = new Vector2(GetRandom01(1), GetRandom01(3));
                break;
            case AimDirection.UpLeft:
                spawnPosition.x = lowerPoint.x;
                spawnPosition.y = upperPoint.y;
                randomOffsset = new Vector2(GetRandom01(3), GetRandom01(3));
                break;
            case AimDirection.DownLeft:
                spawnPosition.x = lowerPoint.x;
                spawnPosition.y = lowerPoint.y;
                randomOffsset = new Vector2(GetRandom01(3), GetRandom01(3));
                break;
            case AimDirection.Up:
                spawnPosition.x = 0;
                spawnPosition.y = upperPoint.y;
                randomOffsset = new Vector2(GetRandom01(3), GetRandom01(1));
                break;
            case AimDirection.Down:
                spawnPosition.x = 0;
                spawnPosition.y = lowerPoint.y;
                randomOffsset = new Vector2(GetRandom01(3), GetRandom01(1));
                break;
            case AimDirection.Right:
                spawnPosition.x = upperPoint.x;
                spawnPosition.y = 0;
                randomOffsset = new Vector2(GetRandom01(1), GetRandom01(3));
                break;
            case AimDirection.UpRight:
                spawnPosition.x = upperPoint.x;
                spawnPosition.y = upperPoint.y;
                randomOffsset = new Vector2(GetRandom01(3), GetRandom01(3));
                break;
            case AimDirection.DownRight:
                spawnPosition.x = upperPoint.x;
                spawnPosition.y = lowerPoint.y;
                randomOffsset = new Vector2(GetRandom01(3), GetRandom01(3));
                break;
        }


        Vector3 position = spawnPosition + randomOffsset;
        return position;

    }

    private float GetRandom01(float range) {
        int flag = Random.Range(0, 2) * 2 - 1;
        return Random.Range(0f, range) * flag;
    }

}
