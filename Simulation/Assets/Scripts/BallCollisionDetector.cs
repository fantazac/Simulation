using UnityEngine;

public class BallCollisionDetector : MonoBehaviour
{
    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x * 0.5f);
        if (colliders.Length > 0)
        {
            Debug.Log(colliders[0].gameObject.name);
        }
        transform.position += Vector3.down * 0.01f;
    }
}
