using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWithMouse : Singleton<ObjectWithMouse>
{
    Vector2 rootPosition;
    [SerializeField] float speed = 1;
    [SerializeField] Vector3 target = Vector3.zero;
    [SerializeField] bool runCode = true;
    public bool _runCode => runCode;
    // Start is called before the first frame update
    void Start()
    {
        rootPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            target = hit.point;
        }

        transform.position = Vector3.Lerp(transform.position, 
            target, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "MouseZone")
        {
            runCode = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "MouseZone")
        {
            runCode = true;
        }
    }
}
