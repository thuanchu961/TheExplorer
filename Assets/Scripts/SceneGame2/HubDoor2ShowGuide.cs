using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubDoor2ShowGuide : MonoBehaviour
{
    [SerializeField] GameObject guideBox;
    [SerializeField] string textGuide = "";
    [SerializeField] Text txtGuide;
    AnimGuideBox animGuideBox;
    // Start is called before the first frame update
    void Start()
    {
        animGuideBox = AnimGuideBox._inst_singleton;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            txtGuide.text = textGuide;
            txtGuide.fontSize = 57;
            animGuideBox.TMT_SetIsGuide(true);
            guideBox.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            animGuideBox.TMT_SetIsGuide(false);
        }
    }
}
