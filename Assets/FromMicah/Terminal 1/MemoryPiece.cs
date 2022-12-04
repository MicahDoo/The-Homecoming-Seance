using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MemoryPiece : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector2 startingPosition;
    private Vector3 startingRotation;
    private Vector3 endingRotation;
    private float timeElapsed = 0f;
    private float flippingTime = 0.5f;
    private bool flip = false;
    private bool flipped = false;

    void Start ()
    {
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        if (flip)
        {
            Debug.Log("Flipping");
            transform.eulerAngles = Vector3.Lerp(startingRotation, endingRotation, timeElapsed/flippingTime);
            timeElapsed += Time.deltaTime;
            if(!flipped && timeElapsed >= flippingTime/2f)
            {
                flipped = true;
                foreach(Transform face in transform)
                {
                    Debug.Log("flipped");
                    face.SetSiblingIndex(1);
                    break;
                }
            }
            if(timeElapsed >= flippingTime)
            {
                transform.eulerAngles = endingRotation;
                timeElapsed = 0f;
                flip = false;
                flipped = false;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!flip)
        {
            Debug.Log("OnPointerClick");
            flip = true;
            startingRotation = transform.eulerAngles;
            endingRotation = transform.eulerAngles + new Vector3(0, 180, 0);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = 0.7f;
        canvasGroup.blocksRaycasts = false;
        startingPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        if (rectTransform.anchoredPosition != startingPosition)
        {
            

            rectTransform.anchoredPosition = startingPosition;

            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");

        if(eventData.pointerDrag.transform.parent.name == "PhotoContainer")
        {
            Debug.Log("sameType");
            eventData.pointerDrag.transform.SetSiblingIndex(transform.GetSiblingIndex());
        }
    }
}
