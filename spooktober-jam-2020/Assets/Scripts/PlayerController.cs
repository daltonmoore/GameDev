using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    Animator anim;

    #region AnimationParameters

    const string APB_IsMovingHorizontally = "IsMovingHorizontally";
    const string APF_yInput = "yInput";

    #endregion

    [SerializeField]
    float speed = 100;

    bool FlipFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.E))
        {
            rigidbody2D.velocity = new Vector2(0,0);
            DialogueHelper.NPCDialogueInitiated();
        }
    }

    void Movement()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        anim.SetBool(APB_IsMovingHorizontally, xInput != 0);
        anim.SetFloat(APF_yInput, yInput);

        rigidbody2D.velocity = new Vector2(xInput * Time.deltaTime * speed, yInput * Time.deltaTime * speed);



        if (xInput > 0)
        { 
            Vector3 theScale = transform.localScale;
            theScale.x = -6;
            transform.localScale = theScale;
        }
        else if (xInput < 0)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = 6;
            transform.localScale = theScale;
        }
    }
}
