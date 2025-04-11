using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BoosterItemAnimations : MonoBehaviour
{
    [SerializeField] private GameObject _highlighter;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Select()
    {
        _highlighter.SetActive(true);
    }

    public void Deselect()
    {
        _highlighter.SetActive(false);
    }

    public void Hide(Action onComplete)
    {
        _rectTransform.DOScale(0, 0.3f).SetEase(Ease.InBack).OnComplete(() => onComplete?.Invoke());
    }

    public IEnumerator AnimateToCenter(Vector3 center)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_rectTransform.DOMove(center, 0.5f).SetEase(Ease.InOutQuad));
        sequence.Append(_rectTransform.DOScale(1.3f, 0.5f));
        yield return sequence.WaitForCompletion();
    }

    public IEnumerator AnimateToSlot(Vector3 slot)
    {
        Deselect();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_rectTransform.DOMove(slot, 0.6f).SetEase(Ease.InBack));
        sequence.Append(_rectTransform.DOScale(0.55f, 0.6f));
        yield return sequence.WaitForCompletion();
    }
}
