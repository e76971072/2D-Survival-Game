using UnityEngine;

namespace Enemy
{
    public class SimpleDiedHandler : DiedHandler
    {
        public override void Die()
        {
            base.Die();
            HideEnemy();
            Destroy(gameObject, timeToDestroy);
        }
        
        private void HideEnemy()
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}