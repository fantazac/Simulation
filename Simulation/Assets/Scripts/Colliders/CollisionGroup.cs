using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CollisionGroup
{
    public MyCollider parent;
    public MyCollider[] children;
    
    CollisionGroup()
    {
        
    }

    CollisionGroup(MyCollider parent, MyCollider[] children)
    {
        this.parent = parent;
        this.children = children;
    }
}
