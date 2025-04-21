using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    private string GetFilePath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName + ".json");
    }

    // Salva os dados no arquivo
    public void SaveData<T>(Dictionary<string, T> data, string fileName)
    {
        Debug.Log($"Saving {fileName} data...");
        string json = JsonUtility.ToJson(new ListWrapper<T>(data), true);
        File.WriteAllText(GetFilePath(fileName), json);
    }

    // Carrega os dados do arquivo
    public Dictionary<string, T> LoadData<T>(string fileName)
    {
        string filePath = GetFilePath(fileName);

        if (File.Exists(filePath))
        {
            Debug.Log($"Loading {fileName} data...");
            string json = File.ReadAllText(filePath);
            ListWrapper<T> wrapper = JsonUtility.FromJson<ListWrapper<T>>(json);
            return wrapper.ToDictionary();
        }
        return new Dictionary<string, T>();
    }

    public void ResetData(string fileName)
    {
        Debug.Log($"Resetting {fileName} data...");
        SaveData(new Dictionary<string, object>(), fileName);
    }

    [System.Serializable]
    private class ListWrapper<T>
    {
        public List<T> Items;

        public ListWrapper(Dictionary<string, T> data)
        {
            Items = new List<T>(data.Values);
        }

        public Dictionary<string, T> ToDictionary()
        {
            Dictionary<string, T> dictionary = new Dictionary<string, T>();
            foreach (var item in Items)
            {
                if (item is IIdentifiable identifiable)
                {
                    dictionary[identifiable.Id] = item;
                }
                else
                {
                    Debug.LogWarning("Item does not implement IIdentifiable. Skipping...");
                }
            }
            return dictionary;
        }
    }
}

public interface IIdentifiable
{
    string Id { get; }
}
