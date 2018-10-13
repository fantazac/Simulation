using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
	public static PhysicsManager instance = null;
	private List<MyCollider> _registeredColliders;
	private List<CollisionGroup> _registeredCollisionGroups;

	private void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
			return;
		}
		
		instance = this;
		_registeredColliders = new List<MyCollider>();
	}

	/* Getter for the current PhysicsManager instance */
	public static PhysicsManager Instance
	{
		get { return instance; }
	}
	
	

	/*	Register a collider
	 *	Registered colliders will be taken into account for collision detection	*/
	public static void RegisterCollider(MyCollider collider)
	{
		// returns if the collider is already in the list
		if (instance._registeredColliders.Contains(collider))
			return;
	
		instance._registeredColliders.Add(collider);
	}
	
	/*	Remove a collider from the list
	 * 	The removed collider will no longer be affected by collisions */
	public static void RemoveCollider(MyCollider collider)
	{		
		instance._registeredColliders.Remove(collider);
	}



	// Do a collision test between every registered colliders
	public static MyCollider[] DoCollisionTest(MyCollider collider)
	{
		List<MyCollider> colliders = new List<MyCollider>();
		
		if (collider == null || instance == null)
			return colliders.ToArray();

		foreach (MyCollider otherCollider in instance._registeredColliders)
		{
			if (otherCollider == collider) // ignore itself
				continue;
			
			if (collider.DoCollisionTest(otherCollider))
			{ // collider and otherCollider does intersect
				Debug.Log("Collision detected between " +
				          collider.gameObject.name + " and " +
				          otherCollider.gameObject.name);
				colliders.Add(otherCollider);
			}
		}
		return colliders.ToArray();
	}
	
	// Do a collision test between the registered colliders having the specified layer
	public static MyCollider[] DoCollisionTest(MyCollider collider, int layerMask)
	{
		List<MyCollider> colliders = new List<MyCollider>();
		
		if (collider == null || instance == null)
			return colliders.ToArray();

		foreach (MyCollider otherCollider in instance._registeredColliders)
		{
			// ignore self AND colliders that does not have the specified layer
			if (otherCollider.gameObject.layer != layerMask || otherCollider == collider)
				continue;
			
			if (collider.DoCollisionTest(otherCollider))
			{ // collider and otherCollider does intersect
				Debug.Log("Collision detected between " +
				          collider.gameObject.name + " and " +
				          otherCollider.gameObject.name);
				colliders.Add(otherCollider);
			}
		}
		return colliders.ToArray();
	}
	
	/* Sphere vs Sphere intersection */
	public static bool OverlapSphereSphere(MySphereCollider sphA, MySphereCollider sphB)
	{
		float distance = Vector3.Distance(sphA.transform.position, sphB.transform.position);
		return distance < sphA.radius + sphB.radius;
	}
	
	/* Sphere vs Box(AABB) intersection */
	public static bool OverlapSphereBox(MySphereCollider sph, MyBoxCollider box)
	{
		Vector3 closestPointInAABB = Vector3.Min(Vector3.Max(sph.transform.position, box.Min), box.Max);
		float distance = Vector3.Distance(closestPointInAABB, sph.transform.position);
		return distance < sph.radius;
	}
	
	/* Box vs Box(AABB) intersection */
	public static bool OverlapBoxBox(MyBoxCollider boxA, MyBoxCollider boxB)
	{
		return false;
	}
}
