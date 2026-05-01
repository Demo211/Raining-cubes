using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using static Utils;

public class CubeSpawner: MonoBehaviour
{
    [SerializeField] private Vector3 _spawnZoneRestrictions;
    [SerializeField] private Material _material;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private float _timeBetweenSpawns;
    [SerializeField] private Cube _cubePrefab;

    private ObjectPool<Cube> _pool;

    private void OnEnable()
    {
        _pool = new ObjectPool<Cube>(CreatePoolStash, GetFromPool, ReleaseToPool, DestroyInPool);
        
        StartCoroutine(Spawn());        
    }

    private IEnumerator Spawn()
    {
        while (this.enabled)
        {
            yield return new WaitForSeconds(_timeBetweenSpawns);
            _pool.Get();
        }
    }

    private Cube CreatePoolStash()
    {
        Cube createdCube = Instantiate(_cubePrefab);
        createdCube.DefineParentPool(_pool);

        createdCube.GetComponent<Renderer>().material = _material;
        createdCube.GetComponent<Renderer>().material.color = _defaultColor;

        return createdCube;
    }

    private void GetFromPool(Cube cube)
    {
        cube.transform.position = GetRandomDropPoint();
        cube.gameObject.SetActive(true);
    }

    private void ReleaseToPool(Cube cube)
    {
        cube.gameObject.SetActive(false);
        cube.GetComponent<Renderer>().material.color = _defaultColor;
    }

    private void DestroyInPool(Cube cube)
    {
        cube.gameObject.IsDestroyed();
    }

    private Vector3 GetRandomDropPoint()
    {
        Vector3 offset = new Vector3(GetRandomInRange(-_spawnZoneRestrictions.x, _spawnZoneRestrictions.x),
                                    GetRandomInRange(-_spawnZoneRestrictions.x, _spawnZoneRestrictions.x),
                                    GetRandomInRange(-_spawnZoneRestrictions.x, _spawnZoneRestrictions.x));

        return transform.position + offset;
    }
}

