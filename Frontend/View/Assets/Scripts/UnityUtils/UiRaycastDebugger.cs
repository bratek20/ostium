using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class UiRaycastDebugger : MonoBehaviour
{
    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointerData = new PointerEventData(eventSystem);
            pointerData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerData, results);

            foreach (var result in results)
            {
                Debug.Log("UI Raycast hit: " + result.gameObject.name);
            }

            if (results.Count == 0)
            {
                Debug.Log("No UI element hit.");
            }
        }
    }
}