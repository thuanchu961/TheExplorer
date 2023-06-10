using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDmgZoneCtrl : MonoBehaviour
{
    [SerializeField] BossGetDmg bossGetDmg;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "StaffAttack")
        {
            bossGetDmg.TMT_SetDmgHealth(true);
        }
    }
}
