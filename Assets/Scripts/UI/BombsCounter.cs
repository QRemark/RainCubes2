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
        _bombActiveCount.text = $"�������� ����: {_bombSpawner.ActiveObjectsCount}";
        _bombTotalSpawned.text = $"����� ����������: {_bombSpawner.TotalSpawned}";
        _bombTotalCreated.text = $"����� �������: {_bombSpawner.TotalCreatedObjects}";
    }
}
