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
    public Rigidbody2D Rb2d; //движение игрока
    public bool OpponentIsCatcheable = false; // противник в радиусе захвата
    public bool OpponentCatched = false; // противник пойман
    public bool YouCatched = false; // пойман ты
    public bool YouThrowed = false;
    public float DisatanceUpHead = 2f;
    public int TiltCount = 0; //кол-во трясок
    public int EscapeCount = 0;// кол-во освобождений
    public int direction = 1;
    public Vector2 Speed = new Vector3(0f,0f,0f);
    public Vector2 jump = new Vector2(0f, 2f);
    public int Timefreeze = 1;
    public GameObject Blood;

    private int countfreeze = 0;
    private bool is_jump = false;
    // Use this for initialization
    void Start()
    {

    }
    void Move(Vector2 speed)//move через rigidbody
    {
        Rb2d.velocity = new Vector2(speed.x, Rb2d.velocity.y);
        if (Rb2d.velocity.x < 0)
        {
            direction = -1;
            target.transform.rotation = Quaternion.Euler(0, 180, 0);
        }//поворот
        if (Rb2d.velocity.x > 0)
        {
            direction = 1;
            target.transform.rotation = Quaternion.Euler(0, 0, 0);
        }//поворот
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player1")
        {
            OpponentIsCatcheable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player1")
        {
            OpponentIsCatcheable = false;
        }
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
        if (YouCatched == false)// не пойманы
        {
            Blood.SetActive(false);
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
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) //when Left Key pressed (A)
            {
                Rb2d.velocity = new Vector2(0.0f, Rb2d.velocity.y);
            }
            if (Input.GetKey(KeyCode.G))
            {
                GameObject enemy = GameObject.FindGameObjectWithTag("Player1");
                enemy.GetComponent<PlayerControlSecond>().OpponentIsCatcheable = false;
                YouCatched = false;
            }
            if (OpponentCatched && Input.GetKeyDown(KeyCode.F))
            {
                TiltCount++;
                if (TiltCount == 1)
                {
                    OpponentCatched = false;
                    GameObject enemy = GameObject.FindGameObjectWithTag("Player1");
                    enemy.GetComponent<PlayerControlSecond>().YouThrowed = true;
                    enemy.GetComponent<PlayerControlSecond>().InvokeRepeating("CountFreezeCatched", 0f, 1f);

                    if (direction == -1)
                    {
                        enemy.transform.position = new Vector3(target.transform.position.x - 1f,
                                                                target.transform.position.y + 2f,
                                                                target.transform.position.z);
                    }
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(600f * direction, 1000f));
                    TiltCount = 0;
                }
            }
            if (Input.GetKey(KeyCode.F) && OpponentIsCatcheable) //when Right Key pressed (D)
            {
                GameObject enemy = GameObject.FindGameObjectWithTag("Player1");
                enemy.GetComponent<PlayerControlSecond>().YouCatched = true;
                OpponentCatched = true;
            }

        }
        else// мы пойманы
        {
            Blood.SetActive(true);
            target.transform.rotation = Quaternion.Euler(0, 0, 90);
            GameObject enemy = GameObject.FindGameObjectWithTag("Player1");
            if (YouThrowed == false)
            { 
                target.transform.position = enemy.transform.position;
                target.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y + DisatanceUpHead, enemy.transform.position.z);
            }
            Rb2d.velocity.Set(0, 0);
            if (Input.GetKeyDown(KeyCode.F))
            {
                EscapeCount++;
                if (EscapeCount == 1)
                {
                    target.transform.rotation = Quaternion.Euler(0, 0, 0);
                    YouCatched = false;
                    EscapeCount = 0;
                }
            }
        }
    }

    void CountFreezeCatched()
    {
        countfreeze++;
        if (countfreeze == Timefreeze)
        {
            YouCatched = false;
            YouThrowed = false;
            target.transform.rotation = Quaternion.Euler(0, 0, 0);
            countfreeze = 0;
            CancelInvoke();
        }
    }
}