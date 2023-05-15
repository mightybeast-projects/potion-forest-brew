using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ComponentBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private ComponentDropSlot _currentSlot;
    private Canvas _canvas;
    private GraphicRaycaster _graphicRaycaster;
    private List<RaycastResult> _results;

    private void Awake()
    {
        GameObject ui = GameObject.Find("UI");
        _canvas = ui.GetComponent<Canvas>();
        _graphicRaycaster = ui.GetComponent<GraphicRaycaster>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _currentSlot = transform.parent.GetComponent<ComponentDropSlot>();

        _results = new List<RaycastResult>();
        _graphicRaycaster.Raycast(eventData, _results);

        GetTargetDropSlot();

        if (ItemIsNotOnTreePad())
            transform.SetParent(_currentSlot.transform);
        transform.localPosition = Vector3.zero + new Vector3(0, 25, 0);
    }

    private void GetTargetDropSlot()
    {
        foreach (RaycastResult hit in _results)
        {
            ComponentDropSlot slot = hit.gameObject.GetComponent<ComponentDropSlot>();
            if (slot)
            {
                if (!slot.slotFilled)
                {
                    _currentSlot = slot;
                    _currentSlot.currentComponent = this;
                }

                break;
            }
        }
    }

    private bool ItemIsNotOnTreePad()
    {
        return !_currentSlot.name.Contains("pad");
    }
}