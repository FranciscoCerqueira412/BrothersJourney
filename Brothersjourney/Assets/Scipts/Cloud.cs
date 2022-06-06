using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {


    public float posMax = 20f;
    public float posMin = 12f;

    public float  moveSpeed = 3f;
	bool moveUp = true;

	// Update is called once per frame
	void Update () {
		if (transform.position.y > posMax)
			moveUp = false;
		if (transform.position.y < posMin)
			moveUp = true;

		if (moveUp)
			transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
		else
			transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);
	}
}
