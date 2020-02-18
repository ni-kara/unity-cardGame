using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private GameObject cloneCard;
    private bool onceTime = false;

    public void OnDrag(PointerEventData eventData)
    {
        if (!onceTime && gameObject.GetComponent<Monster>().Id!="")
        {
            cloneCard = Instantiate(gameObject, gameObject.transform.position, Quaternion.identity, GameObject.Find("Canvas").GetComponent<Transform>());
            cloneCard.AddComponent<BoxCollider2D>().size = new Vector2(100,174);
            cloneCard.GetComponent<BoxCollider2D>().isTrigger = true;
            onceTime = true;            
        }

        if(cloneCard)
            cloneCard.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (onceTime)
        {
            Destroy(cloneCard.gameObject);
            onceTime = false;
        }
    }   
    
}
