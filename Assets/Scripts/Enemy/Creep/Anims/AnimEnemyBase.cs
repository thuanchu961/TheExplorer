using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEnemyBase : MonoBehaviour
{
    public State enemyState = State.idle;
    public enum State
    {
        idle,
        run,
        walk,
        attack,
        death,
        len
    }
}
