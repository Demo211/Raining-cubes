using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using static Utils;

public class CubeSpawner: MonoBehaviour
{
    [SerializeField] private Vector3 _spawnZoneRestrictions;
    [SerializeField] private float _timeBetweenSpawns;
    [SerializeField] private Cube _cubePrefab;

    private ObjectPool<Cube> _pool;
    private List<Cube> _activatedCubes;

    private void Awake()
    {
        _activatedCubes = new List<Cube>();
        _pool = new ObjectPool<Cube>(CreatePoolStash, GetFromPool, ReleaseToPool, DestroyInPool);
        
        StartCoroutine(Spawn());        
    }

    private void FixedUpdate()
    {
        for (int i =0; i< _activatedCubes.Count;)
        {
            Cube cube = _activatedCubes[i];

            if (cube.gameObject.activeSelf == false)
            {
                _pool.Release(cube);
            }
            else
            {
                i++;
            }
        }
    }

    private IEnumerator Spawn()
    {
        while (this.enabled)
        {
            yield return new WaitForSeconds(_timeBetweenSpawns);
            _pool.Get();
        }
    }

    #region PoolOperations

    private Cube CreatePoolStash()
    {
        return Instantiate(_cubePrefab);
    }

    private void GetFromPool(Cube cube)
    {
        cube.transform.position = GetRandomDropPoint();
        cube.gameObject.SetActive(true);    
        _activatedCubes.Add(cube);
    }

    private void ReleaseToPool(Cube cube)
    {
        _activatedCubes.Remove(cube);
    }

    private void DestroyInPool(Cube cube)
    {
        cube.gameObject.IsDestroyed();
    }

    #endregion

    private Vector3 GetRandomDropPoint()
    {
        Vector3 offset = new Vector3(GetRandomInRange(-_spawnZoneRestrictions.x, _spawnZoneRestrictions.x),
                                    GetRandomInRange(-_spawnZoneRestrictions.x, _spawnZoneRestrictions.x),
                                    GetRandomInRange(-_spawnZoneRestrictions.x, _spawnZoneRestrictions.x));

        return transform.position + offset;
    }
}

