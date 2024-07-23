using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class FeatherToGet : PlayerinteractableObject
{
    Vector3 sinVector = Vector3.zero;
    [SerializeField] float amplitude;
    [SerializeField] float frequency;
    float timer;
    bool isHovering = true;

    [SerializeField] PlayableDirector GetFeatherSequence;

    public override void Awake()
    {
        base.Awake();

        InteractableObjectType = InteractableObjectType.Feather;
        sinVector = transform.position;
    }

    private void Update()
    {
        if (!isHovering) return;
        timer += Time.deltaTime * Mathf.Deg2Rad * 360;
        sinVector.y = amplitude * Mathf.Sin(timer * frequency);
        transform.position = sinVector;
    }

    public void PlaySequence()
    {
        GetFeatherSequence.Play();
    }

    public override void InteractionOnCollision()
    {
        isHovering = false;
        PlaySequence();
        GetComponent<Collider2D>().enabled = false;
    }
}
