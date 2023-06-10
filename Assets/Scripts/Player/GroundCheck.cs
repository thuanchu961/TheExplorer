using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : Singleton<GroundCheck>
{
    bool isGround, isGroundThrough;
    public bool _isGround => isGround;
    public bool _isGroundThrough => isGroundThrough;
    SceneCtrl sceneCtrl;
    // Start is called before the first frame update
    void Start()
    {
        sceneCtrl = SceneCtrl._inst_singleton;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground" || other.tag == "GroundThrough")
        {
            isGround = true;
        }
        if (other.tag == "GroundThrough")
        {
            isGroundThrough = true;
        }
        if (other.tag == "Ground")
        {
            PlayerCtrl._inst_singleton.GetComponent<CapsuleCollider2D>().isTrigger = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Ground" || other.tag == "GroundThrough")
        {
            isGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ground" || other.tag == "GroundThrough")
        {
            isGround = false;
        }
        if (other.tag == "GroundThrough")
        {
            isGroundThrough = false;
        }
    }
}
