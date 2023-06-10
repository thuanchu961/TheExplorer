using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpactCtrl : MonoBehaviour
{
    [SerializeField] bool deactiveBullet = false;

    private void OnEnable()
    {
        deactiveBullet = false;
        StartCoroutine(DeActive());
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator DeActive()
    {
        yield return new WaitUntil(() => deactiveBullet);
        gameObject.SetActive(false);
    }

    void TMT_SetDeactiveBullet()
    {
        deactiveBullet = true;
    }
}
