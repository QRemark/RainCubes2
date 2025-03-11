using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private float _repeatRate = 0.5f;
    [SerializeField] private float _radiusX = 8f;
    [SerializeField] private float _radiusZ = 8f;
    [SerializeField] private BombSpawner _bombSpawner;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCube), 0.0f, _repeatRate);
    }

    private void SpawnCube()
    {
        Cube cube = GetObjectFromPool();

        if (cube == null)
            return;

        cube.Init(Color.white);

        cube.Disappeared += HandleCubeDisappearance;

        Vector3 spawnPosition = new Vector3(
            _startPoint.position.x + Random.Range(-_radiusX, _radiusX),
            _startPoint.position.y,
            _startPoint.position.z + Random.Range(-_radiusZ, _radiusZ)
        );

        cube.transform.position = spawnPosition;
    }

    private void HandleCubeDisappearance(IDisappearable disappearedItem)
    {
        if (disappearedItem is Cube cube)
        {
            cube.Disappeared -= HandleCubeDisappearance;
            _bombSpawner.SpawnBomb(cube.transform.position);
        }
    }
}