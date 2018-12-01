using UnityEngine;
using System.Collections;
/*todo: 
 bool is_jump
 ontriggerenter2d teleport + catch
 throw
 teleport
 move    
     */
public class PlayerControl : MonoBehaviour
{
    //init variables
    public GameObject target; //also an error here.
    public Rigidbody2D Rb2d;
    public BoxCollider2D Bc2d;
    private float acceleration;
    private float lastVelocity;
    public Vector2 Speed = new Vector3(0f,0f,0f);
    private bool is_jump = false;
    public Vector2 jump = new Vector2(0f, 2f);
    // Use this for initialization
    void Start()
    {

    }
    void Move(Vector2 speed)//move через rigidbody
    {
        Rb2d.velocity = new Vector2(speed.x, Rb2d.velocity.y); 
    }
    bool IsGrounded()
    {
        Vector2 position = transform.position;
        position.y -= 1.5f;
        Vector2 direction = Vector2.down;
        float distance = 0.01f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
    // Update is called once per frame
    void Update()
    {
        acceleration = (Rb2d.velocity.x - lastVelocity) / Time.fixedDeltaTime;
        lastVelocity = Rb2d.velocity.x;
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (is_jump == true)
            {
                is_jump = false;
                Rb2d.AddForce(jump);
            }
            else if (IsGrounded())
            {
                Rb2d.AddForce(jump);
                is_jump = true;
            }
        }
        if (Input.GetKey(KeyCode.D)) //when Right Key pressed (D)
        {
            Move(Speed); 
        }
        if (Input.GetKey(KeyCode.A)) //when Right Key pressed (D)
        {
            Move(-Speed);
        }
        if (Input.GetKeyUp(KeyCode.A)|| Input.GetKeyUp(KeyCode.D)) //when Left Key pressed (A)
        {
            Rb2d.velocity = new Vector2(0.0f,Rb2d.velocity.y);
        }
       
    }
}