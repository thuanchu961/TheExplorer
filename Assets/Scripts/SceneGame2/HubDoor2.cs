using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubDoor2 : Singleton<HubDoor2>
{
    [SerializeField] GameObject pickUpEffect, topLight;
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite door0, door1, door2, door3;
    [SerializeField] float time, lastTime = 0;
    bool runAnimDoor = false, enablePortal = false, run = true;
    [SerializeField] CircleCollider2D circle;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        time = 12;
        runAnimDoor = false;

        CheckKeyAndDoor();
    }

    // Update is called once per frame
    void Update()
    {
        if (enablePortal)
            PortalSceneBoss();

        if (runAnimDoor)
            lastTime = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTime);

        if (run)
        {
            TurnOpenStatus();
        }
    }

    public void TMT_SetStateAnimDoor()
    {
        lastTime = time;
        runAnimDoor = true;
        StartCoroutine(RunEffectDoor());
    }

    IEnumerator RunEffectDoor()
    {
        pickUpEffect.SetActive(false);
        yield return new WaitUntil(() => lastTime <= 0);
        topLight.SetActive(true);
        pickUpEffect.SetActive(true);
        CheckKeyAndDoor();
        runAnimDoor = false;
    }

    void CheckKeyAndDoor()
    {
        if (PlayerPrefs.HasKey("boxKey"))
        {
            topLight.SetActive(true);
            if (PlayerPrefs.GetInt("boxKey") == 1)
            {
                spriteRenderer.sprite = door1;
            }
            else if (PlayerPrefs.GetInt("boxKey") == 2)
            {
                spriteRenderer.sprite = door2;
            }
            else if (PlayerPrefs.GetInt("boxKey") == 3)
            {
                spriteRenderer.sprite = door3;
                run = true;
            }
        }
        else
            spriteRenderer.sprite = door0;
    }

    void TurnOpenStatus()
    {
        if (PlayerPrefs.HasKey("boxKey"))
        {
            if (PlayerPrefs.GetInt("boxKey") == 3)
            {
                run = false;
                circle.enabled = true;
                enablePortal = true;
            }
            else
            {
                run = false;
                circle.enabled = false;
                enablePortal = false;
            }
        }
    }

    void PortalSceneBoss()
    {
        if (Input.GetKey(KeyCode.E))
            SceneCtrl._inst_singleton.TMT_CallLoadScene(4);
    }
}
