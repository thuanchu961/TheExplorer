using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffAttackCtrl : MonoBehaviour
{
    [SerializeField] PlayerCtrl playerCtrl;
    [SerializeField] Rigidbody2D rb2;
    [SerializeField] float way, force;
    // Start is called before the first frame update
    void Start()
    {
        force = 100;
    }

    // Update is called once per frame
    void Update()
    {
        way = playerCtrl.transform.localScale.x;
        if (playerCtrl._isStaffActtack)
        {
            rb2.AddForce(new Vector2(force * way * Time.deltaTime, 0));
        }
    }
}
