using System;
using System.Collections.Generic;
using Player.Universal;
using TMPro;
using UnityEngine;

namespace UI
{
    public class StatUI : MonoBehaviour
    {
        [SerializeField]private List<TextMeshProUGUI> statTexts;

        public void UpdateValueUI()
        {
            for (int i = 0; i < statTexts.Count; i++)
            {
                statTexts[i].text = PlayerManager.Instance.Player.stats.GetStatString(i);
            }
        }
    }
}