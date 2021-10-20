using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drop : MonoBehaviour
{
    public Image followMouseImage;

    void Update()
    {
        followMouseImage.transform.position = Input.mousePosition;

        if (Input.GetMouseButton(1))
        {
            Debug.Log("removed");
            GameObject obj = GetObjectUnderMouse();
            if (obj) { obj.GetComponent<Slot>().DropItem(); }
        }

    }
    GameObject GetObjectUnderMouse()
    {
        GraphicRaycaster raycaster = GetComponent<GraphicRaycaster>();
        PointerEventData eventData = new PointerEventData(EventSystem.current);

        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        raycaster.Raycast(eventData, results);

        foreach (RaycastResult i in results)
        {
            if (i.gameObject.GetComponent<Slot>())
                return i.gameObject;
        }
        return null;
    }
}
