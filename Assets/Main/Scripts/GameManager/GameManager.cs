using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager> {
    [SerializeField] private int level;
    private Player player;

    protected override void Awake() {
        base.Awake();

    }

    private void Start() {

        GameObject currentPlayer = Instantiate(GameResources.Instance.playerPrefab, Vector3.zero, Quaternion.identity);
        player = currentPlayer.GetComponent<Player>();
        player.Initilaize();
    }


    public Player GetPlayer() => player;

    public int GetCurrentLevel() => level;

}
