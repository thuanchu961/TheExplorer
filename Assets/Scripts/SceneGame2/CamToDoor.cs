using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamToDoor : MonoBehaviour
{
    int stateKey;
    bool enableCam = true, doorRunAnim = false, setDoorEffect = true;
    [SerializeField] GameObject hubDoor;
    [SerializeField] Vector3 target, offset = Vector3.zero;
    float speed;
    [SerializeField] float timePreRunAnim, lastTimePreRunAnim = 0;
    [SerializeField] float timeEndRunAnim, lastTimeEndRunAnim = 0;
    // Start is called before the first frame update
    void Start()
    {
        speed = 2;
        timePreRunAnim = 3;
        timeEndRunAnim = 22;
    }

    // Update is called once per frame
    void Update()
    {
        lastTimePreRunAnim = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTimePreRunAnim);
        lastTimeEndRunAnim = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTimeEndRunAnim);

        if (PlayerPrefs.HasKey("Key1"))
        {
            stateKey = PlayerPrefs.GetInt("Key1");
            if (PlayerPrefs.HasKey("enableCam"))
            {
                if (PlayerPrefs.GetInt("enableCam") == 1)
                    enableCam = true;
                else
                    enableCam = false;
            }
            else
                enableCam = true;

            if (stateKey == 1 && enableCam)
            {
                enableCam = false;
                PlayerPrefs.SetInt("enableCam", 0);
                GetComponent<CamMoveToPlayer>().enabled = false;
                StartCoroutine(CallRunCam());
                PlayerCtrl._inst_singleton.TMT_SetIsMove(false);
                GetComponent<Camera>().orthographicSize = 3;
            }

        }

        if (doorRunAnim)
        {
            RunCamToDoor();
            PlayerCtrl._inst_singleton.TMT_SetIsMove(false);
        }
    }

    IEnumerator CallRunCam()
    {
        lastTimePreRunAnim = timePreRunAnim;
        yield return new WaitUntil(() => lastTimePreRunAnim <= 0);
        lastTimeEndRunAnim = timeEndRunAnim;
        doorRunAnim = true;
    }

    public void RunCamToDoor()
    {
        target = hubDoor.transform.position - offset;
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
        if (setDoorEffect)
            StartCoroutine(CallEndRunCam());
    }

    IEnumerator CallEndRunCam()
    {
        setDoorEffect = false;
        HubDoor2._inst_singleton.TMT_SetStateAnimDoor();
        yield return new WaitUntil(() => lastTimeEndRunAnim <= 0);
        GetComponent<CamMoveToPlayer>().enabled = true;
        PlayerCtrl._inst_singleton.TMT_SetIsMove(true);
        doorRunAnim = false;
    }
}
