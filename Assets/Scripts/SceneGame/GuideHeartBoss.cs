using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideHeartBoss : InfoPostCrtl
{
    // Start is called before the first frame update
    void Start()
    {
        animGuideBox = AnimGuideBox._inst_singleton;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TMT_RunGuide()
    {
        txtGuide.text = textGuide;
        txtGuide.fontSize = 50;
        animGuideBox.TMT_SetIsGuide(true);
        guideBox.SetActive(true);
    }

    public void TMT_OffGuide()
    {
        animGuideBox.TMT_SetIsGuide(false);
    }
}
