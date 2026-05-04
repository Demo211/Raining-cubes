using System.Collections;
using UnityEngine;
using static Utils;

public class Cube : MonoBehaviour
{
    [SerializeField] private float minimalLifespan;
    [SerializeField] private float maximalLifespan;

    private float _lifetime;
    private bool _isLiving = false;

    private Color _defaultColor;
    private Renderer _cubeRenderer;

    private void Awake()
    {
        _cubeRenderer = GetComponent<Renderer>();
        _defaultColor = _cubeRenderer.material.color;
    }
    private void OnDisable()
    {
        _isLiving = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Plane>(out Plane plane))
        {
            if (_isLiving == false)
            {
                StartLifespanCountdown();
            }
        }
    }

private IEnumerator LifespanCounter(float  _lifetime)
    {
        yield return new WaitForSeconds(_lifetime);
        _isLiving = false;
        CubeSpawner.Instance.ReturnToPool(this);
        _cubeRenderer.material.color = _defaultColor;
    }

    public void StartLifespanCountdown()
    {
        _isLiving = true;
        _lifetime = GetRandomInRange(minimalLifespan, maximalLifespan);
        _cubeRenderer.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        StartCoroutine(LifespanCounter(_lifetime));
    }
}
