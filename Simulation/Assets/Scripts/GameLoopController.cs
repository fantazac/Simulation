using UnityEngine;

public class GameLoopController : MonoBehaviour
{
    private GameObject ballPrefab;

    private GameObject currentlySpawnedBall;

    private Transform spawner1;
    private Transform spawner2;

    private Transform currentSpawner;

    private int pointsSpawner1;
    private int pointsSpawner2;

    private const float LOWEST_POINT_FOR_BALLS = -2f;
    private const int POINTS_REQUIRED_TO_WIN = 5;

    private void Start()
    {
        InitializeGame();
    }

    private void Update()
    {
        if (!ABallIsInPlay())
        {
            ChangeSpawner();
            SpawnNewBall();
        }
        else if (CurrentSpawnedBallReachedLowestPoint())
        {
            if (CurrentBallHitAColorChanger())
            {
                AddPointToCurrentSpawner();
            }
            Destroy(currentlySpawnedBall);

            if (ASpawnerWon())
            {
                NotifyGameWinner();
                ExitGame();
            }
        }
    }

    private void InitializeGame()
    {
        ballPrefab = Resources.Load<GameObject>("Prefabs/Ball");

        spawner1 = Instantiate(Resources.Load<GameObject>("Prefabs/Spawners/Spawner1")).transform;
        spawner2 = Instantiate(Resources.Load<GameObject>("Prefabs/Spawners/Spawner2")).transform;

        Instantiate(Resources.Load<GameObject>("Prefabs/Floor"));

        Instantiate(Resources.Load<GameObject>("Prefabs/ColorGivers/RedColorGiver"));
        Instantiate(Resources.Load<GameObject>("Prefabs/ColorGivers/OrangeColorGiver"));
        Instantiate(Resources.Load<GameObject>("Prefabs/ColorGivers/YellowColorGiver"));
        Instantiate(Resources.Load<GameObject>("Prefabs/ColorGivers/GreenColorGiver"));
        Instantiate(Resources.Load<GameObject>("Prefabs/ColorGivers/BlueColorGiver"));
        Instantiate(Resources.Load<GameObject>("Prefabs/ColorGivers/PurpleColorGiver"));
    }

    private bool ABallIsInPlay()
    {
        return currentlySpawnedBall;
    }

    private void ChangeSpawner()
    {
        if (currentSpawner == spawner1)
        {
            currentSpawner = spawner2;
        }
        else
        {
            currentSpawner = spawner1;
        }
    }

    private void SpawnNewBall()
    {
        currentlySpawnedBall = Instantiate(ballPrefab, currentSpawner.position, transform.rotation);
        currentlySpawnedBall.GetComponent<BallCollisionDetector>().SetInitialSpeed(currentSpawner == spawner2);
    }

    private bool CurrentSpawnedBallReachedLowestPoint()
    {
        return currentlySpawnedBall.transform.position.y <= LOWEST_POINT_FOR_BALLS;
    }

    private bool CurrentBallHitAColorChanger()
    {
        return currentlySpawnedBall.GetComponent<MeshRenderer>().material.color != Color.white;
    }

    private void AddPointToCurrentSpawner()
    {
        if (currentSpawner == spawner1)
        {
            pointsSpawner1++;
        }
        else
        {
            pointsSpawner2++;
        }
        Debug.Log("Spawner1: " + pointsSpawner1 + " - Spawner2: " + pointsSpawner2);
    }

    private bool ASpawnerWon()
    {
        return pointsSpawner1 == POINTS_REQUIRED_TO_WIN || pointsSpawner2 == POINTS_REQUIRED_TO_WIN;
    }

    private void NotifyGameWinner()
    {
        if (pointsSpawner1 == POINTS_REQUIRED_TO_WIN)
        {
            Debug.Log("GAME FINISHED - Spawner1 WINS!");
        }
        else
        {
            Debug.Log("GAME FINISHED - Spawner2 WINS!");
        }
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
    }
}
