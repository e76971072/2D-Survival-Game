using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    Units unit;
    Vector2 previousCheckpoint;
    float closestDistance = Mathf.Infinity;
    Vector2 targetDirection;
    GameObject player;

    void Start ()
    {
        unit = GetComponent<Units>();
        player = GameObject.FindWithTag("Player");
        previousCheckpoint = (Vector2)(player.transform.position);
    }
	
	void Update ()
    {
        targetDirection = ((Vector2)player.transform.position - (Vector2)gameObject.transform.position).normalized;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, targetDirection);
        if (hitInfo.transform.tag == "Player")
        {
            MoveTowardPosition((Vector2)player.transform.position);
        }
    }

    void MoveTowardPosition (Vector2 targetPosition)
    {
        gameObject.transform.position = Vector2.MoveTowards((Vector2)gameObject.transform.position, targetPosition, unit.speed * Time.deltaTime);
    }
}
