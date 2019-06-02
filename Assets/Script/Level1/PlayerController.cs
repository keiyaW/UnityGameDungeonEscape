using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 40f;
    public float jumpForce = 1800f;
    float moving;

    bool isJump = false;
    bool blink = false;
    private bool isGrounded = true;
    private Rigidbody2D rig;
    private bool facingRight = true;
    bool canStep = false;

    public Animator anim;
    public GameObject[] life;
    public GameObject lifeItem;
    public GameObject key;

    public AudioSource steps;
    public AudioSource sfx;
    public AudioClip jumpToStep;
    public AudioClip hit;

    // Use this for initialization
    void Awake () {
        rig = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        //moving = Input.GetAxisRaw("Horizontal") * moveSpeed;
        moving = CrossPlatformInputManager.GetAxis("Horizontal") * moveSpeed;

        if ((Input.GetButtonDown("Jump") || CrossPlatformInputManager.GetButtonDown("Jump")) && isGrounded)
        {
            sfx.PlayOneShot(jumpToStep);
            isGrounded = false;
            isJump = true;
            anim.SetBool("Jump", isJump);
        }
        if ((Mathf.Abs(moving) > 0) && rig.velocity.x != 0 && isGrounded && canStep)
        {
            if (!steps.isPlaying)
                steps.Play();
        }
        else
            steps.Stop();
    }

    private void FixedUpdate()
    {
        Move(moving * Time.fixedDeltaTime, isJump);
        isJump = false;
    }

    public void Move(float moved, bool jump)
    {
        if (isGrounded)
        {
            anim.SetFloat("Speed", Mathf.Abs(moving));
            }     //activate this for activating jump control
            rig.velocity = new Vector2(moved * 10f, rig.velocity.y);
            if(moved>0 && !facingRight)
            {
                Flip();
            }
            else if(moved<0 && facingRight)
            {
                Flip();
            }
        //}   //unactivate this for activating jump controls
        if(jump)
        {
            isGrounded = false;
            Vector2 jForce = new Vector2(0f, jumpForce);
            rig.AddForce(jForce);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        string name = coll.gameObject.name;
        if (name.Contains("Floor") || name.Contains("Object"))
        {
            canStep = true;
            steps.Play();
            isGrounded = true;
            anim.SetBool("Jump", false);
            sfx.PlayOneShot(jumpToStep);

        }
        if (name.Contains("KeyItem"))
        {
            Destroy(coll.gameObject);
            key.SetActive(true);
        }
        if (name.Contains("Obstacle"))
        {
            Physics2D.IgnoreCollision(lifeItem.GetComponent<Collider2D>(), GetComponent<Collider2D>(),false);
            if (!blink)
            {
                sfx.PlayOneShot(hit);
                blink = true;
                if (life[2].activeSelf)
                {
                    life[2].SetActive(false);
                }
                else if (life[1].activeSelf)
                {
                    life[1].SetActive(false);
                }
                else if (life[0].activeSelf)
                {
                    life[0].SetActive(false);
                }
            }
            StartCoroutine(Damaged(coll.collider));
        }
        if (name.Contains("LifeItem"))
        {
            if (life[2].activeSelf)
            {
                Physics2D.IgnoreCollision(coll.collider, GetComponent<Collider2D>());
            }
            else if (life[1].activeSelf)
            {
                coll.gameObject.SetActive(false);
                life[2].SetActive(true);
            }
            else if (life[0].activeSelf)
            {
                coll.gameObject.SetActive(false);
                life[1].SetActive(true);
            }
        }
    }

    public void OnCollisionExit2D(Collision2D coll)
    {
        string name = coll.gameObject.name;
        if (name.Contains("Fly"))
        {
            canStep = false;
            steps.Stop();
        }
        if (name.Contains("Floor") && !name.Contains("Fly"))
        {
            canStep = false;
            steps.Stop();
        }
        else if (name.Contains("BoxObject") && (transform.position.y > coll.gameObject.transform.position.y + 1))
        {
            canStep = false;
            steps.Stop();
        }
    }

    IEnumerator Damaged(Collider2D coll)
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), coll);
        anim.SetLayerWeight(1, 1);

        yield return new WaitForSeconds(3);
        
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), coll, false);
        anim.SetLayerWeight(1, 0);
        blink = false;
    }
}
