using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMoveToPlayer : MonoBehaviour
{
    PlayerCtrl playerCtrl;
    Vector3 target = Vector3.zero;
    [SerializeField] Vector3 offset = Vector3.zero;
    [SerializeField] float speed;
    float rootTranformZ;
    // Start is called before the first frame update
    void Start()
    {
        playerCtrl = PlayerCtrl._inst_singleton;
        speed = 2;
        rootTranformZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        target = playerCtrl.transform.position - offset;
        target.z = rootTranformZ;

        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
    }
}
