using UnityEngine;

public class PlayerEmotion : MonoBehaviour
{
    [SerializeField] Transform tearSpawnPoint;
    [SerializeField] TearDrop tearDrop;
    [SerializeField] float tearDropDelay;


    private void OnEnable()
    {
        EventsBroadcaster.OnFail += Cry;
        EventsBroadcaster.OnRetry += StopCry;
    }

    private void OnDisable()
    {
        EventsBroadcaster.OnFail -= Cry;
        EventsBroadcaster.OnRetry -= StopCry;
    }

    public void Cry()
    {
        Invoke("SpawnTearDrop", tearDropDelay);
        PlayerManager.Instance.AnimationController.SetTrigger(AnimatorParameters.Cry);
    }

    public void StopCry()
    {
        CancelInvoke();
    }

    void SpawnTearDrop()
    {
        Instantiate(tearDrop, tearSpawnPoint.position, tearSpawnPoint.rotation);
        Invoke("SpawnTearDrop", tearDropDelay);
    }
}
