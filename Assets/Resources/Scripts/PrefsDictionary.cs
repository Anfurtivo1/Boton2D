using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class PrefsDictionary
{
    public static void SaveDictionary(string key, Dictionary<int, int> dict)
    {
        // Lo convertimos a JSON manualmente
        var data = new DictionaryData(dict);
        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();

        Debug.Log($"[PrefsDictionary] Diccionario guardado con clave '{key}'.");
        Debug.Log($"[PrefsDictionary] Contenido JSON:\n{json}");
    }

    public static Dictionary<int, int> LoadDictionary(string key)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            Debug.LogWarning($"[PrefsDictionary] No existe ninguna entrada con la clave '{key}'. Se devuelve un diccionario vacío.");
            return new Dictionary<int, int>();
        }

        string json = PlayerPrefs.GetString(key);
        Debug.Log($"[PrefsDictionary] Cargando diccionario con clave '{key}'.");
        Debug.Log($"[PrefsDictionary] Contenido JSON cargado:\n{json}");

        var data = JsonUtility.FromJson<DictionaryData>(json);
        Dictionary<int, int> dict = data?.ToDictionary() ?? new Dictionary<int, int>();

        Debug.Log($"[PrefsDictionary] Diccionario reconstruido con {dict.Count} entradas.");
        foreach (var kvp in dict)
        {
            Debug.Log($"  ID {kvp.Key} → {kvp.Value}");
        }

        return dict;
    }

    [System.Serializable]
    private class DictionaryData
    {
        public List<int> keys = new List<int>();
        public List<int> values = new List<int>();

        public DictionaryData(Dictionary<int, int> dict)
        {
            keys = dict.Keys.ToList();
            values = dict.Values.ToList();
        }

        public Dictionary<int, int> ToDictionary()
        {
            var dict = new Dictionary<int, int>();
            for (int i = 0; i < keys.Count; i++)
            {
                dict[keys[i]] = values[i];
            }
            return dict;
        }

        public DictionaryData() { }
    }
}
