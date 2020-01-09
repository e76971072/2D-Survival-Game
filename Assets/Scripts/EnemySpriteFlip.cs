using System;
using Pathfinding;
using UnityEngine;

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
        if (Math.Abs(aiPath.velocity.normalized.x) < Mathf.Epsilon) return;
        spriteRenderer.flipX = aiPath.velocity.x < 0;
    }
}