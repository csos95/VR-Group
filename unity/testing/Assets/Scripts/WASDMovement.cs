using UnityEngine;
using System.Collections;

public class WASDMovement : MonoBehaviour {

    public float speed;
    private Vector3 pos, dir;
    private CharacterController control;

	void Update ()
    {
        pos = new Vector3(0, 0, 0);
        control = GetComponent<CharacterController>();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = speed * 1.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = speed / 1.5f;
        }
        if(Input.GetAxis("Horizontal") != 0f)
        {
            pos += Input.GetAxis("Horizontal") * transform.right * speed * Time.deltaTime;
            pos.y = 0;
        }
        if(Input.GetAxis("Vertical") != 0f)
        {
            pos += Input.GetAxis("Vertical") * transform.forward * speed * Time.deltaTime;
            pos.y = 0;
        }
        if (transform.position.y != 12)
        {
            Vector3 temp = transform.position;
            temp.y = 12;
            transform.position = temp;
        }
        control.Move(pos);
    }
}
