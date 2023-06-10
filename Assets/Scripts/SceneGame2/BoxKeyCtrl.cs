using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxKeyCtrl : Singleton<BoxKeyCtrl>
{
    [SerializeField] float timeDeactive, lastTimeDeactive = 0;
    [SerializeField] GameObject key1, key2, key3;
    bool run = false;
    private void OnEnable()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        timeDeactive = 1.5f;
        CheckAndSetKey();
    }

    // Update is called once per frame
    void Update()
    {
        if (run)
            lastTimeDeactive = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTimeDeactive);
    }

    void CheckAndSetKey()
    {
        if (!PlayerPrefs.HasKey("Key1"))
            PlayerPrefs.SetInt("Key1", 0);
        else
        {
            key1 = GameObject.FindGameObjectWithTag("Key1");
            if (key1 != null)
            {
                if (PlayerPrefs.GetInt("Key1") == 0)
                    key1.SetActive(true);
                else if (PlayerPrefs.GetInt("Key1") == 1)
                    key1.SetActive(false);
            }
        }
        if (!PlayerPrefs.HasKey("Key2"))
            PlayerPrefs.SetInt("Key2", 0);
        else
        {
            key2 = GameObject.FindGameObjectWithTag("Key2");
            if (key2 != null)
            {
                if (PlayerPrefs.GetInt("Key2") == 0)
                    key2.SetActive(true);
                else if (PlayerPrefs.GetInt("Key2") == 1)
                    key2.SetActive(false);
            }
        }
        if (!PlayerPrefs.HasKey("Key3"))
            PlayerPrefs.SetInt("Key3", 0);
        else
        {
            key3 = GameObject.FindGameObjectWithTag("Key3");
            if (key3 != null)
            {
                if (PlayerPrefs.GetInt("Key3") == 0)
                    key3.SetActive(true);
                else if (PlayerPrefs.GetInt("Key3") == 1)
                    key3.SetActive(false);
            }
        }
    }

    IEnumerator DeactiveKey(GameObject key, string keyName)
    {
        int keys = 0;
        run = true;
        lastTimeDeactive = timeDeactive;
        yield return new WaitUntil(() => lastTimeDeactive <= 0);
        PlayerPrefs.SetInt(keyName, 1);
        if (PlayerPrefs.HasKey("boxKey"))
        {
            keys = PlayerPrefs.GetInt("boxKey");
            if (keys == 1)
                keys = 2;
            else if (keys == 2)
                keys = 3;
            PlayerPrefs.SetInt("boxKey", keys);
        }
        else
        {
            keys = 1;
            PlayerPrefs.SetInt("boxKey", keys);
        }
        key.SetActive(false);
        run = false;
    }

    public void TMT_SetStateKey(GameObject g)
    {
        StartCoroutine(DeactiveKey(g, g.tag));
    }
}
