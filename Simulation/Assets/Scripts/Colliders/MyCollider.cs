using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCollider : MonoBehaviour
{
	void Start()
	{
		// register the component so we can test collisions with it
		PhysicsManager.RegisterCollider(this);
	}

	private void OnDestroy()
	{
	    // remove the component from the collider list so we won't do
	    // more collision tests with it
		PhysicsManager.RemoveCollider(this);
	}

	public virtual bool DoCollisionTest(MyCollider other)
	{
		return false;
	}
}
