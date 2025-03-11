using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorHandler : MonoBehaviour
{
    private Renderer _renderer;

    private string _srcBlend = "_SrcBlend";
    private string _dstBlend = "_DstBlend";
    private string _aphaBlendKeyword = "_ALPHABLEND_ON";

    private int _srcBlendValue = (int)UnityEngine.Rendering.BlendMode.SrcAlpha;
    private int _dstBlendValue = (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha;
    private int _renderQueueValue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

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

        _renderer.material.SetInt(_srcBlend, _srcBlendValue);
        _renderer.material.SetInt(_dstBlend, _dstBlendValue);

        _renderer.material.EnableKeyword(_aphaBlendKeyword);

        _renderer.material.renderQueue = _renderQueueValue;

        Color currentColor = _renderer.material.color;
        currentColor.a = alpha;
        _renderer.material.color = currentColor;
    }
}