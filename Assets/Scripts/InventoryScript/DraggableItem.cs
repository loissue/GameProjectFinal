using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    public Transform OriginalParent { get; set; }
    public InventoryScript Inventory { get; set; }
    public Magazin Magazin { get; set; }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OriginalParent = transform.parent;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (eventData.pointerEnter == null || eventData.pointerEnter.GetComponent<DropZone>() == null)
        {
            transform.SetParent(OriginalParent);
            rectTransform.anchoredPosition = Vector2.zero;
        }
        else
        {
            DropZone dropZone = eventData.pointerEnter.GetComponent<DropZone>();
            if (dropZone != null)
            {
                if (dropZone.dropZoneType == DropZone.DropZoneType.Inventory && Inventory != null)
                {
                    Inventory.UpdateInventory();
                }
                else if (dropZone.dropZoneType == DropZone.DropZoneType.Weapon && Magazin != null)
                {
                    Magazin.UpdateBullets();
                }
            }
        }
    }
}
