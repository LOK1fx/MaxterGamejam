using UnityEngine;
using System.Collections;

namespace com.LOK1game.recode.Architecture
{
    public class SceneManagerExample : SceneManagerBase
    {
        public override void InitScenesMap()
        {
            sceneConfigMap[TestMapConfig.SCENE_NAME] = new TestMapConfig();
        }
    }
}