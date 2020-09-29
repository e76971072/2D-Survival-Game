using Attacks;
using Pathfinding;
using UnityEngine;

namespace Enemy
{
    public abstract class DiedHandler : MonoBehaviour
    {
        [SerializeField] protected float timeToDestroy;
        
        public virtual void Die()
        {
            
        }
    }
}