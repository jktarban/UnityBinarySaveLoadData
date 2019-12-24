namespace Infra.SaveLoad
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using UnityEngine;

    public class SaveLoadDataManager: ISaveLoadDataManager
    {
        private Dictionary<EnumSaveLoadData, object>  _playerData = new Dictionary<EnumSaveLoadData, object>();
        
        public static ISaveLoadDataManager Instance { get; private set; }

        public static void Set(ISaveLoadDataManager instance)
        {
            Instance = instance;
        }
        
        //save data with generic type
        public void SaveObject<T> (EnumSaveLoadData key, T value)
        {
            SetData (key, value);
        }

        //load data with generic type
        public T LoadObject<T> (EnumSaveLoadData key)
        {
            var data = (T)GetData (key);
            return (T)Convert.ChangeType (data, typeof(T));
        }

        private void SetData (EnumSaveLoadData prefName, object prefObject)
        {
            LoadDataBinary ();
            
            if (_playerData.ContainsKey (prefName)) 
            {
                _playerData [prefName] = prefObject;
            } else 
            {
                _playerData.Add (prefName, prefObject);
            }
            
            SaveDataBinary ();

        }

        private object GetData (EnumSaveLoadData prefName)
        {
            LoadDataBinary ();
            object prefObject = null;

            if (_playerData.ContainsKey (prefName)) 
            {
                prefObject = _playerData [prefName];
            }
            
            return prefObject;
        }

        private void SaveDataBinary ()
        {
            var bf = new BinaryFormatter ();
            var file = File.Create (Application.persistentDataPath + "/PlayerData.dat");
            
            var data = new PlayerData
            {
                Data = _playerData
            };
            
            bf.Serialize (file, data);
            file.Close ();
        }

        private void LoadDataBinary ()
        {
            if (!File.Exists(Application.persistentDataPath + "/PlayerData.dat"))
            {
                return;
            }
            
            var bf = new BinaryFormatter ();
            var file = File.Open (Application.persistentDataPath + "/PlayerData.dat", FileMode.Open);
            var data = (PlayerData)bf.Deserialize (file);
            _playerData = data.Data;
            file.Close ();
        }
    }

    [Serializable]
    internal class PlayerData
    {
        public Dictionary<EnumSaveLoadData, object> Data;
    }
}


