using UnityEngine;

public class TearDrop : MonoBehaviour
{
    [SerializeField] float deadPoint;

    private void Update()
    {
        if(transform.position.y < deadPoint)
            Destroy(gameObject);
    }
}
