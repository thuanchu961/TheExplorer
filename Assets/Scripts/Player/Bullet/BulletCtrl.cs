using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    [SerializeField] float speed, way;
    public float _way
    {
        get
        {
            return way;
        }
        set
        {
            way = value;
        }
    }
    [SerializeField] Rigidbody2D rb2;
    [SerializeField] GameObject bulletImpact;
    [SerializeField] float time, lastTime = 0;
    Vector2 scale;
    private void OnEnable()
    {
        time = 2.5f;
        StartCoroutine(DecativeBullet());
    }
    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        lastTime = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTime);
        scale.x = way;
        transform.localScale = scale;
        rb2.velocity = new Vector2(way * speed * Time.deltaTime, 0);
    }

    IEnumerator DecativeBullet()
    {
        lastTime = time;
        yield return new WaitUntil(() => lastTime <= 0);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyBullet")
            return;
        GameObject g = ObjectPooling._inst_singleton.TMT_GetPlayerBulletImpact(bulletImpact);
        g.transform.position = transform.position;
        g.SetActive(true);
        g.transform.localScale = CommonFunc._inst_singleton.TMT_ChangeLocalScale(transform.localScale.x, g.transform.localScale);
        gameObject.SetActive(false);
    }
}
