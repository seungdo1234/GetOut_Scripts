using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    [SerializeField] [Range(7, 30)] private int itemRandomInt;
    
    [SerializeField] private GameObject AutoCannon; // 배열????에 넣고 반복문.
    [SerializeField] private GameObject Rockets;
    [SerializeField] private GameObject Zapper;

    public SpecialWeaponEquipHandler SpecialWeaponEquipHandler { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
         }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SpecialWeaponEquipHandler = FlightDataManager.Instance.PlayerFlightStat.GetComponent<SpecialWeaponEquipHandler>();
    }

    // 매개 변수로 몬스터 위치 불러와야되나?
    public void DropInfo(Vector3 position)
    {
        int Randomint = Random.Range(0, itemRandomInt);

        if(Randomint < 0)
        {
            itemInstantiate(AutoCannon, position, EWeaponType.AutoCannon);
        }
        else if (Randomint < 3)
        {
            itemInstantiate(Rockets, position, EWeaponType.Rockets);
        }
        else if (Randomint < 6)
        {
            itemInstantiate(Zapper, position, EWeaponType.Zapper);
        }
        else
        {
            Debug.Log("아무일도... 없었다...");
        }
    }

    private void itemInstantiate(GameObject items , Vector3 position, EWeaponType eWeaponType)
    {
        Item item = Instantiate(items, position, items.transform.rotation).GetComponent<Item>(); // 반복문 으로????
        item.ItemInit(eWeaponType);
    }
}
