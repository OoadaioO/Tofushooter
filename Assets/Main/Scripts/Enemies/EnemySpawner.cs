
using UnityEngine;


public class EnemySpawner : MonoBehaviour {


    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemyMaxConcurrentSpawnNumber;

    private int currentEnemyCount;
    private int enemiesSpawnedSoFar;

    private float spawnIntervalTimeMax = 0.2f;
    private float spawnIntervalTimer;





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



    }
    private void Enemy_OnDestoryed(DestroyedEvent destroyedEvent, DestroyedEventArgs destoryedEventArgs) {

        destroyedEvent.OnDestoryed -= Enemy_OnDestoryed;

        currentEnemyCount--;

    }

    private Vector2 GetRandomSpawnPosition() {

        Vector3 lowerPoint= Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        Vector3 upperPoint = Camera.main.ViewportToWorldPoint(new Vector3(1,1,0));

        Debug.Log("lower point:"+lowerPoint +" upperPoint:"+upperPoint);


        Vector3 offset = upperPoint;
        AimDirection direction = (AimDirection)UnityEngine.Random.Range(0, 8);
        switch (direction) {
            case AimDirection.Left:
                offset.x = lowerPoint.x;
                offset.y = 0;
                break;
            case AimDirection.UpLeft:
                offset.x = lowerPoint.x;
                offset.y = upperPoint.y;
                break;
            case AimDirection.DownLeft:
                offset.x = lowerPoint.x;
                offset.y = lowerPoint.y;
                break;
            case AimDirection.Up:
                offset.x = 0;
                offset.y = upperPoint.y;
                break;
            case AimDirection.Down:
                offset.x = 0;
                offset.y = lowerPoint.y;
                break;
            case AimDirection.Right:
                offset.x = upperPoint.x;
                offset.y = 0;
                break;
            case AimDirection.UpRight:
                offset.x = upperPoint.x;
                offset.y = upperPoint.y;
                break;
            case AimDirection.DownRight:
                offset.x = upperPoint.x;
                offset.y = lowerPoint.y;
                break;
        }



        Vector3 position = GameManager.Instance.GetPlayer().transform.position + offset + new Vector3(Random.Range(0, 3), Random.Range(0, 3), 0);
        return position;

    }

}
