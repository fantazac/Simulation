using UnityEngine;

public class MySphereCollider : MyCollider
{
    private float radius;

    public float Radius { get { return radius; } }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, radius);
    }

    private void Start()
    {
        radius = transform.localScale.x * 0.5f;
    }

    public override bool DetectCollisionWithCollider(MyCollider other)
    {
        if (other is MyBoxCollider)
            return PhysicsManager.OverlapSphereBox(this, (MyBoxCollider)other);

        if (other is MySphereCollider)
            return PhysicsManager.OverlapSphereSphere(this, (MySphereCollider)other);

        Debug.Log("[MyBoxCollider].DetectCollisionWithCollider: Unhandled collision type: " + other.gameObject.name);
        return false;
    }
}
