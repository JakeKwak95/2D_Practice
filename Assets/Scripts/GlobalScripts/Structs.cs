using System;
using UnityEngine;


[Serializable]
public struct ColliderPresets
{
    public Vector2 offset;
    public Vector2 size;

    public ColliderPresets(Vector2 offset, Vector2 size)
    {
        this.offset = offset;
        this.size = size;
    }
}

