using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCtrlBase : MonoBehaviour
{
    public State playerState = State.idle;

    public enum State
    {
        idle,
        run,
        jump,
        jumpEnd,
        crouch,
        staffAttack,
        shoot,
        hurt,
        death,
        len
    }
}
