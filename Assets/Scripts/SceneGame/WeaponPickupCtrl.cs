using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPickupCtrl : MonoBehaviour
{
    [SerializeField] GameObject guideBox, pickupEffect;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite changeSprite, defaultSprite;
    [SerializeField] string textGuide = "";
    [SerializeField] Text txtGuide;
    AnimGuideBox animGuideBox;
    [SerializeField] Collider2D circle2;
    [SerializeField] float timeGuide, lastTimeGuide = 0;
    bool run = false;
    // Start is called before the first frame update
    void Start()
    {
        animGuideBox = AnimGuideBox._inst_singleton;
        timeGuide = 30;
        TMT_SetActiveWeapon();
        defaultSprite = spriteRenderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (run)
            lastTimeGuide = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTimeGuide);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            spriteRenderer.sprite = changeSprite;
            txtGuide.text = textGuide;
            txtGuide.fontSize = 42;
            animGuideBox.TMT_SetIsGuide(true);
            guideBox.SetActive(true);
            circle2.enabled = false;
            PlayerCtrl._inst_singleton.TMT_SetStaffState(true);
            PlayerPrefs.SetInt("staffState", 1);
            pickupEffect.SetActive(true);
            StartCoroutine(DeactiveGuideBox());
        }
    }

    IEnumerator DeactiveGuideBox()
    {
        run = true;
        lastTimeGuide = timeGuide;
        yield return new WaitUntil(() => lastTimeGuide <= 0);
        animGuideBox.TMT_SetIsGuide(false);
        run = false;
    }

    public void TMT_SetActiveWeapon()
    {
        if (PlayerPrefs.HasKey("staffState"))
        {
            if (PlayerPrefs.GetInt("staffState") == 1)
            {
                spriteRenderer.sprite = changeSprite;
                circle2.enabled = false;
            }
            else if (PlayerPrefs.GetInt("staffState") == 0)
                spriteRenderer.sprite = defaultSprite;
        }
    }
}
