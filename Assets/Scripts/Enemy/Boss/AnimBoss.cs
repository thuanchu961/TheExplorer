using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimBoss : Singleton<AnimBoss>
{
    public State bossState = State.idle;
    [SerializeField] Animator anim;
    [SerializeField] BossCtrl boss;
    [SerializeField] bool endMove = false, isEndDie = false;
    public bool _endMove => endMove;
    public bool _isEndDie => isEndDie;
    [SerializeField] int iMove = 0;
    public int _iMove => iMove;
    [SerializeField] float timeIMove, lastTimeIMove = 0;
    // Start is called before the first frame update
    void Start()
    {
        timeIMove = 5;
    }

    // Update is called once per frame
    void Update()
    {
        lastTimeIMove = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTimeIMove);

        ChangeAnim();
    }

    public enum State
    {
        idle,
        beamAttack,
        lightningAttack,
        grenadeAttack,
        disabled,
        death,
        walk,
        backWalk,
        angry,
        len
    }

    void ChangeAnim()
    {
        for (int i = 0; i < (int)State.len; i++)
        {
            if ((int)bossState == i)
                anim.SetBool(bossState.ToString(), true);
            else
                anim.SetBool(((State)i).ToString(), false);
        }
    }

    void EndMove()
    {
        iMove++;
        endMove = true;
        if (iMove == 2)
            StartCoroutine(ResetIMove());
    }

    IEnumerator ResetIMove()
    {
        lastTimeIMove = timeIMove;
        yield return new WaitUntil(() => lastTimeIMove <= 0);
        iMove = 0;
        endMove = false;
    }

    void EndAnimBossDie()
    {
        isEndDie = true;
    }

    void PlayAudioWalk()
    {
        boss.TMT_RunAudioWalk();
    }
}
