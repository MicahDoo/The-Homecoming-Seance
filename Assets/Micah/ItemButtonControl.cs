using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemButtonControl : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    private Text buttonText;
    public BackpackMenuControl mainButton;
    private RectTransform rectTransform;
    private Vector2 startingPosition;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startingPosition = rectTransform.anchoredPosition;
        canvas = GetComponentInParent<Canvas>();
        Debug.Log(canvas.name + "AddedAsTheCanvasOfButton");
        buttonText = GetComponentInChildren<Text>();
        Debug.Log(buttonText.name + "AddedAsButtonText");
        mainButton = GetComponentInParent<BackpackMenuControl>();
        Debug.Log(mainButton.name + "AddedAsMainButton");
        canvasGroup = GetComponent<CanvasGroup>();
        //buttonText = GetComponent<Text>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = 0.3f;
        canvasGroup.blocksRaycasts = false;
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

            Ray ray = Camera.main.ScreenPointToRay(eventData.position);
            RaycastHit rayCastHit;
            if (Physics.Raycast(ray, out rayCastHit))
            {
                Debug.Log("Using" + buttonText.text);
                if(InventoryControl.applyItemOn(name, rayCastHit.collider.gameObject)){
                    Debug.Log("MenuCollapsed");
                    mainButton.collapseMenu();
                    InventoryControl.getOffControlPanel();
                }
                else //doesn't have use here
                {
                    LanguageControl.setPromptText("NoUse");
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
}
