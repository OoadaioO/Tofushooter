using UnityEngine;

[RequireComponent(typeof(DestroyedEvent))]
[DisallowMultipleComponent]
public class Destroyed : MonoBehaviour {
    private DestroyedEvent destroyedEvent;

    private void Awake() {
        destroyedEvent = GetComponent<DestroyedEvent>();
    }

    private void OnEnable() {
        destroyedEvent.OnDestoryed += DestroyedEvent_OnDestoryed;
    }
    private void OnDisable() {
        destroyedEvent.OnDestoryed -= DestroyedEvent_OnDestoryed;
    }

    private void DestroyedEvent_OnDestoryed(DestroyedEvent destroyedEvent, DestroyedEventArgs destoryedEventArgs) {

        if (destoryedEventArgs.playerDied) {
            gameObject.SetActive(false);
        } else {
            Destroy(gameObject);
        }

    }
}

