using Character;
using Utility;

namespace Enemy.Enemy_BringerOfDeath
{
    public class SpellCastAttack : Attack_Base
    {
        public void SetupSpellCastAttack(CharacterStats stats)
        {
            stat = stats;
        }
    }
}