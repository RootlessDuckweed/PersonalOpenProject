using UnityEditor.Build.Content;
using UnityEngine;

namespace UI
{
    public class UI : MonoBehaviour
    {
        public void SwitchTo(GameObject menu)
        {
            for (int i = 3; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            if (menu != null)
            {
                menu.SetActive(true);
            }
        }
    }
}