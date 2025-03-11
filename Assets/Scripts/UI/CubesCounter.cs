using TMPro;
using UnityEngine;

public class CubesCounter : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    [SerializeField] private TMP_Text _cubeActiveCount;
    [SerializeField] private TMP_Text _cubeTotalSpawned;
    [SerializeField] private TMP_Text _cubeTotalCreated;

    private void Start()
    {
        _cubeSpawner.CountersUpdated += UpdateCubeUI;
        
        UpdateCubeUI();
    }

    private void OnDestroy() => _cubeSpawner.CountersUpdated -= UpdateCubeUI;

    private void UpdateCubeUI()
    {
        _cubeActiveCount.text = $"Активные кубы: {_cubeSpawner.ActiveObjectsCount}";
        _cubeTotalSpawned.text = $"Всего заспавлено: {_cubeSpawner.TotalSpawned}";
        _cubeTotalCreated.text = $"Всего создано: {_cubeSpawner.TotalCreatedObjects}";
    }
}
