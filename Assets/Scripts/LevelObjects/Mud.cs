public class Mud : PlayerinteractableObject
{
    public override void InteractionOnCollision()
    {
        EventsBroadcaster.OnFail.Invoke();
        GetMuddy();
    }

    public override void Awake()
    {
        base.Awake();
        InteractableObjectType = InteractableObjectType.Mud;
    }

    public void GetMuddy()
    {
        PlayerManager.Instance.Locomotion.SetPosByRay();
    }
}

