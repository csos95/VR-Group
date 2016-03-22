using UnityEngine;
using System.Collections;

public class WASDMovement : MonoBehaviour {

    public float speed = 1.5f;
    public float spacing = 1.0f;
    private Vector3 pos;

	// Use this for initialization
	void Start () {
        pos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        pos = transform.position;
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("forward");
            pos.z += spacing;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("back");
            pos.z -= spacing;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("left");
            pos.x -= spacing;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("righ");
            pos.x += spacing;
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
    }
}
