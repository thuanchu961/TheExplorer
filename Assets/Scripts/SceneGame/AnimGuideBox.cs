using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimGuideBox : Singleton<AnimGuideBox>
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject bgDialog;
    private void Awake()
    {
        bgDialog.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TMT_SetIsGuide(bool b)
    {
        if (b)
            SetBoolAnim(true);
        else
            SetBoolAnim(false);
    }

    void SetBoolAnim(bool b)
    {
        anim.SetBool("show", b);
    }
}
