using System;
using System.Collections.Generic;
using UI.UI_InventorySlot;

namespace SaveAndLoad
{
    [Serializable]
    public class GameData
    {
        public int currency = 0;
        public Dictionary<string, int> inventory = new();
        public List<string> equipmentID = new();
        public KeyValuePair<string, int> flaskSlot;
        public Dictionary<string, bool> skillTree = new();
        public Dictionary<string, bool> checkPoints = new();
        public string closestPointID = "";
    }
}