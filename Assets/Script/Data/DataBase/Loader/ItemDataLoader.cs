using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;



using UnityEngine.ResourceManagement.AsyncOperations;


[Serializable]
public class ItemRowLIst<T>
{
    public List<T> items;
}


public static class AddressableTextLoader
{
    public static async Task<string> LoadJsonAsync(string key)
    {
        AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(key);

        TextAsset txt = await handle.Task;
        Addressables.Release(handle);

        return null;
       // return JsonConvert.DeserializeObject<List<T>>(txt.text);



    }
}
public class ItemDataLoader
{
    public async Task<List<IItemData>> LoadData(string path)
    {
        var textAsset = Resources.Load<TextAsset>(path);
        var json = textAsset.text;


        var wrapper = JsonUtility.FromJson<ItemRowLIst<WeaponWrapper>>(json);

        var result = new List<IItemData>();
        foreach (var row in wrapper.items)
        {
            result.Add(Convert(row));
        }

        return result;

    }

    private WeaponData Convert(WeaponWrapper row)
    {
        var data = ScriptableObject.CreateInstance<WeaponData>();

        var type = (WeaponType)row.weaponType;
        var rank = (ObjectRank)row.weaponRank;
        var sprite = string.IsNullOrEmpty(row.spritePath) ? null : Resources.Load<Sprite>(row.spritePath);


        data.Init(row.weaponName, row.weaponInfo, type, rank, sprite);
        return data;

    }




}
