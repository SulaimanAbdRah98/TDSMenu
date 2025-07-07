using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TDS.UI.Visual.Settings
{
    [ExecuteInEditMode]
    public class SettingsDesigner : MonoBehaviour
    {
        protected enum SettingType
        {
            TOGGLE, DROPDOWN, CUSTOM
        }

        [System.Serializable]
        protected class Setting
        {
            public string Name;
            public SettingType Type;
            public GameObject Instance;

            public GameObject CustomPrefab;

            [HideInInspector] public bool Initiated;
        }

        [SerializeField] private SettingsPrefabSO prefabs;
        [SerializeField] private List<Setting> settings = new List<Setting>();
        [SerializeField] private RectTransform root;

        private void SetInstance(Setting setting)
        {
            if(setting.Instance != null)
            {
                DestroyImmediate(setting.Instance);
            }

            switch(setting.Type)
            {
                case SettingType.TOGGLE:
                    setting.Instance = (GameObject)PrefabUtility.InstantiatePrefab(prefabs.TogglePrefab, root);
                    break;
                case SettingType.DROPDOWN:
                    setting.Instance = (GameObject)PrefabUtility.InstantiatePrefab(prefabs.DropdownPrefab, root);
                    break;
                case SettingType.CUSTOM:
                    setting.Instance = (GameObject)PrefabUtility.InstantiatePrefab(setting.CustomPrefab, root);
                    break;
            }

            setting.Instance.transform.localPosition = Vector2.zero;
        }

        public void Refresh()
        {
            for (int i = settings.Count - 1; i >= 0; i--)
            {
                //If it's null and hasn't been initiated that means this is new
                if (settings[i].Instance == null)
                {
                    //But if it's initiated before that means this setting was deleted,
                    //so we should remove this from our list
                    if (settings[i].Initiated)
                    {
                        Undo.RecordObject(this, "Delete unused setting slot");
                        settings.RemoveAt(i);
                    }
                    else
                    {
                        settings[i].Initiated = true;
                        SetInstance(settings[i]);
                    }
                }
                //If it's not null, let's update it
                else
                {
                    SetInstance(settings[i]);
                }
            }
        }
    }
}