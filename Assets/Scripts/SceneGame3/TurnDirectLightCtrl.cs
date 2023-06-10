using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnDirectLightCtrl : MonoBehaviour
{
    [SerializeField] int turnOnOff;
    [SerializeField] GameObject directLight;
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
            if (turnOnOff == 0)
                directLight.SetActive(false);
            if (turnOnOff == 1)
                directLight.SetActive(true);
        }
    }
}
