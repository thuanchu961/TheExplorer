using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealthCrtl : MonoBehaviour
{
    [SerializeField] GameObject health1, health2, health3, health4, health5;
    int boxHealth, lossOrGain;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("eventHealth", 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey("boxHealth"))
        {
            boxHealth = PlayerPrefs.GetInt("boxHealth");
            lossOrGain = PlayerPrefs.GetInt("eventHealth");
            if (lossOrGain == 1)
                GainHealth();
            else if (lossOrGain == 0)
                LossHealth();
            else if (lossOrGain == 2)
                SetHealth();
        }
        else
        {
            PlayerPrefs.SetInt("boxHealth", 5);
        }
    }

    void LossHealth()
    {
        if (boxHealth == 4)
            LossGainState(health5, "loss", "gain");
        else if (boxHealth == 3)
        {
            LossGainState(health5, "loss", "gain");
            LossGainState(health4, "loss", "gain");
        }
        else if (boxHealth == 2)
        {
            LossGainState(health5, "loss", "gain");
            LossGainState(health4, "loss", "gain");
            LossGainState(health3, "loss", "gain");
        }
        else if (boxHealth == 1)
        {
            LossGainState(health5, "loss", "gain");
            LossGainState(health4, "loss", "gain");
            LossGainState(health3, "loss", "gain");
            LossGainState(health2, "loss", "gain");
        }
        else if (boxHealth == 0)
        {
            LossGainState(health5, "loss", "gain");
            LossGainState(health4, "loss", "gain");
            LossGainState(health3, "loss", "gain");
            LossGainState(health2, "loss", "gain");
            LossGainState(health1, "loss", "gain");
        }
    }

    void GainHealth()
    {
        if (boxHealth == 1)
            LossGainState(health1, "gain", "loss");
        else if (boxHealth == 2)
        {
            LossGainState(health2, "gain", "loss");
            LossGainState(health1, "gain", "loss");
        }
        else if (boxHealth == 3)
        {
            LossGainState(health3, "gain", "loss");
            LossGainState(health2, "gain", "loss");
            LossGainState(health1, "gain", "loss");
        }
        else if (boxHealth == 4)
        {
            LossGainState(health4, "gain", "loss");
            LossGainState(health3, "gain", "loss");
            LossGainState(health2, "gain", "loss");
            LossGainState(health1, "gain", "loss");
        }
        else if (boxHealth == 5)
        {
            LossGainState(health5, "gain", "loss");
            LossGainState(health4, "gain", "loss");
            LossGainState(health3, "gain", "loss");
            LossGainState(health2, "gain", "loss");
            LossGainState(health1, "gain", "loss");
        }
    }

    void SetHealth()
    {
        if (boxHealth == 1)
        {
            LossGainState(health5, "loss", "gain");
            LossGainState(health4, "loss", "gain");
            LossGainState(health3, "loss", "gain");
            LossGainState(health2, "loss", "gain");
            LossGainState(health1, "gain", "loss");
        }
        if (boxHealth == 2)
        {
            LossGainState(health5, "loss", "gain");
            LossGainState(health4, "loss", "gain");
            LossGainState(health3, "loss", "gain");
            LossGainState(health2, "gain", "loss");
            LossGainState(health1, "gain", "loss");
        }
        if (boxHealth == 3)
        {
            LossGainState(health5, "loss", "gain");
            LossGainState(health4, "loss", "gain");
            LossGainState(health3, "gain", "loss");
            LossGainState(health2, "gain", "loss");
            LossGainState(health1, "gain", "loss");
        }
        if (boxHealth == 4)
        {
            LossGainState(health5, "loss", "gain");
            LossGainState(health4, "gain", "loss");
            LossGainState(health3, "gain", "loss");
            LossGainState(health2, "gain", "loss");
            LossGainState(health1, "gain", "loss");
        }
        if (boxHealth == 5)
        {
            LossGainState(health5, "gain", "loss");
            LossGainState(health4, "gain", "loss");
            LossGainState(health3, "gain", "loss");
            LossGainState(health2, "gain", "loss");
            LossGainState(health1, "gain", "loss");
        }
    }

    void LossGainState(GameObject health, string state1, string state2)
    {
        health.GetComponent<Animator>().SetBool(state1, true);
        health.GetComponent<Animator>().SetBool(state2, false);
    }
}
