using UnityEngine;
using System.Collections;
/*todo: 
 bool is_jump
 ontriggerenter2d teleport + catch
 throw
 teleport
 move    
     */
public class PlayerControlSecond : MonoBehaviour
{
    //init variables
    public Animator anim;
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
    public Vector2 Speed = new Vector3(0f, 0f, 0f);
    public Vector2 jump = new Vector2(0f, 2f);
    public int Timefreeze = 1;
    public GameObject Blood;
    public AudioSource audioplayer;
    public AudioClip audiojump;
    public AudioClip audioattack;
    public AudioClip audiowalk;
    public AudioClip audioblock;


    private int countfreeze = 0;
    private bool is_jump = false;
    // Use this for initialization
    void Start()
    {

    }
    void Move(Vector2 speed)//move через rigidbody
    {
        if (!audioplayer.isPlaying && is_jump == false)
        {
            audioplayer.PlayOneShot(audiowalk);
        }
        anim.SetBool("IsWalk", true);
        Rb2d.velocity = new Vector2(speed.x, Rb2d.velocity.y);
        if (Rb2d.velocity.x < 0)
        {
            direction = -1;
            target.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (Rb2d.velocity.x > 0)
        {
            direction = 1;
            target.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            OpponentIsCatcheable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
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
        anim.SetBool("IsBlock", false);
        anim.SetBool("IsAttack", false);
        anim.SetBool("IsJump", false);
        anim.SetBool("IsWalk", false);
        anim.SetBool("IsFreed", false);
        if (YouCatched == false)// не пойманы
        {
            anim.SetBool("IsFall", false);
            Blood.SetActive(false);
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                audioplayer.PlayOneShot(audiojump);
                anim.SetBool("IsJump", true);
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
            if (Input.GetKey(KeyCode.RightArrow)) //when Right Key pressed (D)
            {
                Move(Speed);
            }

            if (Input.GetKey(KeyCode.LeftArrow)) //when Right Key pressed (D)
            {
                Move(-Speed);
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) //when Left Key pressed (A)
            {
                Rb2d.velocity = new Vector2(0.0f, Rb2d.velocity.y);
            }
            if (Input.GetKey(KeyCode.RightShift))
            {
                if (!audioplayer.isPlaying)
                {
                    audioplayer.PlayOneShot(audioblock);
                }
                anim.SetBool("IsBlock", true);
                GameObject enemy = GameObject.FindGameObjectWithTag("Player");
                enemy.GetComponent<PlayerControl>().OpponentIsCatcheable = false;
                YouCatched = false;
            }
            if (OpponentCatched && Input.GetKeyDown(KeyCode.RightControl))
            {
                audioplayer.PlayOneShot(audioattack);
                anim.SetBool("IsAttack", true);
                TiltCount++;
                if (TiltCount == 1)
                {
                    OpponentCatched = false;
                    GameObject enemy = GameObject.FindGameObjectWithTag("Player");
                    enemy.GetComponent<PlayerControl>().YouThrowed = true;
                    enemy.GetComponent<PlayerControl>().InvokeRepeating("CountFreezeCatched", 0f, 1f);

                    if (direction == -1)
                    {
                        enemy.transform.position = new Vector3(target.transform.position.x - 1f,
                                                                target.transform.position.y + 2f,
                                                                target.transform.position.z);
                    }
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(600f * direction, 200f));
                    TiltCount = 0;
                }
            }
            if (Input.GetKey(KeyCode.RightControl) && OpponentIsCatcheable) //when Right Key pressed (D)
            {
                anim.SetBool("IsAttack", true);
                audioplayer.PlayOneShot(audioattack);
                GameObject enemy = GameObject.FindGameObjectWithTag("Player");
                enemy.GetComponent<PlayerControl>().YouCatched = true;
                OpponentCatched = true;
            }
        }
        else// мы пойманы
        {
            Blood.SetActive(true);
            target.transform.rotation = Quaternion.Euler(0, 0, 90);
            GameObject enemy = GameObject.FindGameObjectWithTag("Player");
            if (YouThrowed == false)
            {
                anim.SetBool("IsHolded", true);
                target.transform.position = enemy.transform.position;
                target.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y + DisatanceUpHead, enemy.transform.position.z);
            }
            if (YouThrowed == true)
            {
                anim.SetBool("IsHolded", false);
                anim.SetBool("IsFall", true);
            }
            Rb2d.velocity.Set(0, 0);
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                EscapeCount++;
                if (EscapeCount == 1)
                {
                    anim.SetBool("IsHolded", false);
                    anim.SetBool("IsFall", false);
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