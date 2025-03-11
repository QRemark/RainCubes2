using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    private float _minTime = 2f;
    private float _maxTime = 5f;
    private float _timeCounter = 0.1f;

    public void SpawnBomb(Vector3 position)
    {
        Bomb bomb = GetObjectFromPool();

        if (bomb == null) return;

        bomb.transform.position = position;
        bomb.transform.rotation = Quaternion.identity;
        float fadeTime = Random.Range(_minTime, _maxTime + _timeCounter);

        bomb.Init(fadeTime);
    }
}