using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimChomper : AnimEnemyBase
{
    [SerializeField] Animator anim;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ChangeAnim();
    }

    void ChangeAnim()
    {
        for (int i = 0; i < (int)AnimEnemyBase.State.len; i++)
        {
            if ((int)enemyState == i)
                anim.SetBool(enemyState.ToString(), true);
            else
                anim.SetBool(((AnimEnemyBase.State)i).ToString(), false);
        }
    }

    void Death()
    {
        gameObject.GetComponentInParent<EnemyChomper>().gameObject.SetActive(false);
    }
}
