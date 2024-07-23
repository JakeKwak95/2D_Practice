using System.Collections;
using UnityEngine;

public class SavePoint : PlayerinteractableObject
{
    public static SavePoint CurrentSavePoint { get; protected set; }

    [SerializeField] float startingHeight;

    [SerializeField] bool startingSavePoint;

    public override void Awake()
    {
        base.Awake();

        InteractableObjectType = InteractableObjectType.SavePoint;

        if (startingSavePoint) CurrentSavePoint = this;
    }

    public override void InteractionOnCollision()
    {
        CurrentSavePoint = this;
    }

    public void PlayToSavePoint()
    {
        StartCoroutine(CoToSavePoint());
    }

    IEnumerator CoToSavePoint()
    {
        PlayerManager playerManager = PlayerManager.Instance;

        playerManager.Locomotion.SetPos( new Vector2(transform.position.x, transform.position.y+ startingHeight));
        playerManager.InputManager.DisableInput();

        playerManager.Locomotion.FallOff(true);

        while (!playerManager.Locomotion.IsOnGround())
        {
            yield return null;
        }

        playerManager.InputManager.EnableInput();
        CameraManager.Instance.ZoomCamOff();
    }

}