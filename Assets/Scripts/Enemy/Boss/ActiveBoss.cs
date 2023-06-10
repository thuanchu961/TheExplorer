using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActiveBoss : MonoBehaviour
{
    [SerializeField] GameObject audioCuts, boss;
    [SerializeField] float timeEvent2, lastTimeEvent2 = 0;
    bool run1 = true;
    [SerializeField] UnityEvent event1, event2;
    // Start is called before the first frame update
    void Start()
    {
        timeEvent2 = 3;
    }

    // Update is called once per frame
    void Update()
    {
        lastTimeEvent2 = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTimeEvent2);

        if (boss.activeSelf && run1)
            StartCoroutine(ExecuteEvent2());

        if (!audioCuts.activeSelf)
            event1.Invoke();
    }

    IEnumerator ExecuteEvent2()
    {
        run1 = false;
        lastTimeEvent2 = timeEvent2;
        if (!boss.activeSelf)
            while (lastTimeEvent2 > 0)
                PlayerCtrl._inst_singleton.TMT_SetIsMove(false);
        yield return new WaitUntil(() => lastTimeEvent2 <= 0);
        event2.Invoke();
    }
}
