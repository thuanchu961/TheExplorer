using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnIndicatorCtrl : MonoBehaviour
{
    float x;
    [SerializeField]float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 350;
    }

    // Update is called once per frame
    void Update()
    {
        x += Time.deltaTime * speed;
        transform.rotation = Quaternion.Euler(0, 0, x);
    }
}
