using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenTheDoor : MonoBehaviour
{
    [SerializeField] float timeOnEnter, lastTimeOnEnter = 0;
    bool run = false, alreayRun = false;
    [SerializeField] UnityEvent event1, event2;
    // Start is called before the first frame update
    void Start()
    {
        timeOnEnter = 6;
    }

    // Update is called once per frame
    void Update()
    {
        if (run)
            lastTimeOnEnter = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTimeOnEnter);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerBullet")
        {
            event1.Invoke();
            if (!alreayRun)
                StartCoroutine(ActiveEvent2());
        }
    }

    IEnumerator ActiveEvent2()
    {
        alreayRun = true;
        run = true;
        lastTimeOnEnter = timeOnEnter;
        yield return new WaitUntil(() => lastTimeOnEnter <= 0);
        run = false;
        event2.Invoke();
    }
}
