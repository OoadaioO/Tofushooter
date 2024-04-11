using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager> {
    [SerializeField] private Player player;
    [SerializeField] private int level;

    protected override void Awake() {
        base.Awake();

    }


    public Player GetPlayer() => player;

    public int GetCurrentLevel() => level;

}
