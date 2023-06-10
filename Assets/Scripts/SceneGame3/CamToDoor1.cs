using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CamToDoor1 : MonoBehaviour
{
    [SerializeField] GameObject gObject;
    Vector3 target = Vector3.zero;
    [SerializeField] Vector3 offset = Vector3.zero;
    [SerializeField] float speed;
    float rootTranformZ;
    [SerializeField] UnityEvent onEnter, even1;
    bool run = false, runEvent1 = false;
    // Start is called before the first frame update
    void Start()
    {
        speed = 1.5f;
        rootTranformZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        TMT_CamMove(gObject);
    }

    public void TMT_CamMove(GameObject go)
    {
        if (go == null)
            return;

        gObject = go;
        Vector3 pos = go.transform.position;
        target = pos - offset;
        target.z = rootTranformZ - offset.z;

        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x - target.x > -0.1f && run)
        {
            run = false;
            onEnter.Invoke();
        }

        if (transform.position.x - target.x > -0.1f && runEvent1)
        {
            runEvent1 = false;
            offset = new Vector3(0, -2.54f, -0.65f);
            even1.Invoke();
        }
    }

    public void TMT_SetRunOnEnter(bool b)
    {
        run = b;
    }

    public void TMT_SetRunEvent1(bool b)
    {
        runEvent1 = b;
    }

    public void TMT_SetOffset(Vector3 vec)
    {
        offset = vec;
    }
}
