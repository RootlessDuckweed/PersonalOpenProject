using TMPro;
using UnityEngine;

namespace Player.Skill.SpecificSkills
{
    public class BlackHoleHotKeyController : MonoBehaviour
    {
        //private SpriteRenderer _sr;
        private KeyCode _myHotKey;
        private TextMeshProUGUI _myText;
        private Enemy.Enemy _enemy;
        private BlackHoleSkillController _blackHole;

        private void Awake()
        {
            
        }

        public void SetupHotKey(KeyCode key,Enemy.Enemy enemy,BlackHoleSkillController blackHole)
        {
            //_sr = GetComponent<SpriteRenderer>();
            _myText = GetComponentInChildren<TextMeshProUGUI>();
            _myText.text = key.ToString();
            _myHotKey = key;
            _enemy = enemy;
            _blackHole = blackHole;
        }
        
        private void CloseTheHotKey()
        {
            _myText.color = Color.clear;
        }
        private void Update()
        {
            if (Input.GetKeyDown(_myHotKey))
            {
                _blackHole.AddEnemyToList(_enemy);
                CloseTheHotKey();
                Destroy(gameObject);
            }
        }
    }
}