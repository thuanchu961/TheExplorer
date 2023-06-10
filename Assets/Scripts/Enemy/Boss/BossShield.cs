using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShield : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] PlayerCtrl playerCtrl;
    [SerializeField] BossGetDmg bossGetDmg;
    bool isShield;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip onShield, offShield;

    // private void Update()
    // {
    //     // CheckShield();
    // }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            Vector2 vec = Vector2.zero;
            vec.x = -(force * 30 * playerCtrl.transform.localScale.x);
            vec.y = force;
            playerCtrl.GetComponent<Rigidbody2D>().AddForce(vec);
            playerCtrl.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerBullet")
            bossGetDmg.TMT_SetIsGetDmgShield(true);
    }

    void SetOnShield()
    {
        isShield = true;
        CheckShield();
    }

    void SetOffShield()
    {
        isShield = false;
        CheckShield();
    }

    void CheckShield()
    {
        if (isShield)
        {
            audioSource.clip = onShield;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = offShield;
            audioSource.Play();
        }
    }
}
