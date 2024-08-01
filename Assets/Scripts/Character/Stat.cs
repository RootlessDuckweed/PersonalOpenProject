using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] private float baseValue;
        public List<float> modifiers;

        public float GetValue()
        {
            if (modifiers.Count <= 0) return baseValue;
            float finalValue = baseValue;
            foreach (var value in modifiers)
            {
                finalValue += value;
            }

            return finalValue;
        }

        public void SetDefaultValue(float value)
        {
            baseValue = value;
        }

        public void AddModifier(float modifier)
        {
            modifiers.Add(modifier);
        }
        
        public void RemoveModifier(float modifier)
        {
            modifiers.Remove(modifier);
        }
        
    }
}