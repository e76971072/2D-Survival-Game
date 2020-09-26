using Pathfinding;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(AIPath))]
    public class EnemySpriteFlip : MonoBehaviour
    {
        private AIPath _aiPath;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _aiPath = GetComponent<AIPath>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (_aiPath.velocity.x > 0)
            {
                _spriteRenderer.flipX = false;
            }
            else if (_aiPath.velocity.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
        }
    }
}