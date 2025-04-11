using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class SidePanelController : MonoBehaviour
{
    [SerializeField] private RectTransform _panel;
    [SerializeField] private List<Transform> _slots;
    [SerializeField] private RectTransform _hidePanelButton;
    [SerializeField] private Transform _defaultHidePanelButtonPosition;
    [SerializeField] private Transform _defaultPanelPosition;
    [SerializeField] private Transform _hiddenPanelPosition;
    [SerializeField] private Transform _foldedHidePanelButtonPosition;
    public bool IsVisible => _isVisible; 
    private bool _isVisible = true;

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

    public IEnumerator ShowPanel()
    {
        _isVisible = true;
        yield return _hidePanelButton.DOMoveX(_defaultHidePanelButtonPosition.position.x, 0.5f).SetEase(Ease.InCubic).WaitForCompletion();
        yield return _panel.DOMoveX(_defaultPanelPosition.position.x, 0.5f).SetEase(Ease.OutCubic).WaitForCompletion();
    }

    public IEnumerator HidePanel()
    {
        _isVisible = false;
        yield return _panel.DOMoveX(_hiddenPanelPosition.position.x, 0.5f).SetEase(Ease.InCubic).WaitForCompletion();
        yield return _hidePanelButton.DOMoveX(_foldedHidePanelButtonPosition.position.x, 0.5f).SetEase(Ease.OutCubic).WaitForCompletion();
    }
    
    public Transform GetNextAvailableSlot()
    {
        return _slots.FirstOrDefault(s => s.childCount == 0);
    }
}
