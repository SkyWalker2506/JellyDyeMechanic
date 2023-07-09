using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _colorButtonPrefab;
    [SerializeField] private Transform _buttonHolder;
    [SerializeField] private Button _resetButton;
    public Action<Color> OnColorSelected;
    public Action OnResetButton;

    private void OnEnable()
    {
        _resetButton.onClick.AddListener(() => OnResetButton?.Invoke());
    }

    private void OnDisable()
    {
        _resetButton.onClick.RemoveListener(() => OnResetButton?.Invoke());
    }

    public void CreateButtons(Color[] colors)
    {
        foreach (var child in _buttonHolder.GetComponentsInChildren<Transform>())
        {
            if (child != _buttonHolder)
            {
                Destroy(child);
            }
        }
        
        for (int i = 0; i < colors.Length; i++)
        {
            Button button = Instantiate(_colorButtonPrefab, _buttonHolder);
            Color color = colors[i];
            button.GetComponent<Image>().color = color;
            button.onClick.AddListener(() => { OnColorSelected?.Invoke(color); } );
        }                
    }
    
    
}