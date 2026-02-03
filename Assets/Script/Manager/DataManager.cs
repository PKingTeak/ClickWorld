using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static AddressableTextLoader;




public class DataManager : MonoSingleton<DataManager>
{

    private ItemDataLoader _loader = new ItemDataLoader();
    public WeaponDataBase WeaponDB { get; private set; } = new WeaponDataBase();
    public BoxDataBase BoxDB { get; private set; } = new BoxDataBase();
    private Dictionary<string, IItemData> _allItemDic = new Dictionary<string, IItemData>();

    private const string ITEM_DATA_KEY = "Item_Data";
    private const string BOX_DATA_KEY = "Box_Data";



    protected override void Awake()
    {
        base.Awake();
        // 게임 시작 시 비동기 로딩 시작
        _ = LoadAllData();
    }

    private async Task LoadAllData()
    {
        List<IItemData> loadedItems = await _loader.LoadData(ITEM_DATA_KEY);

        if (loadedItems != null)
        { 
        foreach (var item in loadedItems)
            {
                ItemData newItem = (ItemData)item;
                if (newItem != null && !string.IsNullOrEmpty(newItem.spriteKey))
                {
                    newItem.ItemSprite = await LoadSpriteAsync(newItem.spriteKey);
                }
                
            }

            WeaponDB.InitData(loadedItems);
            Debug.Log($"[DataManager] 아이템 {loadedItems.Count}개 등록 완료.");
        }

        //박스데이터 로드 
        List<BoxData> loadedBoxes = await _loader.LoadBoxData(BOX_DATA_KEY);

        if (loadedBoxes != null)
        {
            foreach (var box in loadedBoxes)
            {
                if (!string.IsNullOrEmpty(box.spritePath))
                {
                    box.boxsprite = await LoadSpriteAsync(box.spritePath);
                }
            }
        }

        BoxDB.Init(loadedBoxes);
        Debug.Log($"[DataManager] 상자 {loadedBoxes.Count}개 등록 완료.");
    }



    private async Task<Sprite> LoadSpriteAsync(string key)
    {
        var handle = Addressables.LoadAssetAsync<Sprite>(key);
        Sprite sprite = await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            return sprite;
        }
        else 
        {
            Debug.LogWarning($"[DataManager] 이미지 로드 실패: {key}");
            return null;
        }
        
    }

    public IItemData GetItem(string name) //인벤토리에는 어쩌피 아이템 데이터가 들어가니까 
    {
        if (_allItemDic.TryGetValue(name, out var item))
        {
            return item;
        }
        return null;
    }
}