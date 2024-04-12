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
            DeathFX deathFX = PoolManager.Instance.ReuseComponent<DeathFX>(GameResources.Instance.deathFXPrefab, transform.position, Quaternion.identity); ;
            deathFX.gameObject.SetActive(true);
            Destroy(gameObject);
        }

    }
}

