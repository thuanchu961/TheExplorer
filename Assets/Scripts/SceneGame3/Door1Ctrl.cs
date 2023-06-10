using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door1Ctrl : MonoBehaviour
{
    [SerializeField] GameObject doorSprite, boss, pointActiveBoss;
    [SerializeField] float timeDelayCam, lastTimeDelayCam = 0;
    [SerializeField] UnityEvent onEnter;
    bool run = false, runOneTime = true;
    // Start is called before the first frame update
    void Start()
    {
        timeDelayCam = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (boss != null)
        {
            BossActiveDoor();
            return;
        }

        if (run)
            lastTimeDelayCam = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTimeDelayCam);

        if (doorSprite.activeSelf)
            return;

        if (runOneTime)
        {
            runOneTime = false;
            StartCoroutine(DelayTimeCam());
        }
    }

    IEnumerator DelayTimeCam()
    {
        run = true;
        lastTimeDelayCam = timeDelayCam;
        yield return new WaitUntil(() => lastTimeDelayCam <= 0);
        run = false;
        onEnter.Invoke();
    }

    void BossActiveDoor()
    {
        if (!boss.activeSelf && !pointActiveBoss.activeSelf)
            onEnter.Invoke();
    }
}
