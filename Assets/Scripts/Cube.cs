using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using static Utils;

public class Cube : MonoBehaviour
{
    [SerializeField] private float minimalLifespan;
    [SerializeField] private float maximalLifespan;
    private ObjectPool<Cube> _pool;
    private float _lifetime;

    private bool _isLiving = false;
    public bool IsLiving => _isLiving;

    private void OnDisable()
    {
        _isLiving = false;
    }

    private IEnumerator LifespanCounter(float  _lifetime)
    {
        yield return new WaitForSeconds(_lifetime);
        _pool.Release(this);
    }

    public void DefineParentPool(ObjectPool<Cube> pool)
    {
        _pool = pool;
    }

    public void StartLifespanCountdown()
    {
        _isLiving = true;
        _lifetime = GetRandomInRange(minimalLifespan, maximalLifespan);
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        StartCoroutine(LifespanCounter(_lifetime));
    }
}
