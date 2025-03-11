using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorHandler : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void ChangeColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);

        _renderer.material.color = randomColor;
    }

    public void ReturnColor() => _renderer.material.color = Color.white;

    public void SetColor(Color color)
    {
        if (_renderer == null || _renderer.material == null) 
            return;

        _renderer.material.color = color;
    }

    public void SetAlpha(float alpha)
    {
        if (_renderer == null || _renderer.material == null)
            return;

        _renderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        _renderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

        _renderer.material.EnableKeyword("_ALPHABLEND_ON");

        _renderer.material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

        Color currentColor = _renderer.material.color;
        currentColor.a = alpha;
        _renderer.material.color = currentColor;
    }
}