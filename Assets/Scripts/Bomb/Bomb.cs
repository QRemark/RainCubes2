using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer), typeof(Rigidbody), typeof(ColorHandler))]
public class Bomb : MonoBehaviour, IDisappearable
{
    private ColorHandler _colorHandler;

    private float _fadeTime;
    private float _explosionForce = 30f;
    private float _explosionRadius = 15f;
    private float _upwardsModifier = 3f;
    private float _maxAlpha = 1f;
    private float _minAlpha = 0f;

    public event Action<IDisappearable> OnDisappeared;

    private void Awake()
    {
        _colorHandler = GetComponent<ColorHandler>();
    }

    public void Init(float fadeTime)
    {
        _fadeTime = fadeTime;
        _colorHandler.SetColor(Color.black);

        StartCoroutine(FadeAndExplode());
    }

    public void Disappear()
    {
        if (OnDisappeared != null)
        {
            OnDisappeared(this);
            ResetBomb();
        }
    }

    private IEnumerator FadeAndExplode()
    {
        float time = 0;

        while (time < _fadeTime)
        {
            float alpha = Mathf.Lerp(_maxAlpha, _minAlpha, time / _fadeTime);
            _colorHandler.SetAlpha(alpha);
            time += Time.deltaTime;

            yield return null;
        }

        TryExplode();
    }

    private void TryExplode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider collieder in colliders)
        {
            if (collieder.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, _upwardsModifier, ForceMode.Impulse);
        }

        Disappear();
    }

    private void ResetBomb()
    {
        StopCoroutine(FadeAndExplode());
    }
}
