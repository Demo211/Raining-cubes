using System.Collections;
using UnityEngine;
using static Utils;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _minimalLifespan;
    [SerializeField] private float _maximalLifespan;

    private float _lifetime;
    private bool _isLiving = false;

    [SerializeField] private CubeCollorChanger _colorChanger;

    private void OnCollisionEnter(Collision collision)
    {
        if (_isLiving == false)
        {
            if (collision.gameObject.TryGetComponent<Plane>(out Plane plane))
            {
                StartLifespanCountdown();
            }
        }
    }

    private void OnDisable()
    {
        _isLiving = false;
        _colorChanger.SetDefaultColor();
    }    

    private IEnumerator LifespanCounter(float  _lifetime)
    {
        yield return new WaitForSeconds(_lifetime);
        gameObject.SetActive(false);
    }

    private void StartLifespanCountdown()
    {
        _isLiving = true;
        _lifetime = GetRandomInRange(_minimalLifespan, _maximalLifespan);
        _colorChanger.SetRandomColor();

        StartCoroutine(LifespanCounter(_lifetime));
    }
}