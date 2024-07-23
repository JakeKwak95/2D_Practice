using UnityEngine;

public class MathPractice : MonoBehaviour
{
    [SerializeField] Vector2[] points;
    [SerializeField] float debugDuration = 10.0f;

    [ContextMenu("Make Triangle")]
    public void MakeTriangle()
    {
        points[2].x = points[1].x;
        for (int i = 0; i < points.Length; i++)
        {
            if (i == points.Length - 1)
            {
                Debug.DrawLine(points[i], points[0], Color.red, debugDuration);
            }
            else
            {
                Debug.DrawLine(points[i], points[i + 1], Color.red, debugDuration);
            }
        }

        print("atan2 : " + Mathf.Rad2Deg * Mathf.Atan2(Vector2.Distance(points[0], points[1]), Vector2.Distance(points[1], points[2])));
    }
}
