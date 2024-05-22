using System;
using UnityEngine;

public class Zapper : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;

    private float damage;
    public void Init(float damage)
    {
        this.damage = damage;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (IsLayerMatched(targetLayer.value, other.gameObject.layer))
        {
            other.gameObject.GetComponent<HealthSystem>().ChangeHealth(-Time.deltaTime * damage );
        }
    }
    
    private bool IsLayerMatched(int layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }
}