using UnityEngine;

public class BallCollisionDetector : MonoBehaviour
{
    private float timeSinceCreation;
    private Vector3 speed;
    private float initialYSpeed;

    private const float ACCELERATION = -9.8f;
    private const float SPEED_PERCENTAGE_KEPT_ON_COLLISION_WITH_FLOOR = 0.9f;

    private void Start()
    {
        speed = Vector3.right * Random.Range(1f, 3f) + Vector3.up * Random.Range(2f, 5f) + Vector3.forward * Random.Range(-2f, 2f);
        initialYSpeed = speed.y;
        Debug.Log(speed);
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x * 0.5f);
        foreach (Collider collider in colliders)
        {
            if (collider is BoxCollider)
            {
                timeSinceCreation = 0;
                initialYSpeed = -speed.y * SPEED_PERCENTAGE_KEPT_ON_COLLISION_WITH_FLOOR;
                speed = new Vector3(speed.x, initialYSpeed, speed.z);
                Debug.Log(speed);
            }
            else if (collider is SphereCollider && collider.GetComponent<MeshRenderer>().material.color != GetComponent<MeshRenderer>().material.color)
            {
                GetComponent<MeshRenderer>().material.color = collider.GetComponent<MeshRenderer>().material.color;
            }
        }
        timeSinceCreation += Time.deltaTime;
        speed = new Vector3(speed.x, calculateYSpeed(), speed.z);
        transform.position += speed / 60f;
    }

    private float calculateYSpeed()
    {
        return initialYSpeed + ACCELERATION * timeSinceCreation;
    }
}
