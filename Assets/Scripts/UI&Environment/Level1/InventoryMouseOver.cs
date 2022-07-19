using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Debug.Log("Mouse detected");
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Debug.Log("Mouse exit");
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
