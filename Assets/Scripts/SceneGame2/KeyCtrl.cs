using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCtrl : MonoBehaviour
{
    [SerializeField] GameObject pickupEffect;
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
        if (other.tag == "Player")
        {
            pickupEffect.SetActive(true);
            BoxKeyCtrl._inst_singleton.TMT_SetStateKey(gameObject);
        }
    }
}
