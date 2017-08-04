using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float enemySpeed;
    public GameObject[] movingCheckpoints;

    Vector2 previousCheckpoint;
    float closestDistance = Mathf.Infinity;
    List<Vector3> targetCheckpoints;
    Vector2 targetDirection;
    GameObject player;

	// Use this for initialization
	void Start ()
    {
        previousCheckpoint = (Vector2)(player.transform.position);
        player = GameObject.FindGameObjectWithTag("Player");
        movingCheckpoints = GameObject.FindGameObjectsWithTag("MovingCheckpoint");
	}
	
	// Update is called once per frame
	void Update ()
    {
        targetDirection = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, targetDirection);
        if (hitInfo.transform.tag == "Player")
        {
            MoveTowardPosition((Vector2)player.transform.position);
        }
        else
        {
            foreach (GameObject mC in movingCheckpoints)
            {
                if ((Vector2)mC.transform.position != previousCheckpoint)
                {
                    if(Vector2.Distance((Vector2)mC.transform.position, previousCheckpoint) < closestDistance)
                    {
                        closestDistance = Vector2.Distance((Vector2)mC.transform.position, previousCheckpoint);
                    }
                }
            }
        }
    }

    void MoveTowardPosition (Vector2 targetPosition)
    {
        gameObject.transform.position = Vector2.MoveTowards((Vector2)gameObject.transform.position, targetPosition, enemySpeed * Time.deltaTime);
    }
}
