using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Newtonsoft.Json;
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
            List<WeaponWrapper> wrappers = await AddressableTextLoader.LoadJsonAsync<WeaponWrapper>(key);

            var result = new List<IItemData>();

            // 2. 데이터 변환 (Wrapper -> ScriptableObject)
            if (wrappers != null)
            {
                foreach (var row in wrappers)
                {
                    result.Add(Convert(row));
                }
            }
            return result;

        }

        // Wrapper 데이터를 실제 게임 데이터(ScriptableObject)로 변환
        private WeaponData Convert(WeaponWrapper row)
        {
            var data = ScriptableObject.CreateInstance<WeaponData>();

            // Enum 변환 (숫자 -> Enum)
            var type = (WeaponType)row.weaponType;
            var rank = (ObjectRank)row.weaponRank;

            // 이미지 로드 (경로가 있을 때만)
            Sprite sprite = null;
            if (!string.IsNullOrEmpty(row.spritePath))
            {
                sprite = Resources.Load<Sprite>(row.spritePath);
            }

            data.Init(row.weaponName, row.weaponInfo, type, rank, sprite);
            return data;
        }



    }
}
