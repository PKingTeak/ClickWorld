using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Newtonsoft.Json;


[Serializable]
public class ItemRowLIst<T>
{
    public List<T> items;
}

[Serializable]
public class ItemData : IItemData
{
    public int id;
    [field: SerializeField] public string ItemName { get; set; }
    [field: SerializeField] public string ItemInfo { get; set; }
    [field: SerializeField] public ItemCategory ItemType { get; set; }
    [field: SerializeField] public ObjectRank ItemRank { get; set; }
    [field: SerializeField] public Sprite ItemSprite { get; set; }
    public string spriteKey; // 어드레서블 키
    public int value;        // 수치값 (공격력 등)
}



public static class AddressableTextLoader
{
    public static async Task<List<T>> LoadJsonAsync<T>(string key)
    {
        Debug.Log($"[ItemDataLoader] 로딩 시작 키값 : {key}");
        var handle = Addressables.LoadAssetAsync<TextAsset>(key);

        TextAsset txt = await handle.Task;
        if (txt == null)
        {
            Debug.Log($"[ItemDataLoader] 데이터가 없습니다. ");
            return null;
        }


        string jsonText = txt.text;
        Addressables.Release(handle);

        try
        {
            var wrapper = JsonConvert.DeserializeObject<ItemRowLIst<T>>(jsonText);

            if (wrapper != null && wrapper.items != null)
            {
                return wrapper.items;
            }
            else
            {
                Debug.LogWarning($"[Loader] '{key}' 파싱 결과가 비어있습니다. JSON 구조가 {{ \"items\": [] }} 인지 확인하세요.");
                return new List<T>();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[Loader] JSON 파싱 에러: {ex.Message}");
            return null;

        }



    }
    public class ItemDataLoader
    {
        private async Task<string> LoadTextAsync(string key)
        {
            var handle = Addressables.LoadAssetAsync<TextAsset>(key);
            TextAsset txt = await handle.Task;

            if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded && txt != null)
            {
                string result = txt.text;
                Addressables.Release(handle);
                return result;
            }
            Debug.LogError($"[Loader] {key}로드 실패");
            Addressables.Release(handle);
            return null;
        }


        public async Task<List<IItemData>> LoadData(string key)
        {
            var handle = Addressables.LoadAssetAsync<TextAsset>(key);
            TextAsset txt = await handle.Task;

            var result = new List<IItemData>();

            if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded && txt != null)
            {
                string json = txt.text;
                Addressables.Release(handle);

                var wrapperList = JsonConvert.DeserializeObject<ItemRowLIst<ItemWrapper>>(json);

                if (wrapperList != null && wrapperList.items != null)
                {
                    foreach (var wrapper in wrapperList.items)
                    {
                        ItemData newItem = WeaponConvert(wrapper);
                        if (newItem != null)
                        {
                            result.Add(newItem);
                        }
                    }
                }
            }
            else
            {
                Debug.LogError($"[Loader] 아이템 JSON({key}) 로드 실패");
                if (handle.IsValid()) Addressables.Release(handle);
            }

            Debug.Log($"[Loader] 아이템 {result.Count}개 로드 완료");
            return result;

        }

        public async Task<List<BoxData>> LoadBoxData(string key)
        {
            List<BoxDataSheet> wrappers = await AddressableTextLoader.LoadJsonAsync<BoxDataSheet>(key);

            var result = new List<BoxData>();

            if (wrappers != null)
            {
                foreach (var row in wrappers)
                {
                    result.Add(BoxConvert(row));

                }
            }
            Debug.Log($"[ItemDataLoader]{result.Count}");
            return result;


        }


        // Wrapper 데이터를 실제 게임 데이터(ScriptableObject)로 변환
        private ItemData WeaponConvert(ItemWrapper row)
        { 
            ItemData data = new ItemData();
            data.id = row.DetailType;
            data.ItemName = row.ItemName;
            data.ItemInfo = row.ItemInfo;
            data.ItemRank = row.ItemRank;
            data.ItemType = row.ItemCategory;
            data.spriteKey = row.SpritePath;
            data.ItemSprite = null;
            return data;            
            
        }


        private BoxData BoxConvert(BoxDataSheet row)
        {
            var data = new BoxData();
            if (int.TryParse(row.boxId, out var parseID))
            {
                data.boxID = parseID;
            }
            data.boxname = row.boxname;
            data.spritePath = row.spritePath;
            data.obtainLevel = ParseIntList(row.obtainLevel);
            data.obtainChance = parseFloatList(row.obtainChance);
            data.requireClickMaxLevel = ParseIntList(row.requireClickMaxLevel);
            data.requireClickNextLevel = ParseIntList(row.requireClickNextLevel);

            data.spritePath = row.spritePath;
            data.boxsprite = null;
            return data;

        }


        private List<int> ParseIntList(string str)
        {
            var list = new List<int>();
            if (string.IsNullOrEmpty(str))
            {
                return list;
            }

            foreach (var s in str.Split('|'))
            {
                list.Add(int.Parse(s));
            }

            return list;
        }

        private List<float> parseFloatList(string str)
        {
            var list = new List<float>();
            if (string.IsNullOrEmpty(str))
            {
                return list;
            }

            foreach (var s in str.Split('|'))
            {
                if (float.TryParse(s, out float value))
                {
                    list.Add(value);
                }
            }

            return list;
        }


    }
}
