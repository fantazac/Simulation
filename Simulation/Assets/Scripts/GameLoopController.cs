using UnityEngine;

public class GameLoopController : MonoBehaviour
{
    [SerializeField]
    private GameObject ballPrefab;
    [SerializeField]
    private Transform spawner1;
    [SerializeField]
    private Transform spawner2;

    private GameObject currentlySpawnedBall;

    private Transform currentSpawner;

    private int pointsSpawner1;
    private int pointsSpawner2;

    private const float LOWEST_POINT_FOR_BALLS = -2f;
    private const int POINTS_REQUIRED_TO_WIN = 5;

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
                Debug.Log("GAME FINISHED");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
            }
        }
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
}
