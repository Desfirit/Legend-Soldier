using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "LevelsData", menuName = "LevelData", order = 1)]
public class LevelsDefinition : ScriptableObject
{
    [System.Serializable]
    public class Level
    {
        public string levelName;
        public Sprite levelImage; 
    }

    public Level[] levels;

}
