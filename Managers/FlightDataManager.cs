using UnityEngine;

public class FlightDataManager : MonoBehaviour
{
    public static FlightDataManager Instance;

    [SerializeField] private FlightStatHandler playerFlightStat;
    public FlightStatHandler PlayerFlightStat => playerFlightStat;
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
}