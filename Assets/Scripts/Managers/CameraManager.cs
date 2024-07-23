using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    [SerializeField] CinemachineVirtualCamera defaultCam;
    [SerializeField] CinemachineVirtualCamera zoomCam;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        EventsBroadcaster.OnFail += ZoomCamOn;
    }
    private void OnDisable()
    {
        EventsBroadcaster.OnFail -= ZoomCamOn;
    }

    public void ZoomCamOn()
    {
        zoomCam.gameObject.SetActive(true);
    }
    public void ZoomCamOff()
    {
        zoomCam.gameObject.SetActive(false);
    }

}


