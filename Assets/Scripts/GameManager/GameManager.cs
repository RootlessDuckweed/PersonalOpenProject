using System;
using System.Collections.Generic;
using Player.Universal;
using SaveAndLoad;
using UnityEngine;
using Utility;

namespace GameManager
{
    public class GameManager : Singleton<GameManager>,ISaveManager
    {
        private CheckPoint[] checkPoints;
        protected override void Awake()
        {
            base.Awake();
            checkPoints = FindObjectsByType<CheckPoint>(FindObjectsSortMode.None);
        }

        public void LoadData(GameData data)
        {
            foreach (var t in data.checkPoints)
            {
                foreach (var point in this.checkPoints)
                {
                    if (t.Key == point.checkPointID && t.Value)
                    {
                        point.Activated();
                        if(string.IsNullOrEmpty(data.closestPointID)) return;
                        if (point.checkPointID == data.closestPointID)
                        {
                            PlayerManager.Instance.Player.transform.position = point.transform.position;
                        }
                    }
                }
            }
        }

        public void SaveData(ref GameData data)
        {
            data.closestPointID = FindClosestCheckPoint()?.checkPointID;
            data.checkPoints.Clear();
            foreach (var t in checkPoints)
            {
                data.checkPoints.Add(t.checkPointID,t.isActivated);
            }
        }

        private CheckPoint FindClosestCheckPoint()
        {
            float closestDistance = Mathf.Infinity;
            CheckPoint closestPoint = null;
            foreach (var p in checkPoints)
            {
                float distanceToCheckPoint =
                    Vector2.Distance(PlayerManager.Instance.Player.transform.position, p.transform.position);
                if (distanceToCheckPoint < closestDistance && p.isActivated)
                {
                    closestDistance = distanceToCheckPoint;
                    closestPoint = p;
                }
            }

            return closestPoint;
        }

        public void PauseGame(bool pause)
        {
            if (pause)
            {
                Time.timeScale = 0f;
                PlayerManager.Instance.Player.playerInput.Disable();
            }
            else
            {
                Time.timeScale = 1f;
                PlayerManager.Instance.Player.playerInput.Enable();
            }
        }
    }
}