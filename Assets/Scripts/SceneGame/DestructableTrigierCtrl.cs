using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableTrigierCtrl : MonoBehaviour
{
    [SerializeField] GameObject whole, broken, destructable;
    PlayerCtrl playerCtrl;
    [SerializeField] float timeDeactive, lastTimeDeactive = 0;
    bool run = false;
    // Start is called before the first frame update
    void Start()
    {
        playerCtrl = PlayerCtrl._inst_singleton;
        timeDeactive = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (run)
            lastTimeDeactive = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTimeDeactive);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "StaffAttack")
        {
            whole.SetActive(false);
            broken.SetActive(true);
            broken.GetComponent<EdgeCollider2D>().enabled = false;
            run = true;
            lastTimeDeactive = timeDeactive;
            StartCoroutine(DeactiveDestructable());
        }
    }

    IEnumerator DeactiveDestructable()
    {
        yield return new WaitUntil(() => lastTimeDeactive <= 0);
        destructable.SetActive(false);
        run = false;
    }
}
