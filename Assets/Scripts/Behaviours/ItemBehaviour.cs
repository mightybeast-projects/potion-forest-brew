using System.Collections;
using UnityEngine;
using static TweenEngine;

public class ItemBehaviour : MonoBehaviour
{
    public Color color = Color.red;

    public Transform destination;

    void Start()
    {
        var destinationPos = destination.position + (Vector3.up * 0.5f);
        var upDirection = Rotate(DirectionTo(destinationPos), 90);
        var upDistance = DistanceTo(destinationPos) / 3;

        TweenParallel(this,
            Sequence(
                For(0.25f).Move(this).By(upDirection * upDistance),
                For(0.25f).Move(this).By(-upDirection * upDistance)
            ),
            For(0.5f).Move(this).To(destinationPos),
            For(0.5f).Rotate(this).By(360f)
        );
    }

    Vector3 DirectionTo(Vector3 pos) => Vector3.Normalize(pos - transform.position);

    float DistanceTo(Vector3 dest) => Vector3.Distance(transform.position, dest);

    Vector3 Rotate(Vector3 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        return new Vector3(
            (cos * v.x) - (sin * v.y),
            (sin * v.x) + (cos * v.y),
            v.z);
    }
}
