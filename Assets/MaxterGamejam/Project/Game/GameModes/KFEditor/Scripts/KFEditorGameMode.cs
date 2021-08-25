using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace com.LOK1game.recode
{
    [System.Serializable]
    public class KFEditorAsset
    {
        public string name;
        public KFEditorObject prefab;
        public Sprite thumbnail;

        public KFEditorObject.Data data;
    }


    /// <summary>
    /// Редактор карт
    /// </summary>
    public class KFEditorGameMode : GameModeBase
    {
        public static KFEditorGameMode editorGameMode;

        public int selectedAssetIndex;

        public int playerStartsInScene;

        public List<KFEditorAsset> kfEditorAssets;
        public List<KFEditorObject> kfEditorAssetsInScene;

        public override void Init()
        {
            base.Init();

            editorGameMode = this;
        }

        protected override GameObject SpawnPlayer()
        {
            return Instantiate(player);
        }

        public void PlaceAsset(KFEditorAsset asset, Vector3 position)
        {
            var n_asset = Instantiate(asset.prefab, position, Quaternion.identity);

            n_asset.data.position = n_asset.transform.position;
            n_asset.data.rotation = n_asset.transform.rotation;
            n_asset.data.scale = n_asset.transform.localScale;

            kfEditorAssetsInScene.Add(n_asset);
        }

        [ContextMenu("Save Level")]
        public void SaveLevel()
        {
            string json = JsonUtility.ToJson(kfEditorAssetsInScene, true);

            string folder = $"C:/LOK1game.project/KEK-FIREproject/saves/levels/";
            string levelFile = "new_level.json";

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string path = Path.Combine(folder, levelFile);

            if (File.Exists(path))
                File.Delete(path);
            File.WriteAllText(path, json);
        }
    }
}