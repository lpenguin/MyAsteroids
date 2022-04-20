using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Random = UnityEngine.Random;

namespace Game.Spawners
{
    public class OffScreenSpawner: MonoBehaviour
    {
        [SerializeField] 
        private float interval = 2f;
        
        [SerializeField] 
        [AssetReferenceUILabelRestriction("Game Objects")]
        private AssetReference[] prefabs;

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

                InstantiatePrefabOffscreen(prefabs[Random.Range(0, prefabs.Length)]);
                _instances += 1;
            }
        }

        private void InstantiatePrefabOffscreen(AssetReference prefab)
        {
            var topLeft = _camera.ViewportToWorldPoint(new Vector2(0, 0));
            var bottomRight = _camera.ViewportToWorldPoint(new Vector2(1, 1));
            Vector2 size = Vector2.zero;
            
            var op = prefab.InstantiateAsync(topLeft * 1.2f, Quaternion.identity, transform);
            op.Completed += handle =>
            {
                var gameObject = handle.Result;
                
                if (gameObject.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
                {
                    size = spriteRenderer.bounds.size;
                }

                Vector2 position;
                if (Random.Range(0f, 1f) < 0.5f)
                {
                    // Vertical segments
                    float x = Random.Range(0f, 1f) < 0.5f ? topLeft.x - size.x * 0.45f : bottomRight.x + size.x * 0.45f;
                    float y = Random.Range(topLeft.y, bottomRight.y);
                    position = new Vector2(x, y);
                }
                else
                {
                    // Horizontal segments
                    float x = Random.Range(topLeft.x, bottomRight.x);
                    float y = Random.Range(0f, 1f) < 0.5f ? topLeft.y - size.y * 0.45f : bottomRight.y + size.y * 0.45f;
                    position = new Vector2(x, y);
                }

                gameObject.transform.position = position;
            };

        }
    }
}