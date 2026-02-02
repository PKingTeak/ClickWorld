using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Newtonsoft.Json;
using TMPro;
using Unity.VisualScripting;



[Serializable]
public class ItemRowLIst<T>
{
    public List<T> items;
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
        public async Task<List<IItemData>> LoadData(string key)
        {
            // 1. JSON 로드 (위의 깔끔해진 함수 사용)
            List<ItemWrapper> wrappers = await AddressableTextLoader.LoadJsonAsync<ItemWrapper>(key);

            var result = new List<IItemData>();

            // 2. 데이터 변환 (Wrapper -> ScriptableObject)
            if (wrappers != null)
            {
                foreach (var row in wrappers)
                {
                    switch (row.ItemCategory)
                    {
                        case ItemCategory.WeaponItem:
                            result.Add(WeaponConvert(row));
                            //무기 정보를 넘겨 줘야함
                            break;

                        case ItemCategory.PetItem:
                            //아직 미구현
                            break;
                    }
                }
            }
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
        private WeaponData WeaponConvert(ItemWrapper row)
        {
            var data = ScriptableObject.CreateInstance<WeaponData>();

            var rank = row.ItemRank;

            WeaponType type = (WeaponType)row.DetailType;

            // 이미지 로드 (경로가 있을 때만)
            Sprite sprite = null;
            if (!string.IsNullOrEmpty(row.SpritePath))
            {
                sprite = Resources.Load<Sprite>(row.SpritePath);
            }

            data.Init(row.ItemName, row.ItemInfo, type, rank, sprite);
            return data;
        }


        private BoxData BoxConvert(BoxDataSheet row)
        {
            var data = new BoxData();

            List<int> level = new List<int>();
            List<float> chance = new List<float>();

            string[] words = row.obtainLevel.Split('|');
            for (int i = 0; i < words.Length; i++)
            {
                level.Add(int.Parse(words[i]));
            }

            words = row.obtainChance.Split('|');
            for (int i = 0; i < words.Length; i++)
            {
                chance.Add(int.Parse(words[i]));

            }

        //    data.initData(row.boxId,row.boxname, level, chance, int.Parse(row.obtainLevel), int.Parse(row.obtainChance));

            return data;
        }



    }
}
