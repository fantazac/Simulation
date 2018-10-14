using UnityEngine;

public class MyBoxCollider : MyCollider
{
    public Vector3 Min { get; private set; }
    public Vector3 Max { get; private set; }

    protected override void Start()
    {
        base.Start();

        Min = new Vector3(transform.position.x - transform.localScale.x * 0.5f, transform.position.y - transform.localScale.y * 0.5f, transform.position.z - transform.localScale.z * 0.5f);
        Max = -Min;
    }

    public override bool DetectCollisionWithCollider(MyCollider other)
    {
        if (other is MySphereCollider)
        {
            return PhysicsManager.OverlapSphereBox((MySphereCollider)other, this);
        }
        else
        {
            return false;
        }
    }
}
