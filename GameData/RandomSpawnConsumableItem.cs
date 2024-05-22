using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSpawnConsumableItem : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController[] consumableAnimators;
    [SerializeField] private float spawnTime;
    [SerializeField] private int spawnPercent;
    [SerializeField] private float downSpeed;
    private Coroutine spawnConsumableItemCoroutine;
    private void Start()
    {
        StartItemRandomSpawn();
    }

    private void StartItemRandomSpawn()
    {
        if (spawnConsumableItemCoroutine != null)
        {
            StopCoroutine(spawnConsumableItemCoroutine);
        }

        spawnConsumableItemCoroutine = StartCoroutine(SpawnConsumableItemCoroutine());
    }

    private IEnumerator SpawnConsumableItemCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnTime);
        
        while (true)
        {
            int per = Random.Range(1, 101);

            if (per < spawnPercent)
            {
                // 생성
                ConsumableItem consumableItem = GameManager.Instance.Pool.SpawnFromPool(EPoolObjectType.ConsumableItem)
                    .ReturnMyConponent<ConsumableItem>();
                
                float randPos  = Random.Range(-3, 3);
                consumableItem.transform.position = new Vector3(randPos, 7, 0);
                
                
                int conItemType =  Random.Range(0, 3);
                consumableItem.ConsumableInit(consumableAnimators[conItemType], (EConsumableItemType)conItemType + 1, downSpeed);
            }
            yield return wait;
        }
    }
}