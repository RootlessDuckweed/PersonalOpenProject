using Character;
using UnityEngine;

namespace Enemy.Enemy_BringerOfDeath
{
    public class SpellCastController : MonoBehaviour
    {

        public void SetupSpellCast(CharacterStats stats)
        {
            GetComponentInChildren<SpellCastAttack>(true).SetupSpellCastAttack(stats);
        }
    }
}