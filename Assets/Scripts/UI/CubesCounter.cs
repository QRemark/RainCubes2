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
        _cubeSpawner.OnCountersUpdated += UpdateCubeUI;
        
        UpdateCubeUI();
    }

    private void OnDestroy() => _cubeSpawner.OnCountersUpdated -= UpdateCubeUI;

    private void UpdateCubeUI()
    {
        _cubeActiveCount.text = $"�������� ����: {_cubeSpawner.ActiveObjectsCount}";
        _cubeTotalSpawned.text = $"����� ����������: {_cubeSpawner.TotalSpawned}";
        _cubeTotalCreated.text = $"����� �������: {_cubeSpawner.TotalCreatedObjects}";
    }
}
