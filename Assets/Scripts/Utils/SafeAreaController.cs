using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaController : MonoBehaviour
{
    private RectTransform _panel;

    private void Awake()
    {
        _panel = GetComponent<RectTransform>();
        ApplySafeArea();
    }

    /// <summary>
    /// Applies the safe area to the panel
    /// </summary>
    private void ApplySafeArea()
    {
        Rect safeArea = Screen.safeArea;
        Vector2 anchorMin, anchorMax;
        CalculateAnchors(safeArea, out anchorMin, out anchorMax);

        _panel.anchorMin = anchorMin;
        _panel.anchorMax = anchorMax;
    }

    /// <summary>
    /// Calculates the anchors for the panel
    /// </summary>
    /// <param name="safeArea"> Safe area </param>
    /// <param name="anchorMin"> Anchor min </param>
    /// <param name="anchorMax"> Anchor max </param>
    private void CalculateAnchors(Rect safeArea, out Vector2 anchorMin, out Vector2 anchorMax)
    {
        anchorMin = new Vector2(
            safeArea.x / Screen.width,
            safeArea.y / Screen.height
        );

        anchorMax = new Vector2(
            (safeArea.x + safeArea.width) / Screen.width,
            (safeArea.y + safeArea.height) / Screen.height
        );
    }
}
