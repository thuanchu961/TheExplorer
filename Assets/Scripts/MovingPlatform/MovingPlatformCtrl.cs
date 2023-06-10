using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformCtrl : MonoBehaviour
{
    [SerializeField] float spaceToMoveX = 0, spaceToMoveY = 0, speed, targetPoint, rootPosition, oldSpeed;
    public float _speed => speed;
    [SerializeField] Vector3 target = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        speed = 2;
        if (spaceToMoveX != 0)
        {
            rootPosition = transform.position.x;
            targetPoint = rootPosition + spaceToMoveX;
        }
        if (spaceToMoveY != 0)
        {
            rootPosition = transform.position.y;
            targetPoint = rootPosition + spaceToMoveY;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spaceToMoveX != 0)
            MovingX();
        if (spaceToMoveY != 0)
            MovingY();

        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    void MovingX()
    {
        if (transform.position.x == rootPosition)
            target = new Vector3(targetPoint, transform.position.y, 0);
        else if (transform.position.x == targetPoint)
            target = new Vector3(rootPosition, transform.position.y, 0);
    }

    void MovingY()
    {
        if (transform.position.y == rootPosition)
            target = new Vector2(transform.position.x, targetPoint);
        else if (transform.position.y == targetPoint)
            target = new Vector2(transform.position.x, rootPosition);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
            other.gameObject.transform.SetParent(transform, true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
            other.gameObject.transform.parent = null;
    }

    public void TMT_SetSpeed(bool b)
    {
        if (!b)
        {
            oldSpeed = speed;
            speed = 0;
        }
        else
            speed = oldSpeed;
    }
}
