using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static AddressableTextLoader;




public class DataManager : MonoSingleton<DataManager>
{
    public WeaponDataBase WeaponDB { get; private set; } = new WeaponDataBase();
    public BoxDataBase BoxDB { get; private set; } = new BoxDataBase();
    private Dictionary<string, IItemData> _allItemDic = new Dictionary<string, IItemData>();
    

    private ItemDataLoader loader = new ItemDataLoader();
    

    protected override void Awake() 
    {
        base.Awake();
        // 게임 시작 시 비동기 로딩 시작
        _ = InitDataAsync();
    }

    private async Task InitDataAsync()
    {
        Debug.Log($"[DataManager] 로딩 완료");

        List<IItemData> loadedItems = await loader.LoadData("Item_Data");
        List<BoxData> loadedBoxs = await loader.LoadBoxData("Box_Data");
        
        Debug.Log($"[DataManager] 로딩 완료");

        List<WeaponData> weaponOnlyList = new List<WeaponData>();
        List<BoxData> BoxDataList = new List<BoxData>();
        
        if (loadedItems != null)
        {
            foreach (var item in loadedItems)
            {

                if (item is WeaponData weapon)
                {

                    weaponOnlyList.Add(weapon);
                    if (!_allItemDic.ContainsKey(weapon.name))
                    {
                        _allItemDic.Add(weapon.name, weapon);
                    }
                }
            }
        }

        if (loadedBoxs != null)
        { 
            foreach (var item in loadedBoxs)
            {
                if (item is BoxData box)
                {
                    BoxDataList.Add(box);
                    
                }
            }
        }
         
             
        WeaponDB.InitData(weaponOnlyList); //무기는 무기들만 
        BoxDB.Init(BoxDataList);
        Debug.Log($"[DataManager] 데이터 초기화 완료! 로드된 무기 개수: {weaponOnlyList.Count}");
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