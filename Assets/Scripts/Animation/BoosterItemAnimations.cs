using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// Handles the animations of the booster items
/// </summary>
public class BoosterItemAnimations : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private GameObject _highlighter;

    [Header("Animation Settings")]
    [SerializeField] private AnimationConfig _animationConfig;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Highlights the booster item
    /// </summary>
    public void Select()
    {
        _highlighter.SetActive(true);
    }

    /// <summary>
    /// Removes highlight from the booster item
    /// </summary>
    public void Deselect()
    {
        _highlighter.SetActive(false);
    }

    /// <summary>
    /// Hides the booster item with a scale animation
    /// </summary>
    /// <param name="duration"> Duration of the animation </param>
    public void Hide(float duration)
    {
        _rectTransform
            .DOScale(0, duration)
            .SetEase(Ease.InBack);
    }

    /// <summary>
    /// Animates the booster item to the center of the screen
    /// </summary>
    /// <param name="center"> Center of the screen </param>
    /// <param name="duration"> Duration of the animation </param>
    public IEnumerator AnimateToCenter(Vector3 center, float duration)
    {
        yield return PlayMoveAndScaleSequence(center, _animationConfig.boosterItemCenterScale, duration);
    }

    /// <summary>
    /// Animates the booster item to the slot
    /// </summary>
    /// <param name="slot"> Slot to animate to </param>
    /// <param name="duration"> Duration of the animation </param>
    public IEnumerator AnimateToSlot(Vector3 slot, float duration)
    {
        Deselect();
        yield return PlayMoveAndScaleSequence(slot, _animationConfig.boosterItemSlotScale, duration);
    }

    /// <summary>
    /// Plays a sequence of move and scale animations
    /// </summary>
    /// <param name="target"> Target position </param>
    /// <param name="scale"> Scale to apply </param>
    /// <param name="duration"> Duration of the animation </param>
    private IEnumerator PlayMoveAndScaleSequence(Vector3 target, float scale, float duration)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_rectTransform.DOMove(target, duration).SetEase(Ease.InOutQuad));
        sequence.Append(_rectTransform.DOScale(scale, duration));
        yield return sequence.WaitForCompletion();
    }
}
