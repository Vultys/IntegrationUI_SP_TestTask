using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostersDialogController : MonoBehaviour
{
    [SerializeField] private List<Image> _boosterPlacements;

    [SerializeField] private BoosterManager _boosterManager;

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
    }
}
