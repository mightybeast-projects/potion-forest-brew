using UnityEngine;

public class LightningBehaviour : MonoBehaviour 
{
    [SerializeField] private GameObject _lightningMask;

    public void EnableLightningMask()
    {
        _lightningMask.SetActive(true);
    }

    public void DisableLightningMask()
    {
        _lightningMask.SetActive(false);
    }
}