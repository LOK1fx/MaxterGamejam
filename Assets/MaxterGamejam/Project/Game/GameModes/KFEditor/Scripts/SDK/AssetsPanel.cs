using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace com.LOK1game.recode
{
    public class AssetsPanel : MonoBehaviour
    {
        [SerializeField] private AssetPanel _assetCellprefab;

        private void Start()
        {
            SpawnAssets(GameControllerBase.GetGameMode<KFEditorGameMode>().kfEditorAssets);
        }

        private void SpawnAssets(List<KFEditorAsset> assets)
        {
            for (int i = 0; i < assets.Count; i++)
            {         
                var cell = Instantiate(_assetCellprefab, transform);

                StartCoroutine(LoadThumbnail(cell, assets[i]));
            }
        }

        private IEnumerator LoadThumbnail(AssetPanel cell, KFEditorAsset asset)
        {
            yield return new WaitForSeconds(0.3f);

            cell.thumbnail.sprite = asset.thumbnail;
            cell.assetName = asset.name;          
        }
    }
}