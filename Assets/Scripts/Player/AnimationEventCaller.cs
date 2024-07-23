using UnityEngine;

public class AnimationEventCaller : MonoBehaviour
{
    PlayerManager playerManager;
    void Start()
    {
        playerManager = GetComponentInParent<PlayerManager>();
    }

    public void StartLanding()
    {
        playerManager.Locomotion.StartLanding();
    }

    public void EndLanding()
    {
        playerManager.Locomotion.EndLanding();
    }
}
