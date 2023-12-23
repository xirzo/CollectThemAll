using UnityEngine;
using Collect.Domain.Pooling;
using System.Collections.Generic;

namespace Collect.Domain.Spawning
{
    public class CameraUpperBoundSpawner : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _objectToSpawnPrefab;
        [SerializeField] private Transform _enemiesParent;
        [Space]
        [SerializeField] private Camera _camera;
        [Space]
        [SerializeField, Min(0)] private float _firstSpawnDelay = 0.1f;
        [SerializeField, Min(0)] private float _spawnRepeatTimeInSeconds = 0.5f;
        private ObjectPool<MonoBehaviour> _pool;
        private List<MonoBehaviour> _objects;
        private Vector3 _randomPointInCameraBounds;
        private float _leftXCameraBound;
        private float _rightXCameraBound;
        private float _lowerYCameraBound;
        private float _rightXScaledCameraBound;
        private float _leftXScaledCameraBound;
        private float _lowerYScaledCameraBound;

        private void GetCameraBounds()
        {
            _leftXCameraBound = _camera.ViewportToWorldPoint(new Vector3(0, 0)).x;
            _rightXCameraBound = _camera.ViewportToWorldPoint(new Vector3(1, 0)).x;
            _lowerYCameraBound = _camera.ViewportToWorldPoint(new Vector3(0, 0)).y;

        }
        private void GetCameraScaledBounds()
        {
            float halfedScaleX = _objectToSpawnPrefab.transform.localScale.x / 2;
            float halfedScaleY = _objectToSpawnPrefab.transform.localScale.y / 2;
            _leftXScaledCameraBound = _leftXCameraBound + halfedScaleX;
            _rightXScaledCameraBound = _rightXCameraBound - halfedScaleX;
            _lowerYScaledCameraBound = _lowerYCameraBound - halfedScaleY;
        }

        private void Awake()
        {
            _objects = new List<MonoBehaviour>();
            _pool = new ObjectPool<MonoBehaviour>(_objectToSpawnPrefab, _enemiesParent);

            GetCameraBounds();
            GetCameraScaledBounds();
        }

        private void GetRandomPointInCameraBounds()
        {
            float randomX = Random.Range(_leftXScaledCameraBound, _rightXScaledCameraBound);
            _randomPointInCameraBounds = new Vector3(randomX, transform.position.y);
        }

        private void SpawnAtRandomPointInCameraBounds()
        {
            GetRandomPointInCameraBounds();
            var newEnemy = _pool.Unpool();
            _objects.Add(newEnemy);
            newEnemy.transform.position = _randomPointInCameraBounds;
        }

        private void Start()
        {
            InvokeRepeating("SpawnAtRandomPointInCameraBounds", _firstSpawnDelay, _spawnRepeatTimeInSeconds);
        }

        private void PoolOutOfCameraEnemies()
        {
            foreach (var enemy in _objects)
            {
                if (enemy.transform.position.y < _lowerYScaledCameraBound)
                {
                    _pool.Pool(enemy);
                }
            }
        }

        private void Update()
        {
            PoolOutOfCameraEnemies();
        }
    }
}
