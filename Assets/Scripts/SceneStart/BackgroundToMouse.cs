using UnityEngine;

public class BackgroundToMouse : MonoBehaviour
{
    [SerializeField] Vector3 rootPosition;
    [SerializeField] float offset = 0.00001f;
    // Start is called before the first frame update
    void Start()
    {
        rootPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Vector2.zero;
        pos.x = rootPosition.x + (offset * Input.mousePosition.x);
        pos.y = rootPosition.y + (offset * Input.mousePosition.y);
        pos.z = transform.position.z;

        transform.position = pos;
    }
}
