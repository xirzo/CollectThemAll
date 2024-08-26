using System.Collections.Generic;
using Collect.Domain.Entities;
using Collect.Domain.Pooling;
using Collect.Domain.Score;
using UnityEngine;

namespace Collect.Domain.Spawning
{
	public class CameraUpperBoundSpawner : MonoBehaviour
	{
		[SerializeField] private Entity _objectToSpawnPrefab;
		[SerializeField] private Transform _enemiesParent;
		[SerializeField] private CoinReceiver _coinReceiver;

		[Space] [SerializeField] private Camera _camera;

		[Space] [SerializeField] [Min(0)] private float _firstSpawnDelay = 0.1f;

		[SerializeField] [Min(0)] private float _spawnRepeatTimeInSeconds = 0.5f;
		[SerializeField] [Min(0)] private float _ySpawnOffset = 0.5f;

		private float _leftXCameraBound;
		private float _leftXScaledCameraBound;
		private float _lowerYCameraBound;
		private float _lowerYScaledCameraBound;

		private List<Entity> _objects;
		private ObjectPool<Entity> _pool;

		private Vector3 _randomPointInCameraBounds;
		private float _rightXCameraBound;
		private float _rightXScaledCameraBound;
		private float _upperYCameraBound;

		private void Awake()
		{
			_objects = new List<Entity>();
			_pool = new ObjectPool<Entity>(_objectToSpawnPrefab, _enemiesParent);
			_coinReceiver.Construct(_pool);

			GetCameraBounds();
			GetCameraScaledBounds();
		}

		private void Start()
		{
			InvokeRepeating("SpawnAtRandomPointInCameraBounds", _firstSpawnDelay, _spawnRepeatTimeInSeconds);
		}

		private void Update()
		{
			PoolOutOfCameraEnemies();
		}

		private void GetCameraBounds()
		{
			_leftXCameraBound = _camera.ViewportToWorldPoint(new Vector3(0, 0)).x;
			_rightXCameraBound = _camera.ViewportToWorldPoint(new Vector3(1, 0)).x;
			_upperYCameraBound = _camera.ViewportToWorldPoint(new Vector3(0, 1)).y;
			_lowerYCameraBound = _camera.ViewportToWorldPoint(new Vector3(0, 0)).y;
		}

		private void GetCameraScaledBounds()
		{
			var halfedScaleX = _objectToSpawnPrefab.transform.localScale.x / 2;
			var halfedScaleY = _objectToSpawnPrefab.transform.localScale.y / 2;
			_leftXScaledCameraBound = _leftXCameraBound + halfedScaleX;
			_rightXScaledCameraBound = _rightXCameraBound - halfedScaleX;
			_lowerYScaledCameraBound = _lowerYCameraBound - halfedScaleY;
		}

		private void GetRandomPointInCameraBounds()
		{
			var randomX = Random.Range(_leftXScaledCameraBound, _rightXScaledCameraBound);
			_randomPointInCameraBounds = new Vector3(randomX, _upperYCameraBound + _ySpawnOffset);
		}

		private void SpawnAtRandomPointInCameraBounds()
		{
			GetRandomPointInCameraBounds();
			var newEnemy = _pool.Unpool();
			_objects.Add(newEnemy);
			newEnemy.transform.position = _randomPointInCameraBounds;
		}

		private void PoolOutOfCameraEnemies()
		{
			foreach (var enemy in _objects)
				if (enemy.transform.position.y < _lowerYScaledCameraBound)
					_pool.Pool(enemy);
		}
	}
}