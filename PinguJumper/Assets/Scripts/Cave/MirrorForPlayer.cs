using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorForPlayer : MonoBehaviour
{
    private int mirrorCount;
    [SerializeField]private List<GameObject> playerMirrors;

    public void RequestMirror()
    {
        if (mirrorCount > 0)
        {
            mirrorCount--;
            UpdateMirrorCountOnPlayer();
        }
    }

    public void AddMirror()
    {
        mirrorCount++;
        UpdateMirrorCountOnPlayer();
    }

    private void UpdateMirrorCountOnPlayer()
    {
        for (int i = 0; i < playerMirrors.Count; i++)
        {
            playerMirrors[i].SetActive(mirrorCount - i > 0 ? true : false);
        }
    }

    public bool hasMirror()
    {
        return mirrorCount != 0;
    }
}
