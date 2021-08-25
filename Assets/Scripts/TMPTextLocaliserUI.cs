using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TMPTextLocaliserUI : MonoBehaviour
{
    private TMP_Text textField;

    [SerializeField] private string _key;

    private void Start()
    {
        if(_key == null && textField.text != null)
        {
            Localise(textField.text);
        }
        else
        {
            Localise(_key);
        }   
    }

    public void Localise(string key)
    {
        if (!string.IsNullOrEmpty(key))
        {
            textField = GetComponent<TMP_Text>();

            string value = LocalisationSystem.GetLocalisedValue(key);

            textField.text = value;
        }
    }

#if UNITY_EDITOR

    [ContextMenu("Update Text")]
    public void EditorUpdate()
    {
        Localise(_key);
    }

#endif
}
