using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIAmmo : MonoBehaviour
{
    [SerializeField]
    private TMP_Text ammoText = null;
    public void UpdateBulletText(int bulletCount)
    {
        if (bulletCount == 0)
        {
            ammoText.color = Color.red;
        }
        else
        {
            ammoText.color = Color.white;
        }
        ammoText.SetText(bulletCount.ToString());
    }
}
