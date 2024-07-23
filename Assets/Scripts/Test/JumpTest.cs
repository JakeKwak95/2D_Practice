using System.Collections;
using UnityEngine;

public class JumpTest : MonoBehaviour
{
    [SerializeField] float timeToReachTop;
    [SerializeField] float jumpHeight;
    [SerializeField] Vector2 pos;

    private void Awake()
    {
        pos = transform.position;
    }

    private void Update()
    {
        transform.position = pos;
    }

    [ContextMenu("Jump")]
    private void Jump()
    {
        StartCoroutine(CoJump());
    }

    IEnumerator CoJump()
    {
        float timer = 0;
        float velocity = (2 * jumpHeight) / timeToReachTop;
        float fakeGravity = (-2 * jumpHeight) / (timeToReachTop * timeToReachTop);
        float currentY = pos.y;
 
        while (pos.y > 0)
        {
            timer += Time.deltaTime;
            pos.y = fakeGravity * timer * timer / 2 + velocity * timer + currentY;
            print(pos.y);
            yield return null;
        }
    }
}
