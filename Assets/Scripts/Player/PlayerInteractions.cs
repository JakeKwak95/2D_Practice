using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    private void OnEnable()
    {
        EventsBroadcaster.OnFail += HandleFailEvent;
    }
    private void OnDisable()
    {
        EventsBroadcaster.OnFail -= HandleFailEvent;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerinteractableObject interactable))
        {
            interactable.InteractionOnCollision();


            /*            switch (interactable.InteractableObjectType)
                        {
                            case InteractableObjectType.Mud:
                                interactable.InteractionOnCollision(this);
                                break;

                            case InteractableObjectType.Feather:
                                interactable.InteractionOnCollision(this);
                                break;

                            default:
                                break;
                        }*/
        }
    }

    void HandleFailEvent()
    {
        print("Play Lose Sequence :(");
    }
}
