using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCtrl : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] PlayerCtrl playerCtrl;
    [SerializeField] float time, lastTime = 0;
    Vector3 pos;
    float scale = 0;
    bool run = false;
    [SerializeField] GameObject audioShoot;
    SceneCtrl sceneCtrl;
    // Start is called before the first frame update
    void Start()
    {
        time = 2;
        sceneCtrl = SceneCtrl._inst_singleton;
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneCtrl._sceneLoad.activeSelf)
            return;

        if (playerCtrl._isDeath)
            return;

        if (run)
            lastTime = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTime);

        scale = playerCtrl.transform.localScale.x;
        transform.localScale = playerCtrl.transform.localScale;
        pos = transform.position;
        pos.z = playerCtrl.transform.position.z;

        if (lastTime > 0)
        {
            run = true;
            return;
        }

        if (playerCtrl._isShootActackState)
        {
            GameObject g = ObjectPooling._inst_singleton.TMT_GetPlayerBullet(bullet);

            g.GetComponent<BulletCtrl>()._way = scale;
            g.transform.position = pos;
            g.SetActive(true);
            run = false;
            audioShoot.GetComponent<AudioBase>().TMT_RandomAudioClip();
            lastTime = time;
        }
    }
}
