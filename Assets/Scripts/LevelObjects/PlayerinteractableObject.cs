using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class PlayerinteractableObject : MonoBehaviour
{
    public InteractableObjectType InteractableObjectType { get; protected set; }
    public abstract void InteractionOnCollision();

    public virtual void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
