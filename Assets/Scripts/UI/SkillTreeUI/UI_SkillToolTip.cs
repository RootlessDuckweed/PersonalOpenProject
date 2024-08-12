using TMPro;
using UnityEngine;

namespace UI.SkillTreeUI
{
    public class UI_SkillToolTip : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI skillText;
        [SerializeField] private TextMeshProUGUI skillName;

        public void ShowToolTip(string text,string titleName)
        {
            skillText.text = text;
            skillName.text = titleName;
        }

        public void HideToolTip()
        {
            skillText.text = "Description area!";
            skillName.text = "Skill Name";
        }
    }
}