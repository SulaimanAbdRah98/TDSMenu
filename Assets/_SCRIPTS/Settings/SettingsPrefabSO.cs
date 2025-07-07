using UnityEngine;

namespace TDS.UI.Visual.Settings
{
    [CreateAssetMenu(fileName = "settingsprefab_", menuName = "Data/UI/Settings/Prefabs")]
    public class SettingsPrefabSO : ScriptableObject
    {
        public GameObject TogglePrefab;
        public GameObject DropdownPrefab;
    }
}