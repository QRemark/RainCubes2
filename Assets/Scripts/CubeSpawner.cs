using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{ 
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Cube _prefab;
    
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 15;

    [SerializeField] private float _repeatRate = 0.5f;
    [SerializeField] private float _radiusX = 8f;
    [SerializeField] private float _radiusZ = 8f;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: SubscribeOnCube,
            actionOnRelease: UnsubscribeOnCube,
            actionOnDestroy: (cube) => Destroy(cube.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCube), 0.0f, _repeatRate);
    }

    private Cube GetCube()
    {
        Cube cube = _pool.Get();

        cube.Init(Color.white);

        return cube;
    }

    private void SpawnCube()
    {
        Cube cube = GetCube();

        Vector3 spawnPosition = new Vector3(_startPoint.position.x + Random.Range(-_radiusX, _radiusX),
        _startPoint.position.y,
        _startPoint.position.z + Random.Range(-_radiusZ, _radiusZ));

        cube.transform.position = spawnPosition;

        cube.ResetSpeed();
    }

    private void SubscribeOnCube(Cube cube) 
    {
        cube.gameObject.SetActive(true);
        cube.OnTimerEnded += ReturnCubeInPool;
    }

    private void UnsubscribeOnCube(Cube cube)
    {
        cube.gameObject.SetActive(false);
        cube.OnTimerEnded -= ReturnCubeInPool;
    }

    private void ReturnCubeInPool(Cube cube)
    {
        _pool.Release(cube);
    }
}
