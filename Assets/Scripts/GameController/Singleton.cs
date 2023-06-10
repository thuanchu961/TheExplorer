using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<Trung> : MonoBehaviour where Trung : MonoBehaviour
{
    static Trung inst_singleton;
    public static Trung _inst_singleton
    {
        get
        {
            if (inst_singleton == null)
            {
                inst_singleton = FindObjectOfType<Trung>();
            }
            if (inst_singleton == null)
            {
                GameObject g = new GameObject("singleton");
                g.AddComponent<Trung>();
                inst_singleton = g.GetComponent<Trung>();
            }
            else
            {
                Trung[] tmts = FindObjectsOfType<Trung>();
                if (tmts.Length == 1)
                    inst_singleton = tmts[0];
                if (tmts.Length > 1)
                {
                    inst_singleton = tmts[tmts.Length - 1];
                    for (int i = 0; i < tmts.Length - 1; i++)
                    {
                        Destroy(tmts[i].gameObject);
                    }
                }
            }
            return inst_singleton;
        }
    }
}
