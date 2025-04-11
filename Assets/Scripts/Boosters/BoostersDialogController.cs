using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BoostersDialogController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private List<Image> _boosterPlacements;
    [SerializeField] private List<BoosterItemAnimations> _boosterAnimations;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _okButton;
    [SerializeField] private GameObject _refreshButton;

    [Header("Transforms")]
    [SerializeField] private Transform _centerPoint;
    [SerializeField] private Transform _slotPoint;

    [Header("Prefabs & Effects")]
    [SerializeField] private GameObject _checkMarkPrefab;
    [SerializeField] private ParticleSystem _confirmEffect;

    [Header("Managers & Settings")]
    [SerializeField] private BoosterManager _boosterManager;
    [SerializeField] private SidePanelController _sidePanelController;
    [SerializeField] private AnimationConfig _animationConfig;

    private BoosterItemAnimations _activeBooster;

    public void Start()
    {
        RefreshBoosterUI();
    }

    /// <summary>
    /// Called from UI button to refresh boosters
    /// </summary>
    public void OnBoosterRefreshButtonClicked()
    {
        RefreshBoosterUI();
    }

    /// <summary>
    /// Refreshes the booster UI
    /// </summary>
    private void RefreshBoosterUI()
    {
        var boosters = _boosterManager.GenerateBoosters();
        
        for(int i = 0; i < _boosterPlacements.Count; i++)
        {
            _boosterPlacements[i].sprite = boosters[i].icon;
        }
        
        DeselectActiveBooster();
        _okButton.SetActive(false);
    }

    /// <summary>
    /// Called from UI button to select a booster
    /// </summary>
    /// <param name="booster"></param>
    public void OnBoosterPlacementClicked(BoosterItemAnimations booster)
    {
        if(_activeBooster == booster)
            return;

        DeselectActiveBooster();

        _activeBooster = booster;
        _activeBooster.Select();
        _okButton.SetActive(true);
    }

    /// <summary>
    /// Deselects the currently selected booster
    /// </summary>
    private void DeselectActiveBooster()
    {
        if(_activeBooster != null)
        {
            _activeBooster.Deselect();
            _activeBooster = null;
        }
    }

    /// <summary>
    /// Called from UI button to confirm the selected booster
    /// </summary>
    public void OnOkButtonClicked()
    {
        _okButton.SetActive(false);
        _refreshButton.SetActive(false);
        StartCoroutine(PlayBoosterSelectionSequence());
    }

    /// <summary>
    /// Plays the booster selection sequence
    /// </summary>
    private IEnumerator PlayBoosterSelectionSequence()
    {
        HideNonSelectedBoosters(_animationConfig.hideNonSelectedBoostersDuration);
        yield return new WaitForSeconds(_animationConfig.hideNonSelectedBoostersDuration);

        yield return _activeBooster.AnimateToCenter(_centerPoint.position, _animationConfig.animateToCenterDuration);
        GameObject checkmark = Instantiate(_checkMarkPrefab, _activeBooster.transform);
        PlayConfirmEffect();
        yield return new WaitForSeconds(_animationConfig.animateToCenterDuration);

        if(!_sidePanelController.IsVisible)
        {
            yield return _sidePanelController.ShowPanel();
        }
        checkmark.SetActive(false);

        yield return AnimateBoosterToSlot(_animationConfig.animateToSlotDuration);
        yield return new WaitForSeconds(_animationConfig.animateToSlotDuration);
        yield return _sidePanelController.HidePanel();

        FadeOutDialog(_animationConfig.fadeOutDialogDuration);
    }

    /// <summary>
    /// Hides the non selected boosters
    /// </summary>
    /// <param name="duration"> Duration of the animation </param>
    private void HideNonSelectedBoosters(float duration)
    {
        foreach(var booster in _boosterAnimations)
        {
            if(booster != _activeBooster)
            {
                booster.Hide(duration);
            }
        }
    }

    /// <summary>
    /// Plays the confirm effect
    /// </summary>
    private void PlayConfirmEffect()
    {
        _confirmEffect.transform.position = _centerPoint.position;
        _confirmEffect.Play();   
    }

    /// <summary>
    /// Animates the booster to the slot
    /// </summary>
    /// <param name="duration"> Duration of the animation </param>
    private IEnumerator AnimateBoosterToSlot(float duration = 0.6f)
    {
        yield return _activeBooster.AnimateToSlot(_slotPoint.position, duration);
        _activeBooster.transform.SetParent(_slotPoint);
    }

    /// <summary>
    /// Fades out the dialog
    /// </summary>
    /// <param name="duration"> Duration of the animation </param>
    private void FadeOutDialog(float duration)
    {
        DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 0, duration)
            .OnComplete(() => _canvasGroup.gameObject.SetActive(false));
    }
}
