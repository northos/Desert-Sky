using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {
	public float moveSpeed;
	public float raycastDist;
	public float targetRange;
	Vector3 target = Vector3.zero;

	// when the player clicks, store that location and pathfind towards it
	// if there is a target, do pathfinding
	void Update () {
		// on mouse click, have the player chanacter pathfind to that location
		if (CrossPlatformInputManager.GetAxis ("Fire1") == 1f) {
			// replace previous target
			target = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			target.Set (target.x, target.y, 0);
		}
		// follow pathfind towards target if it exists
		else if (target != Vector3.zero) {
			if (Vector3.Distance (transform.position, target) < targetRange) {
				target = Vector3.zero;
			} else {
				RaycastHit2D hit;
				Vector3 moveVector = (target - transform.position).normalized * moveSpeed;
				hit = Physics2D.Raycast ((Vector2)transform.position, (Vector2)(target - transform.position).normalized, raycastDist);
				if (hit.collider != null && hit.collider != GetComponent<BoxCollider2D> ()) {
					print ("hit");
					moveVector += (Vector3)hit.normal * moveSpeed;
				}
				transform.Translate (moveVector * Time.deltaTime);
			}
		}
	}

	/* precalculated pathfinding
	// find a path for the player character to the target location, avoiding obstacles and terrain
	void pathfindTo (Vector3 target) {
		// add target point to end of list
		waypoints.Add (target);
		RaycastHit2D hit;
		Vector2 currentPoint = (Vector2)transform.position;
		// use 2D raycasts to look for obstacles along path
		while (hit = Physics2D.Raycast (currentPoint, ((Vector2)target - currentPoint).normalized)) {
			if (hit.collider == null)
				break;
			// if hit, do obstacle avoidance
		}
	}*/
}
