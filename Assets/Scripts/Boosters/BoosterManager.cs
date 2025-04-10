using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class BoosterManager : MonoBehaviour
{
    [SerializeField] private BoostersConfig _boostersConfig;

    private List<Booster> _currentBoosters;

    public List<Booster> GenerateBoosters()
    {
        List<Booster> newBoosters = new List<Booster>();
        List<Booster> pool = new List<Booster>(_boostersConfig.boosters);

        if(_currentBoosters != null && _currentBoosters.Count > 0)
        {
            var availableBoosters = pool.Except(_currentBoosters).ToList();
            availableBoosters.OrderBy(_ => Random.value).ToList();
            newBoosters.Add(availableBoosters[0]);
            newBoosters.Add(availableBoosters[1]);
        }
        else
        {
            pool.OrderBy(_ => Random.value).ToList();
            newBoosters.Add(pool[0]);
            newBoosters.Add(pool[1]);
        }

        Booster third;
        if(_currentBoosters != null && _currentBoosters.Count > 0 && Random.value < _boostersConfig.thirdBoosterRefreshProbability)
        {
            third = _currentBoosters[Random.Range(0, _currentBoosters.Count)];
        }
        else
        {
            var remaining = pool.Except(newBoosters).ToList();
            third = remaining[Random.Range(0, remaining.Count)];
        }
        
        newBoosters.Add(third);

        newBoosters.OrderBy(_ => Random.value).ToList();
        _currentBoosters = new List<Booster>(newBoosters);

        return newBoosters;
    }
}
