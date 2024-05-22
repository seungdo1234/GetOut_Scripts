using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 
public class Item : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;
    EWeaponType itemType;
    [SerializeField] private float dropSpeed = 2.0f;
    Rigidbody2D rd;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        rd.velocity = Vector2.down * dropSpeed;
    }

    public void ItemInit(EWeaponType type)
    {
        itemType = type;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsLayerMatched(targetLayer.value, other.gameObject.layer ))
        {
            ItemManager.Instance.SpecialWeaponEquipHandler.EquipSpecialWeapon(itemType);
            AudioManager.instance.PlaySfx(Sfx.ItemEqu);
            Destroy(this.gameObject);
        }
    }
    
    private bool IsLayerMatched(int layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }
}