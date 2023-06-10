using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossCtrl : MonoBehaviour
{
    [SerializeField] AnimBoss anim;
    [SerializeField] Rigidbody2D rb2;
    [SerializeField] PlayerCtrl playerCtrl;
    [SerializeField] float walkDir = -1, speed, maxSpeed;
    Vector2 movement = Vector2.zero;
    [SerializeField] float timeChangeState, lastTimeChangeState = 0;
    [SerializeField] float timeShootBeam, lastTimeShootBeam = 0;
    [SerializeField] float timeOffAngry, lastTimeOffAngry = 0;
    [SerializeField] bool endAnimMove, isMove = false, endDie = false;
    public bool _endDie => endDie;
    [SerializeField] bool isActiveGrenadeAttack, isAngry = false;
    [SerializeField]
    bool runCode = true, runCode1 = true, runCode2 = false, runCode3 = true,
     runCode4 = false, runCode5 = true, runCode6 = true;
    [SerializeField] GameObject bossLightning, bossBeamAttack, bossGrenadeAttack, gunnerGrenade, gunnerBeam;
    [SerializeField] GameObject lazeBeam, targetOfPlayer, canvasBossInfo;
    [SerializeField] LineRenderer lineLaze;
    [SerializeField] BossGetDmg bossGetDmg;
    [SerializeField] bool isDeath;
    [SerializeField] AudioSource audioBeam, audioLightning, audioWalk, audioBackWalk, audioBossDie;
    [SerializeField] Animator animUIShield, animUIHealth;
    // [SerializeField] Text test;
    // Start is called before the first frame update
    void Start()
    {
        playerCtrl = PlayerCtrl._inst_singleton;
        speed = 100;
        maxSpeed = 2.3f;
        timeChangeState = 10;
        lastTimeChangeState = 20;
        timeShootBeam = 7;
        timeOffAngry = 10;
    }

    // Update is called once per frame
    void Update()
    {
        endDie = AnimBoss._inst_singleton._isEndDie;

        if (endDie)
        {
            DeactiveBoss();
            return;
        }

        if (runCode4 && runCode5)
        {
            runCode5 = false;
            bossDie();
        }

        isDeath = bossGetDmg._isDeath;
        if (isDeath)
        {
            runCode4 = true;
            return;
        }

        lastTimeChangeState = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTimeChangeState);
        lastTimeShootBeam = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTimeShootBeam);
        lastTimeOffAngry = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTimeOffAngry);

        if (isAngry)
        {
            anim.bossState = AnimBoss.State.angry;
            if (runCode3)
            {
                runCode3 = false;
                StartCoroutine(OffIsAngry());
            }
            return;
        }

        if (!bossGetDmg._isShield)
        {
            List<GameObject> l = ObjectPooling._inst_singleton.TMT_GetActiveBullet();
            foreach (var i in l)
            {
                i.SetActive(false);
            }
            runCode = false;
            // anim.bossState = AnimBoss.State.idle;
            bossBeamAttack.SetActive(false);
            bossLightning.SetActive(false);
            bossGrenadeAttack.SetActive(false);
            lastTimeChangeState = 0;
            StartCoroutine(RunBossDisable());
            StopCoroutine(BossChangeAnim());
            StopCoroutine(ShootBeam());
            return;
        }

        if (runCode)
        {
            runCode = false;
            StartCoroutine(BossChangeAnim());
        }

        CreateLaze();

        if (!isMove)
            return;

        Moving();
    }

    void Moving()
    {
        endAnimMove = anim._endMove;
        if (endAnimMove && runCode1)
        {
            runCode1 = false;
            walkDir = -walkDir;
        }
        movement.x = walkDir * speed * Time.deltaTime;

        rb2.velocity = movement;

        if (rb2.velocity.x > maxSpeed)
            rb2.velocity = new Vector2(maxSpeed, rb2.velocity.y);
        if (rb2.velocity.x < -maxSpeed)
            rb2.velocity = new Vector2(-maxSpeed, rb2.velocity.y);
    }

    IEnumerator BossChangeAnim()
    {
        yield return new WaitUntil(() => lastTimeChangeState <= 0);
        int ran = (int)AnimBoss.State.beamAttack;
        anim.bossState = (AnimBoss.State)ran;
        lastTimeChangeState = timeChangeState;
        ActionAttack(ran);
        yield return new WaitUntil(() => lastTimeChangeState <= 0);
        if (walkDir > 0)
            anim.bossState = AnimBoss.State.walk;
        else if (walkDir < 0)
            anim.bossState = AnimBoss.State.backWalk;
        isMove = true;
        runCode1 = true;
        lastTimeChangeState = timeChangeState;
        yield return new WaitUntil(() => lastTimeChangeState <= 0);
        isMove = false;
        rb2.velocity = Vector2.zero;
        ran = RanAnim();
        anim.bossState = (AnimBoss.State)ran;
        lastTimeChangeState = timeChangeState;
        ActionAttack(ran);
        yield return new WaitUntil(() => lastTimeChangeState <= 0);
        ran = RanAnim();
        anim.bossState = (AnimBoss.State)ran;
        lastTimeChangeState = timeChangeState;
        ActionAttack(ran);
        yield return new WaitUntil(() => lastTimeChangeState <= 0);
        ran = RanAnim();
        anim.bossState = (AnimBoss.State)ran;
        lastTimeChangeState = timeChangeState;
        ActionAttack(ran);
        runCode = true;
    }

    int RanAnim()
    {
        int min, max;
        min = (int)AnimBoss.State.beamAttack;
        max = (int)AnimBoss.State.lightningAttack;
        if (isActiveGrenadeAttack)
            max = (int)AnimBoss.State.grenadeAttack;
        int ran = Random.Range(min, max + 1);
        return ran;
    }

    void ActionAttack(int index)
    {
        if (index == 1)
        {
            SetActiveBossAction(bossBeamAttack, bossLightning, bossGrenadeAttack);
            lineLaze.enabled = true;
            StartCoroutine(ShootBeam());
        }
        else if (index == 2)
        {
            SetActiveBossAction(bossLightning, bossBeamAttack, bossGrenadeAttack);
            audioLightning.Play();
        }
        else if (index == 3)
        {
            SetActiveBossAction(bossGrenadeAttack, bossLightning, bossBeamAttack);
        }
    }

    void SetActiveBossAction(GameObject gb1, GameObject gb2, GameObject gb3)
    {
        gb1.SetActive(true);
        gb2.SetActive(false);
        gb3.SetActive(false);
    }

    void CreateLaze()
    {
        Vector2 dir = (targetOfPlayer.transform.position - lazeBeam.transform.position).normalized;
        // float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // lazeBeam.transform.rotation = Quaternion.Euler(0, 0, angle + 180);
        // Debug.DrawRay(lazeBeam.transform.position, dir.normalized * 10, Color.green);

        lineLaze.SetPosition(0, lazeBeam.transform.position);
        lineLaze.SetPosition(1, dir.normalized * 1000);
    }

    private void OnDrawGizmos()
    {
        // Gizmos.color = Color.yellow;

        // Gizmos.DrawLine(targetOfPlayer.transform.position, lazeBeam.transform.position);
    }

    IEnumerator ShootBeam()
    {
        lastTimeShootBeam = timeShootBeam;
        yield return new WaitUntil(() => lastTimeShootBeam <= 0);
        GameObject g = ObjectPooling._inst_singleton.TMT_GetBossBeam(gunnerBeam);
        g.transform.position = bossBeamAttack.transform.position;
        g.SetActive(true);
        lineLaze.enabled = false;
        if (bossBeamAttack.activeSelf)
            audioBeam.Play();
    }

    public void TMT_SetRunCode2(bool b)
    {
        runCode2 = b;
    }

    IEnumerator RunBossDisable()
    {
        yield return new WaitUntil(() => runCode2);
        anim.bossState = AnimBoss.State.disabled;
    }

    public void TMT_RunBossAngry()
    {
        runCode2 = false;
        isAngry = true;
    }

    IEnumerator OffIsAngry()
    {
        lastTimeOffAngry = timeOffAngry;
        yield return new WaitUntil(() => lastTimeOffAngry <= 0);
        runCode3 = true;
        isAngry = false;
        bool isDeath = bossGetDmg._isDeath;
        if (!isDeath)
        {
            bossGetDmg.TMT_SetIsShield(true);
            runCode = true;
        }
    }

    void bossDie()
    {
        runCode4 = false;
        anim.bossState = AnimBoss.State.death;
        animUIHealth.SetBool("off", true);
        animUIShield.SetBool("off", true);
        audioBossDie.Play();
    }

    void DeactiveBoss()
    {
        if (!runCode6)
            return;

        runCode6 = false;
        canvasBossInfo.SetActive(false);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void TMT_RunAudioWalk()
    {
        if (walkDir > 0)
            audioBackWalk.Play();
        else if (walkDir < 0)
            audioWalk.Play();
    }
}