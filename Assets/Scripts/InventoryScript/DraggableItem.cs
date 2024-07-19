using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private Vector2 originalPosition;
    private Transform originalParent;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
        originalParent = rectTransform.parent;
        canvasGroup.alpha = 0.6f; // Làm mờ item khi kéo
        canvasGroup.blocksRaycasts = false; // Cho phép item được kéo qua các UI element khác
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; // Cập nhật vị trí của item khi kéo
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f; // Trả lại độ trong suốt
        canvasGroup.blocksRaycasts = true; // Không cho phép item được kéo qua các UI element khác

        if (!RectTransformUtility.RectangleContainsScreenPoint(originalParent.GetComponent<RectTransform>(), Input.mousePosition))
        {
            rectTransform.SetParent(originalParent);
            rectTransform.anchoredPosition = originalPosition;
        }
    }
}
