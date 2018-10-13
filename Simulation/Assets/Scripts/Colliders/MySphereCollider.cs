using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySphereCollider : MyCollider
{
    public float radius = 1f;

    // draw gizmos sphere to represent collisions in editor 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, radius);
    }

    // do a collisions test between itself and the other collider
    public override bool DoCollisionTest(MyCollider other)
    {
        if (other is MyBoxCollider)
            return PhysicsManager.OverlapSphereBox(this, (MyBoxCollider) other);

        if (other is MySphereCollider)
            return PhysicsManager.OverlapSphereSphere(this, (MySphereCollider) other);

        Debug.Log("[MyBoxCollider].DoCollision: Unhandled collision type: " + other.gameObject.name);
        return false;
    }
    
}
