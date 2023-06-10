using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BossGetDmg : MonoBehaviour
{
    [SerializeField]
    bool isGetDmgShield = false, isShield = true, isGetDmgHealth = false,
     stopGetDmgHealth = true, stopGetDmgShield = false, isDisabled = false, isShield2 = false,
      isDeath = false;
    public bool _isShield => isShield;
    public bool _isDeath => isDeath;
    [SerializeField] Animator animBossShield, animUIShield, animUIHealth;
    [SerializeField] Slider bossShieldSlider, bossHealthSlider;
    [SerializeField] GameObject bossShield, lightDmgZone, bossLight, platformMove, getDmgZone;
    [SerializeField] BossCtrl bossCtrl;
    float platformSpeed;
    [SerializeField]
    bool runCode1 = true, runCode2 = false, runCode3 = false, runCode4 = true,
     runCode5 = false, runCode6 = true, runCode7 = false;
    float playerDmgShield, playerDmgHp;
    float bossHp = 0, bossHpShield = 0;
    [SerializeField] int noShieldTime;
    public int _noShieldTime => noShieldTime;
    [SerializeField] CamToDoor1 camToDoor1;
    [SerializeField] float timeGetDmgHealth = 2, lastTimeGetDmgHealth = 0;
    [SerializeField] float timeSetOffFalse = 1, lastTimeSetOffFalse = 0;
    Vector2 oldPos, limitBossPos;
    [SerializeField] GameObject limitBoss;
    [SerializeField] UnityEvent event1, event2, event3;
    // Start is called before the first frame update
    void Start()
    {
        bossHpShield = bossShieldSlider.value;
        playerDmgShield = bossHpShield * 0.1f;
        bossHp = bossHealthSlider.value;
        playerDmgHp = bossHp * 0.1f;
        limitBossPos = limitBoss.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDeath)
        {
            animUIHealth.SetBool("getDmg", false);
            return;
        }

        lastTimeGetDmgHealth = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTimeGetDmgHealth);
        lastTimeSetOffFalse = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTimeSetOffFalse);
        limitBoss.transform.position = limitBossPos;

        if (!isShield && runCode5)
        {
            runCode5 = false;
        }

        if (!isShield && runCode1)
        {
            if (bossHealthSlider.value > 0)
                isDisabled = true;
            runCode1 = false;

            if (runCode6)
            {
                runCode6 = false;
                platformSpeed = platformMove.GetComponent<MovingPlatformCtrl>()._speed;
                platformMove.GetComponent<MovingPlatformCtrl>().TMT_SetSpeed(false);
                event1.Invoke();
            }
        }

        RunCode7();
        TurnOnShield2();
        GetDmgShield();
        GetDmgHealth();

        if (isDisabled)
            transform.position = oldPos;

        if (!lightDmgZone.activeSelf)
            return;

        runCode2 = true;

        RunEvent();
    }

    void GetDmgShield()
    {
        if (stopGetDmgShield)
            return;

        if (bossShieldSlider.value <= 0)
            return;

        if (isGetDmgShield)
        {
            isGetDmgShield = false;
            animUIShield.SetBool("getDmg", true);
            bossShieldSlider.value -= playerDmgShield;
            if (bossShieldSlider.value <= 0)
            {
                stopGetDmgHealth = false;
                stopGetDmgShield = true;
                oldPos = transform.position;
                isShield = false;
            }
            StartCoroutine(SetOffFalse());
        }
    }

    void GetDmgHealth()
    {
        if (stopGetDmgHealth)
            return;

        if (lastTimeGetDmgHealth > 0)
            return;

        if (isGetDmgHealth)
        {
            stopGetDmgShield = false;
            isGetDmgHealth = false;
            animUIHealth.SetBool("getDmg", true);
            StartCoroutine(SetOffFalse());
            bossHealthSlider.value -= playerDmgHp;
            if (bossHealthSlider.value <= bossHp / 2 && runCode4)
            {
                runCode4 = false;
                StopBossGetDmg();
            }
            if (bossHealthSlider.value <= 0)
            {
                isDeath = true;
                getDmgZone.SetActive(false);
                StopBossGetDmg();
            }
            lastTimeGetDmgHealth = timeGetDmgHealth;
        }
    }

    void StopBossGetDmg()
    {
        stopGetDmgHealth = true;
        bossCtrl.TMT_RunBossAngry();
    }

    public void TMT_SetIsGetDmgShield(bool b)
    {
        isGetDmgShield = b;
    }

    IEnumerator SetOffFalse()
    {
        lastTimeSetOffFalse = timeSetOffFalse;
        yield return new WaitUntil(() => lastTimeSetOffFalse <= 0);
        animUIShield.SetBool("getDmg", false);
        animBossShield.SetBool("close", false);
        animUIHealth.SetBool("getDmg", false);
    }

    public void TMT_SetDmgHealth(bool b)
    {
        isGetDmgHealth = b;
    }

    public void TMT_DisableBoss()
    {
        animBossShield.SetBool("close", true);
        StartCoroutine(SetOffFalse());

        if (runCode7)
            return;

        lightDmgZone.SetActive(true);
        bossLight.SetActive(false);
    }

    void RunEvent()
    {
        if (runCode2)
        {
            runCode2 = false;
            event2.Invoke();
            runCode3 = true;
            camToDoor1.TMT_SetOffset(new Vector3(0, -1.52f, -7.36f));
        }

        if (runCode3)
            if (Input.GetKeyUp(KeyCode.E))
            {
                runCode3 = false;
                event3.Invoke();
            }
    }

    public void TMT_SetIsShield(bool b)
    {
        isShield = b;
        bossShieldSlider.value = bossHpShield;
        isShield2 = true;
    }

    void TurnOnShield2()
    {
        if (isShield2)
        {
            isShield2 = false;
            isDisabled = false;
            runCode1 = true;
            animBossShield.SetBool("open", true);
            animUIShield.SetBool("on", true);
            bossCtrl.TMT_SetRunCode2(true);
            StartCoroutine(SetOnShieldFalse());
        }
    }

    IEnumerator SetOnShieldFalse()
    {
        yield return new WaitForEndOfFrame();
        animBossShield.SetBool("open", false);
        animUIShield.SetBool("on", false);
        runCode7 = true;
    }

    void RunCode7()
    {
        if (runCode7 && isDisabled)
        {
            TMT_DisableBoss();
            runCode7 = false;
        }
    }

    public void TMT_SetActiveBossShield(bool b)
    {
        bossShield.SetActive(b);
    }
}
