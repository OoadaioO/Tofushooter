using UnityEngine;
using System;

[DisallowMultipleComponent]
public class DestroyedEvent : MonoBehaviour {

    public event Action<DestroyedEvent, DestroyedEventArgs> OnDestoryed;

    public void CallDestroyedEvent(bool playerDied,int points) {
        OnDestoryed?.Invoke(this, new DestroyedEventArgs() {
            playerDied = playerDied,
            points = points
        });
    }

}

public class DestroyedEventArgs : EventArgs {
    public bool playerDied;
    public int points;
}
