using UnityEngine;

public class Scurge : MonoBehaviour
{
    private AutoFlightHandler scurgeHandler;

    private void Awake()
    {
        scurgeHandler = GetComponent<AutoFlightHandler>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsLayerMatched(scurgeHandler.TargetLayer.value, other.gameObject.layer))
        {
            HealthSystem healthSystem = other.GetComponent<HealthSystem>();
            float damage = 1;
            if (healthSystem != null)
            {
                healthSystem.ChangeHealth(-damage);
            }
            DisableBullet();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Camera"))
        {
            DisableBullet();
        }
    }

    private void DisableBullet()
    {
        gameObject.SetActive(false);
    }

    // 레이어가 일치하는지 확인하는 메소드입니다.
    private bool IsLayerMatched(int layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }
}
