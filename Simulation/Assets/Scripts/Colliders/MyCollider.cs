﻿using UnityEngine;

public abstract class MyCollider : MonoBehaviour
{
    protected virtual void Start()
    {
        PhysicsManager.RegisterCollider(this);
    }

    private void OnDestroy()
    {
        PhysicsManager.RemoveCollider(this);
    }

    public abstract bool DetectCollisionWithCollider(MyCollider other);
}
