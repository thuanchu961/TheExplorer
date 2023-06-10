using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommonFunc : Singleton<CommonFunc>
{
    PlayerCtrl playerCtrl;
    Scene sceneTMT;
    // Start is called before the first frame update
    void Start()
    {
        sceneTMT = SceneManager.GetActiveScene();
        if (sceneTMT.buildIndex == 0 || sceneTMT.buildIndex == 5 || sceneTMT.buildIndex == 6)
            return;

        playerCtrl = PlayerCtrl._inst_singleton;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 TMT_ChangeLocalScale(float objScale, Vector3 localScale)
    {
        Vector3 scale = localScale;
        if (objScale == 0)
            return localScale;
        if (objScale > 0)
            scale.x = Mathf.Abs(scale.x);
        if (objScale < 0)
            scale.x = -Mathf.Abs(scale.x);
        return scale;
    }

    public float TMT_DelayTimeCount(float time)
    {
        if (time <= 0)
            return time;

        time -= Time.deltaTime * 5;
        return time;
    }

    public void TMT_CheckStaffState()
    {
        int state = 0;
        if (PlayerPrefs.HasKey("staffState"))
        {
            state = PlayerPrefs.GetInt("staffState");
            if (state == 1 && playerCtrl != null)
                playerCtrl.TMT_SetStaffState(true);

            if (state == 0)
                playerCtrl.TMT_SetStaffState(false);
        }
    }
}
