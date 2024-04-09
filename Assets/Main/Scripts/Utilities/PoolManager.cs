using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingletonMonobehaviour<PoolManager> {
    [System.Serializable]
    public struct Pool {
        public GameObject prefab;
        public int poolSize;

    }

    [SerializeField] private Pool[] poolArray;


    private Transform objectTransform;
    private Dictionary<int, Queue<GameObject>> poolDicionary = new Dictionary<int, Queue<GameObject>>();


    private void Start() {
        objectTransform = this.gameObject.transform;

        for (int i = 0; i < poolArray.Length; i++) {
            Pool pool = poolArray[i];
            CreatePool(pool.prefab, pool.poolSize);
        }
    }

    private void CreatePool(GameObject prefab, int poolSize) {

        int poolKey = prefab.GetInstanceID();

        string prefabName = prefab.name;
        GameObject parentGameObject = new GameObject(prefabName + "_Anchor");
        parentGameObject.transform.SetParent(objectTransform);

        if (!poolDicionary.ContainsKey(poolKey)) {
            Queue<GameObject> queue = new Queue<GameObject>();
            poolDicionary.Add(poolKey, queue);
            for (int i = 0; i < poolSize; i++) {
                GameObject newObject = Instantiate(prefab, parentGameObject.transform);
                newObject.SetActive(false);
                queue.Enqueue(newObject);
            }
        }
    }

    public T ReuseComponent<T>(GameObject prefab, Vector3 position, Quaternion rotation) {
        int poolKey = prefab.GetInstanceID();

        if (poolDicionary.ContainsKey(poolKey)) {
            GameObject objectToReuse = GetGameObjectFromPool(poolKey);
            ResetObject(objectToReuse, position, rotation);
            return objectToReuse.GetComponent<T>();
        } else {
            Debug.LogError("No Object pool for :" + prefab);
            return default;
        }
    }

    private GameObject GetGameObjectFromPool(int poolKey) {
        GameObject gameObjectToReuse = poolDicionary[poolKey].Dequeue();
        poolDicionary[poolKey].Enqueue(gameObjectToReuse);
        if (gameObjectToReuse.activeSelf) {
            gameObjectToReuse.SetActive(false);
        }
        return gameObjectToReuse;
    }

    private void ResetObject(GameObject objectToReuse, Vector3 position, Quaternion rotation) {
        objectToReuse.transform.SetPositionAndRotation(position, rotation);
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate() {
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(poolArray), poolArray);
    }

#endif
    #endregion
}


