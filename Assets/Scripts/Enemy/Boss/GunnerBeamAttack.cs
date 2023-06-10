using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerBeamAttack : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb2;
    Vector2 movement = Vector2.zero, rootPos, targetPos;
    [SerializeField] GameObject target, beamEffect;
    Vector2 dir;
    float maxSpeed = -22.5f;
    // Start is called before the first frame update
    private void OnEnable()
    {
        rb2.velocity = Vector2.zero;
        // transform.position = rootPos;
        target = PlayerCtrl._inst_singleton.GetComponentInChildren<TargetPlayer>().gameObject;
        dir = target.transform.position - transform.position;
        dir = dir.normalized;
    }
    void Start()
    {
        // speed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        rb2.velocity = speed * dir * Time.deltaTime;

        if (rb2.velocity.x < maxSpeed)
            rb2.velocity = new Vector2(maxSpeed, rb2.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "PlayerBullet")
            return;
        gameObject.SetActive(false);
        GameObject g = ObjectPooling._inst_singleton.TMT_GetBossBeamEffect(beamEffect);
        g.transform.position = transform.position;
        g.SetActive(true);
    }
}
