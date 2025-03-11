using TMPro;
using UnityEngine;

public class BombsCounter : MonoBehaviour
{
    [SerializeField] private BombSpawner _bombSpawner;

    [SerializeField] private TMP_Text _bombActiveCount;
    [SerializeField] private TMP_Text _bombTotalSpawned;
    [SerializeField] private TMP_Text _bombTotalCreated;

    private void Start()
    {
        _bombSpawner.OnCountersUpdated += UpdateBombUI;

        UpdateBombUI();
    }

    private void OnDestroy() => _bombSpawner.OnCountersUpdated -= UpdateBombUI;

    private void UpdateBombUI()
    {
        _bombActiveCount.text = $"Активных бомб: {_bombSpawner.ActiveObjectsCount}";
        _bombTotalSpawned.text = $"Всего заспавлено: {_bombSpawner.TotalSpawned}";
        _bombTotalCreated.text = $"Всего создано: {_bombSpawner.TotalCreatedObjects}";
    }
}
