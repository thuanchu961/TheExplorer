using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsCreditsCtrl : MonoBehaviour
{
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float max = 1128, min = 651;
        Vector2 pos = transform.position;
        pos.y += Time.deltaTime * speed;
        transform.position = pos;

        if (pos.y > max)
            speed = -Mathf.Abs(speed);
        if (pos.y < min)
            speed = Mathf.Abs(speed);
    }
}
