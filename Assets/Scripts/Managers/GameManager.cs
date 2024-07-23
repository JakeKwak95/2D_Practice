using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        EventsBroadcaster.OnRetry += ToSavePoint;
    }
    private void OnDisable()
    {
        EventsBroadcaster.OnRetry -= ToSavePoint;
    }

    private void ToSavePoint()
    {
        SavePoint.CurrentSavePoint.PlayToSavePoint();
    }

    public void Retry()
    {
        EventsBroadcaster.OnRetry.Invoke();
    }
}
