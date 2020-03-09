using Pathfinding;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(AIPath))]
    public class EnemySpriteFlip : MonoBehaviour
    {
        private AIPath aiPath;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            aiPath = GetComponent<AIPath>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (aiPath.velocity.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (aiPath.velocity.x < 0)
            {
                spriteRenderer.flipX = true;
            }
        }
    }
}