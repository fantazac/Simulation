using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    public static PhysicsManager instance = null;
    private List<MyCollider> registeredColliders;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        registeredColliders = new List<MyCollider>();
    }

    public static void RegisterCollider(MyCollider collider)
    {
        if (!instance.registeredColliders.Contains(collider))
            instance.registeredColliders.Add(collider);
    }

    public static void RemoveCollider(MyCollider collider)
    {
        instance.registeredColliders.Remove(collider);
    }

    public static MyCollider DetectCollisionFromLayer(MyCollider collider, int layerMask)
    {
        if (collider == null || instance == null)
            return null;

        foreach (MyCollider otherCollider in instance.registeredColliders)
        {
            if (otherCollider.gameObject.layer != layerMask || otherCollider == collider)
                continue;

            if (collider.DetectCollisionWithCollider(otherCollider))
            {
                return otherCollider;
            }
        }

        return null;
    }

    public static bool OverlapSphereSphere(MySphereCollider sphA, MySphereCollider sphB)
    {
        float distance = Vector3.Distance(sphA.transform.position, sphB.transform.position);
        return distance < sphA.Radius + sphB.Radius;
    }

    public static bool OverlapSphereBox(MySphereCollider sph, MyBoxCollider box)
    {
        Vector3 closestPointInAABB = Vector3.Min(Vector3.Max(sph.transform.position, box.Min), box.Max);
        float distance = Vector3.Distance(closestPointInAABB, sph.transform.position);
        return distance < sph.Radius;
    }
}
