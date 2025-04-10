using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostersDialogController : MonoBehaviour
{
    [SerializeField] private List<Image> _boosterPlacements;

    [SerializeField] private BoosterManager _boosterManager;

    [SerializeField] private GameObject _okButton;

    private GameObject _activeBoosterHighlighter;

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
        
        if(_activeBoosterHighlighter != null)
        {
            _activeBoosterHighlighter.SetActive(false);
            _activeBoosterHighlighter = null;
            _okButton.SetActive(false);
        }
    }

    public void OnBoosterPlacementClicked(GameObject hightlighter)
    {
        if(_activeBoosterHighlighter != null)
        {
            _activeBoosterHighlighter.SetActive(false);
        }

        hightlighter.SetActive(true);
        _activeBoosterHighlighter = hightlighter;
        if(!_okButton.activeSelf)
        {
            _okButton.SetActive(true);
        }
    }
}
