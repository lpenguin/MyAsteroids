using System.Collections;
using UnityEngine;

namespace Game.Utils
{
    public class DestroyAfter: MonoBehaviour
    {
        [SerializeField]
        [Min(0)]
        private float seconds;

        private void Start()
        {
            StartCoroutine(DestroySelf());
        }

        private IEnumerator DestroySelf()
        {
            yield return new WaitForSeconds(seconds);
            Destroy(gameObject);
        }
    }
}