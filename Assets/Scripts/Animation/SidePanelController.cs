using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class SidePanelController : MonoBehaviour
{
    [Header("Panel Ellements")]
    [SerializeField] private RectTransform _panel;
    [SerializeField] private RectTransform _toggleButton;

    [Header("Panel Positions")]
    [SerializeField] private Transform _panelVisiblePosition;
    [SerializeField] private Transform _panelHiddenPosition;

    [Header("Toggle Button Positions")]
    [SerializeField] private Transform _buttonVisiblePosition;
    [SerializeField] private Transform _buttonHiddenPosition;

    [Header("Animation Settings")]
    [SerializeField] private AnimationConfig _animationConfig;

    private bool _isVisible = true;
    public bool IsVisible => _isVisible; 

    /// <summary>
    /// Called from UI button to toggle panel visibility
    /// </summary>
    public void OnTogglePanelButtonClicked()
    {
        if(_isVisible)
        {
            StartCoroutine(HidePanel());
        }
        else
        {
            StartCoroutine(ShowPanel());
        }
    }

    /// <summary>
    /// Shows the panel with a move animation
    /// </summary>
    public IEnumerator ShowPanel()
    {
        _isVisible = true;
        yield return AnimatePanel(_toggleButton, _buttonVisiblePosition.position.x, Ease.OutCubic);
        yield return AnimatePanel(_panel, _panelVisiblePosition.position.x, Ease.OutCubic);
    }

    /// <summary>
    /// Hides the panel with a move animation
    /// </summary>
    public IEnumerator HidePanel()
    {
        _isVisible = false;
        yield return AnimatePanel(_panel, _panelHiddenPosition.position.x, Ease.OutCubic);
        yield return AnimatePanel(_toggleButton, _buttonHiddenPosition.position.x, Ease.OutCubic);
    }

    /// <summary>
    /// Animates the panel to the target position
    /// </summary>
    /// <param name="target"> Target RectTransform </param>
    /// <param name="toX"> Target X position </param>
    /// <param name="ease"> Easing curve </param>
    /// <returns> Tween object </returns>
    public Tween AnimatePanel(RectTransform target, float toX, Ease ease)
    {
        return target.DOMoveX(toX, _animationConfig.sidePanelAnimationDuration).SetEase(ease);
    }
}
