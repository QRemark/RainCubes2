using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer), typeof(Rigidbody), typeof(ColorHandler))]
[RequireComponent(typeof(Exploder))]

public class Bomb : MonoBehaviour, IDisappearable
{
    private ColorHandler _colorHandler;
    private Exploder _exploder;

    private float _fadeTime;

    private float _maxAlpha = 1f;
    private float _minAlpha = 0f;

    public event Action<IDisappearable> Disappeared;

    private void Awake()
    {
        _colorHandler = GetComponent<ColorHandler>();
        _exploder = GetComponent<Exploder>();
    }

    public void Init(float fadeTime)
    {
        _fadeTime = fadeTime;
        _colorHandler.SetColor(Color.black);

        StartCoroutine(FadeAndExplode());
    }

    public void Disappear()
    {
        ResetBomb();
        Disappeared?.Invoke(this);
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

        _exploder.Detonate();

        Disappear();
    }

    private void ResetBomb()
    {
        StopCoroutine(FadeAndExplode());
    }
}
