using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomSpawnableObject<T> {

    private struct chanceBoundaries {
        public T spawnableObject;
        public int lowerBoundaryValue;
        public int highBoundaryValue;
    }

    private int ratioValueTotal = 0;
    private List<chanceBoundaries> chanceBoundarieList = new List<chanceBoundaries>();
    private List<SpawnableObjectsByLevel<T>> spawnableObjectsByLevelList;

    public RandomSpawnableObject(List<SpawnableObjectsByLevel<T>> spawnableObjectsByLevelList) {
        this.spawnableObjectsByLevelList = spawnableObjectsByLevelList;
    }

    public T GetItem() {
        int upperBoundary = -1;
        ratioValueTotal = 0;
        chanceBoundarieList.Clear();
        T spawnableObject = default(T);

        foreach (SpawnableObjectsByLevel<T> spawnableObjectsByLevel in spawnableObjectsByLevelList) {
            if (spawnableObjectsByLevel.level == GameManager.Instance.GetCurrentLevel()) {
                foreach (SpawnableObjectRatio<T> spawnableObjectRatio in spawnableObjectsByLevel.spawnableObjectRatioList) {
                    int lowerBoundary = upperBoundary + 1;
                    upperBoundary = lowerBoundary + spawnableObjectRatio.ratio - 1;
                    ratioValueTotal += spawnableObjectRatio.ratio;

                    chanceBoundarieList.Add(new chanceBoundaries() {
                        spawnableObject = spawnableObjectRatio.dungeonObject,
                        lowerBoundaryValue = lowerBoundary,
                        highBoundaryValue = upperBoundary
                    });
                }
            }

        }

        if (chanceBoundarieList.Count == 0) return default(T);

        int lookUpValue = Random.Range(0, ratioValueTotal);
        foreach (chanceBoundaries spawnChance in chanceBoundarieList) {
            if (lookUpValue >= spawnChance.lowerBoundaryValue && lookUpValue <= spawnChance.highBoundaryValue) {
                spawnableObject = spawnChance.spawnableObject;
                break;
            }
        }
        return spawnableObject;
    }

}