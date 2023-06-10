using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIKeyCtrl : MonoBehaviour
{
    [SerializeField] GameObject uikey1, uikey2, uikey3;
    int boxKey;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey("boxKey"))
        {
            boxKey = PlayerPrefs.GetInt("boxKey");
            if (boxKey == 1)
                uikey1.GetComponent<Animator>().SetBool("gain", true);
            else if (boxKey == 2)
            {
                uikey1.GetComponent<Animator>().SetBool("gain", true);
                uikey2.GetComponent<Animator>().SetBool("gain", true);
            }
            else if (boxKey == 3)
            {
                uikey1.GetComponent<Animator>().SetBool("gain", true);
                uikey2.GetComponent<Animator>().SetBool("gain", true);
                uikey3.GetComponent<Animator>().SetBool("gain", true);
            }
        }
    }
}
