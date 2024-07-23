public class DeadPoint : PlayerinteractableObject
{

    public override void Awake()
    {
        base.Awake();

        InteractableObjectType = InteractableObjectType.DeadPoint;
    }
    public override void InteractionOnCollision()
    {
        EventsBroadcaster.OnRetry.Invoke();
    }
}
