using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private CubeColorChanger _colorChananger;

    private Renderer _renderer;
    private Rigidbody _rigidbody;

    public event Action<Cube> OnTimerEnded;

    private bool _isColorChanged = false;

    private float _lifeTime;
    private float _minLifeTimer = 2.0f;
    private float _maxLifeTimer = 5.0f;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Init(Color initialColor)
    {
        _isColorChanged = false;
        _renderer.material.color = initialColor;
    }

    public void ResetSpeed()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform))
        {
            if (_isColorChanged == false)
            {
                _isColorChanged = true;

                _colorChananger.ChangeColor();

                StartLifeTimer();
            }
        }
    }

    private void StartLifeTimer()
    {
        _lifeTime = UnityEngine.Random.Range(_minLifeTimer, _maxLifeTimer + 1.0f);
        Invoke(nameof(NotifyTimeEnd), _lifeTime);
    }

    private void NotifyTimeEnd()
    {
        OnTimerEnded?.Invoke(this);
    }
}
