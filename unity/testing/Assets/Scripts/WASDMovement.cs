using UnityEngine;
using System.Collections;

public class WASDMovement : MonoBehaviour {

    public float speed;
    private Vector3 pos, dir;
    private CharacterController control;

	void Update ()
    {
        Debug.Log(transform.position);
        pos = new Vector3(0, 0, 0);
        control = GetComponent<CharacterController>();
        dir = Camera.main.transform.forward;
        if (Input.GetKey(KeyCode.W))
        {
            pos.x = dir.x * speed * Time.deltaTime;
            pos.z = dir.z * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos.x = -(dir.x * speed * Time.deltaTime);
            pos.z = -(dir.z * speed * Time.deltaTime);
        }
        if(pos.y != 12)
        {
            Vector3 temp = transform.position;
            temp.y = 12;
            transform.position = temp;
        }
        control.Move(pos);
    }
}
