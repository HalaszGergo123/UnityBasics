using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    public LayerMask playerMask;

    private bool touchWasPressed; //kis betuvel kezd a variablet
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    private int superJumpsRemaining = 0;


    // Start is called before the first frame update
    void Start() //nagy betuvel a metodokat
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update()
    {
        // Check if touch key is pressed down
        if (Input.touchCount == 1)
            touchWasPressed = true;

        horizontalInput = Input.GetAxis("Horizontal");
    }
    // FixedUpdate is called once every physics update
    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y, 0);

        if (Physics.OverlapSphere(groundCheckTransform.position,0.1f,playerMask).Length==0)
        {
            return;
        }    

        if (touchWasPressed)
        {
            float jumpPower = 5f;
            if(superJumpsRemaining > 0)
            {
                jumpPower *= 2;
                superJumpsRemaining--;
            }
            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            touchWasPressed = false;
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
    }


}

