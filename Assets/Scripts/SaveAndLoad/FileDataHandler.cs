using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace SaveAndLoad
{
    public class FileDataHandler
    {
        private string dataDirPath;
        private string dataFileName;

        public FileDataHandler(string dataDirPath,string dataFileName)
        {
            this.dataDirPath = dataDirPath;
            this.dataFileName = dataFileName;
        }

        public void Save(GameData data)
        {
            string fullPath = Path.Combine(dataDirPath, dataFileName);
            try
            {
                if (!Directory.Exists(dataDirPath))
                    Directory.CreateDirectory(dataDirPath);
                string dataToStore = JsonConvert.SerializeObject(data, Formatting.Indented);
                Debug.Log(dataToStore);
                using (FileStream stream = new FileStream(fullPath,FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataToStore);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("Error");
            }
        }

        public GameData Load()
        {
            string fullPath = Path.Combine(dataDirPath, dataFileName);
            GameData loadData=null;
            if (File.Exists(fullPath))
            {
                try
                {
                    string dataToLoad;
                    using (FileStream stream = new FileStream(fullPath,FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }
                    loadData = JsonConvert.DeserializeObject<GameData>(dataToLoad);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return loadData;
        }

        public void DeleteTheData()
        {
            string fullPath = Path.Combine(dataDirPath, dataFileName);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

    }
}