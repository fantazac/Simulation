using UnityEngine;

public class MySphereCollider : MyCollider
{
    public float Radius { get; private set; }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, Radius);
    }

    protected override void Start()
    {
        base.Start();

        Radius = transform.localScale.x * 0.5f;
    }

    public override bool DetectCollisionWithCollider(MyCollider other)
    {
        if (other is MyBoxCollider)
        {
            return PhysicsManager.OverlapSphereBox(this, (MyBoxCollider)other);
        }
        else if (other is MySphereCollider)
        {
            return PhysicsManager.OverlapSphereSphere(this, (MySphereCollider)other);
        }
        else
        {
            return false;
        }
    }
}
