using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


# if UNITY_EDITOR
public class StationsDataReader : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string str in importedAssets)
        {
            const string file_name = "stations_data_JR山手線.csv";

            if (str.IndexOf("Assets/fieldData/" + file_name) != -1)
            {
                Debug.Log($"Start reading '{file_name}'.");

                TextAsset textasset = AssetDatabase.LoadAssetAtPath<TextAsset>(str);

                string assetfile = str.Replace(".csv", ".asset");
                StationsDataBase sd = AssetDatabase.LoadAssetAtPath<StationsDataBase>(assetfile);
                if (sd == null)
                {
                    sd = ScriptableObject.CreateInstance<StationsDataBase>();
                    AssetDatabase.CreateAsset(sd, assetfile);
                }

                sd.data = CSVSerializer.Deserialize<StationsData>(textasset.text);
                EditorUtility.SetDirty(sd);

                Debug.Log($"Finish reading '{file_name}'.");
            }
        }

        AssetDatabase.SaveAssets();
    }
}
# endif
