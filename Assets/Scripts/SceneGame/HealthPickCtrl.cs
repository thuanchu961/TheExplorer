using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickCtrl : MonoBehaviour
{
    [SerializeField] GameObject broke, whole;
    [SerializeField] AudioSource audioSource;
    [SerializeField] CircleCollider2D circle;
    int boxHealth;
    bool pickedup;
    // Start is called before the first frame update
    void Start()
    {
        pickedup = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pickedup)
            return;
        if (PlayerPrefs.HasKey("boxHealth"))
        {
            boxHealth = PlayerPrefs.GetInt("boxHealth");
            if (boxHealth == 5)
                circle.enabled = false;
            else
                circle.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            broke.SetActive(true);
            whole.SetActive(false);
            audioSource.Play();
            circle.enabled = false;
            pickedup = true;
        }
    }
}
