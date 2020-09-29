using UnityEngine;

namespace Enemy
{
    public class SimpleDiedHandler : DiedHandler
    {
        public override void Die()
        {
            base.Die();
            Destroy(gameObject);
        }
    }
}