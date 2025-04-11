using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BoostersDialogController : MonoBehaviour
{
    [SerializeField] private List<Image> _boosterPlacements;

    [SerializeField] private List<BoosterItemAnimations> _boosterAnimations;

    [SerializeField] private BoosterManager _boosterManager;

    [SerializeField] private GameObject _okButton;

    [SerializeField] private Transform _centerPoint;

    [SerializeField] private Transform _slotPoint;

    [SerializeField] private GameObject _checkMarkPrefab;

    [SerializeField] private ParticleSystem _confirmEffect;

    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] private SidePanelController _sidePanelController;

    [SerializeField] private GameObject _refreshButton;

    private BoosterItemAnimations _activeBooster;

    public void Start()
    {
        OnBoosterRefreshButtonClicked();
    }

    public void OnBoosterRefreshButtonClicked()
    {
        List<Booster> newBoosters = _boosterManager.GenerateBoosters();
        RefreshBoosters(newBoosters);
    }

    private void RefreshBoosters(List<Booster> newBoosters)
    {
        for(int i = 0; i < _boosterPlacements.Count; i++)
        {
            _boosterPlacements[i].sprite = newBoosters[i].icon;
        }
        
        if(_activeBooster != null)
        {
            _activeBooster.Deselect();
            _activeBooster = null;
            _okButton.SetActive(false);
        }
    }

    public void OnBoosterPlacementClicked(BoosterItemAnimations booster)
    {
        if(_activeBooster != null)
        {
            _activeBooster.Deselect();
        }

        _activeBooster = booster;
        _activeBooster.Select();

        if(!_okButton.activeSelf)
        {
            _okButton.SetActive(true);
        }
    }

    public void OnOkButtonClicked()
    {
        _okButton.SetActive(false);
        _refreshButton.SetActive(false);
        StartCoroutine(PlayBoosterSelectionSequence());
    }

    private IEnumerator PlayBoosterSelectionSequence()
    {
        HideBoosters();

        yield return new WaitForSeconds(0.3f);

        yield return _activeBooster.AnimateToCenter(_centerPoint.position);

        GameObject checkmark = Instantiate(_checkMarkPrefab, _activeBooster.transform);
        _confirmEffect.transform.position = _centerPoint.position;
        _confirmEffect.Play();

        yield return new WaitForSeconds(0.5f);

        if(!_sidePanelController.IsVisible)
        {
            yield return _sidePanelController.ShowPanel();
        }

        checkmark.SetActive(false);
        yield return _activeBooster.AnimateToSlot(_slotPoint.position);

        _activeBooster.transform.SetParent(_slotPoint);

        yield return new WaitForSeconds(0.5f);
        yield return _sidePanelController.HidePanel();

        DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 0, 0.5f).OnComplete(() => _canvasGroup.gameObject.SetActive(false));

    }

    private void HideBoosters()
    {
        foreach(var booster in _boosterAnimations)
        {
            if(booster != _activeBooster)
            {
                booster.Hide(null);
            }
        }
    }
}
