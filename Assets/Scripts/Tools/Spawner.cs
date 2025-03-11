using UnityEngine;
using System;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour, IDisappearable
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected int _poolCapacity = 5;
    [SerializeField] protected Transform _poolParent;
    [SerializeField] protected int _poolMaxSize = 10;

    protected Pool<T> _pool;

    public int ActiveObjectsCount => _pool.ActiveCount;
    public int TotalCreatedObjects => _pool.TotalCreated;
    public int TotalSpawned { get; protected set; }

    public event Action OnCountersUpdated;

    protected virtual void Awake()
    {
        if (_pool == null)
        {
            _pool = new Pool<T>();
            _pool.Initialize(_prefab, _poolCapacity, _poolMaxSize, _poolParent);
            _pool.OnPoolChanged += UpdateCounters;
        }
    }

    protected T GetObjectFromPool()
    {
        T obj = _pool.GetObject();

        if (obj == null) return null;

        TotalSpawned++;
        UpdateCounters();
        obj.OnDisappeared += ReturnObjectInPool;

        return obj;
    }

    protected virtual void UpdateCounters()
    {
        OnCountersUpdated?.Invoke();
    }

    private void ReturnObjectInPool(IDisappearable disappearedObject)
    {
        disappearedObject.OnDisappeared -= ReturnObjectInPool;
        _pool.ReleaseObject((T)disappearedObject);
        UpdateCounters();
    }
}
