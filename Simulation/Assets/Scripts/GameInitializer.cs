using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField]
    private GameObject floorPrefab;
    [SerializeField]
    private GameObject spawner1Prefab;
    [SerializeField]
    private GameObject spawner2Prefab;
    [SerializeField]
    private GameObject colorGiverRedPrefab;
    [SerializeField]
    private GameObject colorGiverOrangePrefab;
    [SerializeField]
    private GameObject colorGiverYellowPrefab;
    [SerializeField]
    private GameObject colorGiverGreenPrefab;
    [SerializeField]
    private GameObject colorGiverBluePrefab;
    [SerializeField]
    private GameObject colorGiverPurplePrefab;

    private void Start()
    {
        Instantiate(floorPrefab);
        Instantiate(spawner1Prefab);
        Instantiate(spawner2Prefab);
        Instantiate(colorGiverRedPrefab);
        Instantiate(colorGiverOrangePrefab);
        Instantiate(colorGiverYellowPrefab);
        Instantiate(colorGiverGreenPrefab);
        Instantiate(colorGiverBluePrefab);
        Instantiate(colorGiverPurplePrefab);
    }
}
