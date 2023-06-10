using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCtrlBase : MonoBehaviour
{
    protected float dmg;
    [SerializeField] protected Slider hpEnemy;
    [SerializeField] protected AnimEnemyBase anim;
    [SerializeField] protected float speed;
    protected int way;
    [SerializeField] protected Rigidbody2D rb2;
    [SerializeField] protected GameObject target;
    [SerializeField]
    protected bool isMove = true, isDeath = false, isAttack = false, isLimit = false,
     isGetDame = false;
    [SerializeField] protected float rangeDetect, attackZone;
    [SerializeField] protected PlayerCtrl playerCtrl;
    [SerializeField] protected float getForce, forceWay;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void Moving()
    {
        rb2.velocity = new Vector2(speed * way * Time.deltaTime, 0);
    }

    protected virtual void ChangeAnim()
    {
        if (isDeath)
        {
            anim.enemyState = AnimEnemyBase.State.death;
            rb2.velocity = Vector2.zero;
            return;
        }
        else if (isAttack)
            anim.enemyState = AnimEnemyBase.State.attack;
        else if (target != null && isMove)
            anim.enemyState = AnimEnemyBase.State.run;
        else if (rb2.velocity.x == 0)
            anim.enemyState = AnimEnemyBase.State.idle;
        else
            anim.enemyState = AnimEnemyBase.State.walk;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {

    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        // isLimit = false;
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "StaffAttack")
        {
            getDame(getForce);
        }
        if (other.collider.tag == "PlayerBullet")
        {
            getDame(getForce / 2);
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D other)
    {
        isGetDame = false;
    }

    protected virtual void getDame(float forceTemp)
    {
        isGetDame = true;
        isMove = false;
        rb2.velocity = Vector2.zero;
        forceWay = (int)playerCtrl.transform.localScale.x;
        rb2.AddForce(new Vector2(forceTemp * forceWay, forceTemp * 2));
    }

    protected virtual void DetectTarget()
    {
        if (Vector2.Distance(playerCtrl.transform.position, transform.position) > rangeDetect)
            target = null;
        else
            target = playerCtrl.gameObject;

        if (Vector2.Distance(playerCtrl.transform.position, transform.position) > attackZone)
            isAttack = false;
        else
            isAttack = true;
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeDetect);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackZone);
    }

    protected virtual void Death()
    {
        isDeath = true;
        isMove = false;
        rb2.velocity = Vector2.zero;
        gameObject.layer = 12;
    }
}
