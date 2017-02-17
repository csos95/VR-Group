//commented stuff is for mobile controls
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed;
    private Vector3 pos;
    private CharacterController control;
    private float horizontal, vertical;

    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationX = 0F;
    float rotationY = 0F;
    Quaternion originalX;
    Quaternion originalY;

    //private Vector2 touchOrigin = -Vector2.one;

    void Start()
    {
        control = GetComponent<CharacterController>();

        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
        originalX = transform.localRotation;
        originalY = Camera.main.transform.localRotation;
    }

    void Update()
    {

        //if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        //{
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayCastHit;

            if (Physics.Raycast(ray.origin, ray.direction, out rayCastHit, Mathf.Infinity))
            {
                Door door = rayCastHit.transform.GetComponent<Door>();
                if (door != null)
                {
                    door.PlayAnimation();
                }
            }
        }

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            ChangeSpeed();
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            ToggleCursor();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationX = ClampAngle(rotationX, minimumX, maximumX);
        rotationY = ClampAngle(rotationY, minimumY, maximumY);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
        transform.localRotation = originalX * xQuaternion;// * yQuaternion;
        Camera.main.transform.localRotation = originalY * yQuaternion;

        //}
        /*else
        {
            if(Input.touchCount > 0 && Input.touches[0].position.x > Screen.width/2)
            {
                Touch myTouch = Input.touches[0];
                if(myTouch.phase == TouchPhase.Began)
                {
                    touchOrigin = myTouch.position;
                }
                else if(myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
                {
                    Vector2 touchEnd = myTouch.position;
                    float x = touchEnd.x - touchOrigin.x;
                    float y = touchEnd.y - touchOrigin.y;
                    touchOrigin.x = -1;
                    if(Mathf.Abs(x) > Mathf.Abs(y))
                    {
                        horizontal = x > 0 ? 1 : -1;
                    }
                    else
                    {
                        vertical = y > 0 ? 1 : -1;
                    }
                }
            }
        }*/

        if (horizontal != 0f || vertical != 0f)
        {
            Move();
        }
    }

    private void Move()
    {
        pos = new Vector3(0, 0, 0);
        pos += horizontal * transform.right * speed * Time.deltaTime;
        pos += vertical * transform.forward * speed * Time.deltaTime;
        pos.y = 0;
        if (transform.position.y != 0.9f && !control.isGrounded)
        {
            Vector3 temp = transform.position;
            temp.y = 0.9f;
            transform.position = temp;
        }
        control.Move(pos);
    }

    private void ChangeSpeed()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = speed * 2.0f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = speed / 2.0f;
        }
    }

    private void ToggleCursor()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

}
