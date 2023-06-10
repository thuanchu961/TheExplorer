using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    List<GameObject> playerBullets = new List<GameObject>();
    List<GameObject> playerBulletImpacts = new List<GameObject>();
    List<GameObject> bossBeams = new List<GameObject>();
    List<GameObject> bossBeamEffects = new List<GameObject>();

    public GameObject TMT_GetPlayerBullet(GameObject bullet)
    {
        foreach (var i in playerBullets)
        {
            if (i.activeSelf)
                continue;
            return i;
        }

        GameObject g = Instantiate(bullet, transform.position, Quaternion.identity);
        playerBullets.Add(g);
        return g;
    }

    public GameObject TMT_GetPlayerBulletImpact(GameObject bulletImpact)
    {
        foreach (var i in playerBulletImpacts)
        {
            if (i.activeSelf)
                continue;
            return i;
        }

        GameObject g = Instantiate(bulletImpact, transform.position, Quaternion.identity);
        playerBulletImpacts.Add(g);
        return g;
    }

    public GameObject TMT_GetBossBeam(GameObject beam)
    {
        foreach (var i in bossBeams)
        {
            if (i.activeSelf)
                continue;
            return i;
        }

        GameObject g = Instantiate(beam, transform.position, Quaternion.identity);
        bossBeams.Add(g);
        return g;
    }

    public GameObject TMT_GetBossBeamEffect(GameObject effect)
    {
        foreach (var i in bossBeamEffects)
        {
            if (i.activeSelf)
                continue;
            return i;
        }

        GameObject g = Instantiate(effect, transform.position, Quaternion.identity);
        bossBeamEffects.Add(g);
        return g;
    }

    public List<GameObject> TMT_GetActiveBullet()
    {
        List<GameObject> l = new List<GameObject>();
        foreach (var i in bossBeams)
        {
            if (i.activeSelf)
                l.Add(i);
        }
        return l;
    }

    public List<GameObject> TMT_GetActivePlayerBullet()
    {
        List<GameObject> l = new List<GameObject>();
        foreach (var i in playerBullets)
        {
            if (i.activeSelf)
                l.Add(i);
        }
        return l;
    }
}
