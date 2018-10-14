using UnityEngine;

public class MyBoxCollider : MyCollider
{
    private Vector3 min;
    private Vector3 max;

    public Vector3 Min { get { return transform.position + min; } }
    public Vector3 Max { get { return transform.position + max; } }

    public override bool DetectCollisionWithCollider(MyCollider other)
    {
        if (other is MySphereCollider)
            return PhysicsManager.OverlapSphereBox((MySphereCollider)other, this);

        Debug.Log("[MyBoxCollider].DetectCollisionWithCollider: Unhandled collision type: " + other.gameObject.name);
        return false;
    }
}
