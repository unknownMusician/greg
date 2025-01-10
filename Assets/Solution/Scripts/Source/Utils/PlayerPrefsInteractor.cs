using AreYouFruits.Nullability;
using UnityEngine;

namespace Greg.Utils
{
    public static class PlayerPrefsInteractor
    {
        public static void Save<TPersistentData>(TPersistentData persistentData)
        {
            var json = JsonUtility.ToJson(persistentData);
            PlayerPrefs.SetString(typeof(TPersistentData).Name, json);
        }

        public static Optional<TPersistentData> Load<TPersistentData>()
        {
            var key = typeof(TPersistentData).Name;
            
            if (!PlayerPrefs.HasKey(key))
            {
                return Optional.None();
            }
            
            var dataJson = PlayerPrefs.GetString(key);
            var persistentData = JsonUtility.FromJson<TPersistentData>(dataJson);
            return persistentData;
        }
    }
}