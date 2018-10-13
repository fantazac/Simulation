using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBoxCollider : MyCollider
{
	public Vector3 min;
	public Vector3 max;

	// Min/Max Getters
	public Vector3 Min { get { return transform.position + min; } }
	public Vector3 Max { get { return transform.position + max; } }
	
	public override bool DoCollisionTest(MyCollider other)
	{
		if (other is MyBoxCollider)
			return PhysicsManager.OverlapBoxBox(this, (MyBoxCollider) other);

		if (other is MySphereCollider)
			return PhysicsManager.OverlapSphereBox((MySphereCollider) other, this);

		Debug.Log("[MyBoxCollider].DoCollision: Unhandled collision type: " + other.gameObject.name);
		return false;
	}
}
