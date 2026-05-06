using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CubeCollorChanger : MonoBehaviour
{
    private Color _defaultColor;
    private Renderer _cubeRenderer;

    private void Awake()
    {
        _cubeRenderer = GetComponent<Renderer>();
        _defaultColor = _cubeRenderer.material.color;
    }

    public void SetDefaultColor()
    {
        _cubeRenderer.material.color = _defaultColor;
    }

    public void SetRandomColor()
    {
        _cubeRenderer.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }    
}

