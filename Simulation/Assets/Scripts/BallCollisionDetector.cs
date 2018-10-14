using UnityEngine;

public class BallCollisionDetector : MonoBehaviour
{
    private float timeSinceCreation;
    private Vector3 speed;
    private float initialYSpeed;
    private MySphereCollider sphereCollider;

    private const float ACCELERATION = -9.8f;
    private const float SPEED_PERCENTAGE_KEPT_ON_COLLISION_WITH_FLOOR = 0.9f;

    private const int GROUND_LAYER = 8;
    private const int COLOR_GIVER_LAYER = 9;

    private void Awake()
    {
        sphereCollider = GetComponent<MySphereCollider>();
    }

    public void SetInitialSpeed(bool isRightSpawner)
    {
        float xMultiplier = isRightSpawner ? -1f : 1f;
        speed = Vector3.right * Random.Range(1f, 4f) * xMultiplier + Vector3.up * Random.Range(1f, 4f) + Vector3.forward * Random.Range(-1f, 1f);
        initialYSpeed = speed.y;
    }

    private void Update()
    {
        if (PhysicsManager.DetectCollisionFromLayer(sphereCollider, GROUND_LAYER) != null)
        {
            if (speed.y < 0)// Prevents the ball from sticking to the ground
            {
                timeSinceCreation = 0;
                initialYSpeed = -speed.y * SPEED_PERCENTAGE_KEPT_ON_COLLISION_WITH_FLOOR;
                speed = new Vector3(speed.x, initialYSpeed, speed.z);
            }

            MyCollider colorGiverCollider = PhysicsManager.DetectCollisionFromLayer(sphereCollider, COLOR_GIVER_LAYER);
            if (colorGiverCollider)
            {
                MeshRenderer ballMeshRenderer = GetComponent<MeshRenderer>();
                MeshRenderer colorGiverMeshRenderer = colorGiverCollider.GetComponent<MeshRenderer>();
                if (!MeshesAreSameColor(ballMeshRenderer, colorGiverMeshRenderer))
                {
                    ballMeshRenderer.material.color = colorGiverMeshRenderer.material.color;
                }
            }
        }

        UpdateBallSpeed();
    }

    private void UpdateBallSpeed()
    {
        timeSinceCreation += Time.deltaTime;
        speed = new Vector3(speed.x, calculateYSpeed(), speed.z);
        transform.position += speed / 60f;
    }

    private float calculateYSpeed()
    {
        return initialYSpeed + ACCELERATION * timeSinceCreation;
    }

    private bool MeshesAreSameColor(MeshRenderer meshA, MeshRenderer meshB)
    {
        return meshA.material.color == meshB.material.color;
    }
}
