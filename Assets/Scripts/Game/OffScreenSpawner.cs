using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class OffScreenSpawner: MonoBehaviour
    {
        public interface IHasSpawnerParent
        {
            OffScreenSpawner Spawner { get; set; }
        }
        
        [SerializeField] 
        private int maxInstances = 10;
        
        [SerializeField] 
        private float interval = 2f;
        
        [SerializeField] 
        private GameObject[] prefabs;

        private int _instances = 0;
        private Camera _camera;
        private Coroutine _instantiateCoro;

        public void Decrement()
        {
            _instances -= 1;
        }
        
        private void Start()
        {
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            _instantiateCoro ??= StartCoroutine(InstantiateCoro());
        }

        private void OnDisable()
        {
            StopCoroutine(_instantiateCoro);
            _instantiateCoro = null;
        }

        private IEnumerator InstantiateCoro()
        {
            while (true)
            {
                yield return new WaitForSeconds(interval);

                if (_instances <= maxInstances)
                {
                    InstantiatePrefabOffscreen(prefabs[Random.Range(0, prefabs.Length)]);
                    _instances += 1;
                }
            }
        }

        private void InstantiatePrefabOffscreen(GameObject prefab)
        {
            var topLeft = _camera.ViewportToWorldPoint(new Vector2(0, 0));
            var bottomRight = _camera.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 size = Vector2.zero;
            if (prefab.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
            {
                size = spriteRenderer.bounds.size;
            }

            Vector2 position;
            if (Random.Range(0, 1) < 0.5f)
            {
                // Vertical segments
                float x = Random.Range(topLeft.x, bottomRight.x);
                float y = Random.Range(0, 1) < 0.5f ? -size.y / 2 : bottomRight.y + size.y / 2; 
                position = new Vector2(x, y);
            }
            else
            {
                // Horizontal segments
                float x = Random.Range(0, 1) < 0.5f ? -size.x / 2 : bottomRight.x + size.x / 2; 
                float y = Random.Range(topLeft.y, bottomRight.y);
                position = new Vector2(x, y);
            }

            var go = Instantiate(prefab, position, Quaternion.identity, transform);
            if (go.TryGetComponent<IHasSpawnerParent>(out var hasSpawnerParent))
            {
                hasSpawnerParent.Spawner = this;
            }
        }
    }
}