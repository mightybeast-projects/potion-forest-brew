using UnityEngine;

public class SpinningBehaviour : MonoBehaviour 
{
    [SerializeField] private float _spinningMultiplier;

    private void FixedUpdate()
    {
        transform.eulerAngles += new Vector3(0, 0, _spinningMultiplier);
    }
}