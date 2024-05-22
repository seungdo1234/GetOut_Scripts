using UnityEngine;

public class BackgroundMoveTrigger : MonoBehaviour
{
    [SerializeField]private LayerMask targetLayer;
    private Vector3 movementVec;

    private void Awake()
    {
        movementVec = new Vector3(transform.position.x, 23.8f, 0f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (IsLayerMatched(targetLayer.value, other.gameObject.layer))
        {
            transform.position = movementVec;
        }
    }
    
    private bool IsLayerMatched(int layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }
}