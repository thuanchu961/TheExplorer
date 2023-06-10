using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlayer1 : AnimCtrlBase
{
    [SerializeField] Animator anim;
    float rootSpeed, rootGravityScale;
    [SerializeField] PlayerCtrl playerCtrl;
    [SerializeField] Rigidbody2D rbPlayer;
    float horizontal;
    // Start is called before the first frame update
    void Start()
    {
        rootSpeed = anim.speed;
        rootGravityScale = rbPlayer.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        ChangeAnim();
    }

    void ChangeAnim()
    {
        for (int i = 0; i < (int)AnimCtrlBase.State.len; i++)
        {
            if ((int)playerState == i)
                anim.SetBool(playerState.ToString(), true);
            else
                anim.SetBool(((AnimCtrlBase.State)i).ToString(), false);
        }

        if (playerCtrl._isGround)
            if (Input.GetKey(KeyCode.S))
                anim.SetFloat("shootState", 1);
            else
                anim.SetFloat("shootState", horizontal != 0 ? 0.3f : 0);
        else
            anim.SetFloat("shootState", 0.6f);

    }

    void EndHurt()
    {
        playerCtrl.TMT_SetIsHurt(false);
        playerCtrl.TMT_SetIsJump(false);
    }

    void EndJump()
    {
        playerCtrl.TMT_SetIsMove(true);
    }

    void StartJumpEnd()
    {
        playerCtrl.TMT_SetIsMove(false);
        rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, 0);
        anim.SetBool("hurt", false);
        // rbPlayer.gravityScale = 20;
    }

    void EndJumpEnd()
    {
        rbPlayer.gravityScale = rootGravityScale;
        playerCtrl.TMT_SetIsJump(false);
        playerCtrl.TMT_SetIsMove(true);
    }
}
