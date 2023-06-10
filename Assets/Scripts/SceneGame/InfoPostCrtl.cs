using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InfoPostCrtl : MonoBehaviour
{
    [SerializeField] protected GameObject guideBox;
    [SerializeField] protected string textGuide = "";
    [SerializeField] protected Text txtGuide;
    protected AnimGuideBox animGuideBox;
    [SerializeField] GameObject pointLight;
    [SerializeField] SpriteRenderer spriteRenderer;
    Sprite defaultSprite;
    [SerializeField] Sprite changeSprite;
    [SerializeField] int fontSize = 36;
    // Start is called before the first frame update
    void Start()
    {
        defaultSprite = spriteRenderer.sprite;
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
            pointLight.SetActive(true);
            spriteRenderer.sprite = changeSprite;
            txtGuide.text = textGuide;
            txtGuide.fontSize = fontSize;
            animGuideBox.TMT_SetIsGuide(true);
            guideBox.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            spriteRenderer.sprite = defaultSprite;
            pointLight.SetActive(false);
            animGuideBox.TMT_SetIsGuide(false);
        }
    }
}
