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
        yield return _hidePanelButton.DOLocalMoveX(696.5f, 0.5f).SetEase(Ease.InCubic).WaitForCompletion();
        yield return _panel.DOLocalMoveX(847.5f, 0.5f).SetEase(Ease.OutCubic).WaitForCompletion();
    }

    public IEnumerator HidePanel()
    {
        _isVisible = false;
        yield return _panel.DOLocalMoveX(1200f, 0.5f).SetEase(Ease.InCubic).WaitForCompletion();
        yield return _hidePanelButton.DOLocalMoveX(900f, 0.5f).SetEase(Ease.OutCubic).WaitForCompletion();
    }
    
    public Transform GetNextAvailableSlot()
    {
        return _slots.FirstOrDefault(s => s.childCount == 0);
    }
}
