using System;
using System.Collections.Generic;
using System.Linq;
using Inventory;
using Player.Universal;
using UnityEngine;
using Utility;

namespace SaveAndLoad
{
    public class SaveManager : Singleton<SaveManager>
    {
        private GameData gameData;
        private List<ISaveManager> saveManagers;
        private FileDataHandler dataHandler;
        [SerializeField] private string fileName;

        
        private void Start()
        {
            dataHandler = new FileDataHandler(Application.streamingAssetsPath, fileName);
            saveManagers = FindAllSaveManagers();
            LoadGame();
        }

        public void NewGame()
        {
            gameData = new GameData();
        }

        public void LoadGame()
        {
            // gameData = data from data handler
            gameData = dataHandler.Load();
            if (gameData == null)
            {
                print("No saved data found");
                NewGame();
            }
            else
            {
                foreach (var t in saveManagers)
                {
                    t.LoadData(gameData);
                }
                
            }
        }

        public void SaveGame()
        {
            // data handler save gameData
            foreach (var t in saveManagers)
            {
                t.SaveData(ref gameData);
            }
            dataHandler.Save(gameData);
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        private List<ISaveManager> FindAllSaveManagers()
        {
            IEnumerable<ISaveManager> saveManagers =
                FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                    .OfType<ISaveManager>();
            return new List<ISaveManager>(saveManagers);
        }
        

        public void DeleteTheData()
        {
            dataHandler.DeleteTheData();
        }

        public bool HasSavedData()
        {
            return dataHandler.Load() != null;
        }
    }
}