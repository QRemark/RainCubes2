using System;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody), typeof(ColorHandler))]
public class Cube : MonoBehaviour, IDisappearable
{
    private ColorHandler _colorChanger;
    private Renderer _renderer;
    private Rigidbody _rigidbody;

    private bool _isColorChanged = false;

    private float _lifeTime;
    private float _minLifeTimer = 2.0f;
    private float _maxLifeTimer = 5.0f;
    private float _maxLifeTimerConfrime = 0.1f;

    public event Action<IDisappearable> Disappeared;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _colorChanger = GetComponent<ColorHandler>();
    }

    public void Init(Color initialColor)
    {
        _isColorChanged = false;
        _renderer.material.color = initialColor;
    }

    public void Disappear()
    {
        CancelInvoke(nameof(NotifyTimeEnd));

        _colorChanger.ReturnColor();
        Disappeared?.Invoke(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform))
        {
            if (_isColorChanged == false)
            {
                _isColorChanged = true;
                _colorChanger.ChangeColor();
                StartLifeTimer();
            }
        }
    }

    private void StartLifeTimer()
    {
        _lifeTime = UnityEngine.Random.Range(_minLifeTimer, _maxLifeTimer + _maxLifeTimerConfrime);
        Invoke(nameof(NotifyTimeEnd), _lifeTime);
    }

    private void NotifyTimeEnd()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        Disappear();
    }   
}
