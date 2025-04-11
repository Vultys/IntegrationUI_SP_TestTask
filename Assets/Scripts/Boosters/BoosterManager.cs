using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoosterManager : MonoBehaviour
{
    [SerializeField] private BoostersConfig _boostersConfig;

    private List<Booster> _currentBoosters;

    /// <summary>
    /// Generates a new list of boosters
    /// </summary>
    /// <returns> List of boosters </returns>
    public List<Booster> GenerateBoosters()
    {
        var pool = new List<Booster>(_boostersConfig.boosters);
        var newBoosters = new List<Booster>();

        if(_currentBoosters?.Count > 0)
        {
            var availableBoosters = GetShuffledList(pool.Except(_currentBoosters).ToList());
            newBoosters.AddRange(availableBoosters.Take(_boostersConfig.unchangedBoosters));
        }
        else
        {
            var shuffledPool = GetShuffledList(pool);
            newBoosters.AddRange(shuffledPool.Take(_boostersConfig.unchangedBoosters));
        }

        newBoosters.Add(GenerateThirdBooster(pool, newBoosters));

        _currentBoosters = GetShuffledList(newBoosters);
        return _currentBoosters;
    }

    /// <summary>
    /// Generates a third booster
    /// </summary>
    /// <param name="pool"> List of boosters </param>
    /// <param name="currentSelection"> List of currently selected boosters </param>
    /// <returns> Booster </returns>
    private Booster GenerateThirdBooster(List<Booster> pool, List<Booster> currentSelection)
    {
        bool reuseOld = _currentBoosters != null 
                        && _currentBoosters.Count > 0 
                        && Random.value < _boostersConfig.thirdBoosterRefreshProbability;

        if(reuseOld)
        {
            return _currentBoosters[Random.Range(0, _currentBoosters.Count)];
        }

        var remaining = pool.Except(currentSelection).ToList();
        if(remaining.Count == 0)
        {
            remaining = pool;
        }

        return remaining[Random.Range(0, remaining.Count)];
    }

    /// <summary>
    /// Shuffles the list
    /// </summary>
    /// <param name="list"> List to shuffle </param>
    /// <returns> Shuffled list </returns>
    private List<Booster> GetShuffledList(List<Booster> list)
    {
        return list.OrderBy(_ => Random.value).ToList();
    }
}
