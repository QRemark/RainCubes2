using TMPro;
using UnityEngine;

public class ObjectCounter<T> : MonoBehaviour where T : MonoBehaviour, ICounter
{
    [SerializeField] private T _spawner;
    [SerializeField] private string _activeTextTemplate;
    [SerializeField] private TMP_Text _activeCountText;
    [SerializeField] private TMP_Text _totalSpawnedText;
    [SerializeField] private TMP_Text _totalCreatedText;

    private void Start()
    {
        _spawner.CountersUpdated += UpdateUI;
        UpdateUI();
    }

    private void OnDestroy()
    {
        if (_spawner != null)
            _spawner.CountersUpdated -= UpdateUI;
    }

    private void UpdateUI()
    {
        _activeCountText.text = string.Format(_activeTextTemplate, _spawner.ActiveObjectsCount);
        _totalSpawnedText.text = $"Всего заспавнено: {_spawner.TotalSpawned}";
        _totalCreatedText.text = $"Всего создано: {_spawner.TotalCreatedObjects}";
    }
}