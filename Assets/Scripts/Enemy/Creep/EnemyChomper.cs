using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChomper : EnemyCtrlBase
{
    [SerializeField] bool runcode1 = true, runDeath = false;
    [SerializeField] bool isMove2 = true;
    [SerializeField] float timeMove, lastTimeMove = 0;
    float rootHp, getDmgStaff = 0.34f, getDmgShoot = 0.5f, rootSpeed, upSpeed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 100;
        timeMove = 10;
        way = 1;
        getForce = 350;
        rootHp = hpEnemy.value;
        rootSpeed = speed;
        upSpeed = speed + speed * 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        lastTimeMove = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTimeMove);

        ChangeAnim();
        DetectTarget();

        if (hpEnemy.value == 0 && runDeath)
        {
            runDeath = false;
            isMove = false;
            rb2.velocity = Vector2.zero;
            hpEnemy.gameObject.SetActive(false);
            Death();
        }

        if (isDeath)
            return;

        if (isGetDame)
            return;

        if (runcode1)
            DetectLimit();

        if (!isMove)
            return;

        if (isAttack)
            isMove2 = false;
        else
            isMove2 = true;

        if (!isMove2)
            return;

        if (target != null)
        {
            CheckDirWithPlayer();
            speed = upSpeed;
        }
        else
            speed = rootSpeed;

        Moving();
    }

    void DetectLimit()
    {
        if (isLimit)
        {
            runcode1 = false;
            isMove = false;
            rb2.velocity = Vector2.zero;
            StartCoroutine(DelayMoveWithLimit());
            rb2.AddForce(new Vector2(-(upSpeed * way), 0));
        }
        else
            isMove = true;
    }

    void CheckDirWithPlayer()
    {
        if (transform.position.x < playerCtrl.transform.position.x)
        {
            way = Mathf.Abs(way);
            transform.localScale = CommonFunc._inst_singleton.TMT_ChangeLocalScale(way, transform.localScale);
        }
        else if (transform.position.x > playerCtrl.transform.position.x)
        {
            way = -Mathf.Abs(way);
            transform.localScale = CommonFunc._inst_singleton.TMT_ChangeLocalScale(way, transform.localScale);
        }
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
        if (other.collider.tag == "StaffAttack")
        {
            hpEnemy.value -= rootHp * getDmgStaff;
            if (hpEnemy.value <= rootHp * getDmgStaff)
            {
                getForce = 450;
                runDeath = true;
            }

            StartCoroutine(MoveBack(3));
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (other.tag == "PlayerBullet")
        {
            hpEnemy.value -= rootHp * getDmgShoot;
            if (hpEnemy.value <= rootHp * getDmgShoot)
            {
                getForce = 450;
                runDeath = true;
            }

            StartCoroutine(MoveBack(3));

            return;
        }

        isLimit = true;
    }

    IEnumerator MoveBack(float time)
    {
        lastTimeMove = time;
        yield return new WaitUntil(() => lastTimeMove <= 0);
        if (hpEnemy.value != 0)
            isMove = true;
    }

    IEnumerator DelayMoveWithLimit()
    {
        lastTimeMove = timeMove;
        yield return new WaitUntil(() => lastTimeMove <= 0);
        way = -way;
        transform.localScale = CommonFunc._inst_singleton.TMT_ChangeLocalScale(way, transform.localScale);
        runcode1 = true;
        isLimit = false;
        isMove = false;

    }
}
