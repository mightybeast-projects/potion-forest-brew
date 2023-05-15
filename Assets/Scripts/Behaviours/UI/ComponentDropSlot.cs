using UnityEngine;

public class ComponentDropSlot : MonoBehaviour
{
    public bool slotFilled => currentComponent;
    public ComponentBehaviour currentComponent;
}