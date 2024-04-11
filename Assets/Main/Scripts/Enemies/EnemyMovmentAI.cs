using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[DisallowMultipleComponent]
public class EnemyMovmentAI : MonoBehaviour {

    [HideInInspector] public int updateFrameNumber = 1; // 默认值是1，通过enemy spawner 设置.

    [SerializeField] private float moveSpeed;


    private float currentEnemyPathRebuildCooldown;
    private Vector3 playerReferencePosition;
    private Coroutine moveEnemyRoutine;
    private WaitForFixedUpdate waitForFixedUpdate;

    private Enemy enemy;
    private KnockBack knockBack;


    private void Awake() {
        enemy = GetComponent<Enemy>();
        knockBack = GetComponent<KnockBack>();

        playerReferencePosition = GameManager.Instance.GetPlayer().GetPlayerPosition();
    }

    private void Update() {
        MoveEnemy();
    }

    private void MoveEnemy() {


        currentEnemyPathRebuildCooldown -= Time.deltaTime;




        // 仅在特定帧上进行a*算法重建路径，从而在敌人之间分散负载
        if (Time.frameCount % Settings.targetFrameRateToSpreadPathfindingOver != updateFrameNumber) return;


        // 移动CD达到 || 玩家移动距离超过限定
        // 重新构建enemy移动路径，并移动enemy
        if (currentEnemyPathRebuildCooldown <= 0f || (CalculateDistanceToPlayer(playerReferencePosition) > Settings.playerMoveDistanceToRebuildPath)) {

            // 重制cd
            currentEnemyPathRebuildCooldown = Settings.enemyPathRebuildCooldown;

            // 重制玩家位置引用
            playerReferencePosition = GameManager.Instance.GetPlayer().GetPlayerPosition();


            // 使用PAStar算法移动enemy - 触发构建玩家追踪路径
            Vector3 nextPosition = GameManager.Instance.GetPlayer().GetPlayerPosition();

            if (Vector3.Distance(nextPosition, transform.position) > 0.2f) {

                if (moveEnemyRoutine != null) {
                    enemy.idleEvent.CallIdleEvent();
                    StopCoroutine(moveEnemyRoutine);
                }

                moveEnemyRoutine = StartCoroutine(MoveEnemyRoutine());

            } else {
                enemy.idleEvent.CallIdleEvent();
            }

        }
    }

    private IEnumerator MoveEnemyRoutine() {
        Vector3 nextPosition = GameManager.Instance.GetPlayer().GetPlayerPosition();

        while (Vector3.Distance(nextPosition, transform.position) > 0.2f) {
            if (knockBack == null || !knockBack.isKnockBack) {
                enemy.movementToPositionEvent.CallMovementToPositionEvent(nextPosition, transform.position,
                               moveSpeed,
                               (nextPosition - transform.position).normalized);
            }

            yield return waitForFixedUpdate;// 等待下一帧
        }
        // 移动结束，触发idle事件
        enemy.idleEvent.CallIdleEvent();
    }

    private float CalculateDistanceToPlayer(Vector3 position) {
        return Vector3.Distance(position, GameManager.Instance.GetPlayer().GetPlayerPosition());
    }

    public void SetUpdateFrameNumber(int updateFrameNumber) {
        this.updateFrameNumber = updateFrameNumber;
    }


}
