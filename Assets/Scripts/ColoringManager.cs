using System;
using System.Collections.Generic;
using UnityEngine;

public class ColoringManager : MonoBehaviour
{
    [SerializeField] private UIManager _UIManager;
    [SerializeField] private Color[] _colors;
    public static ColoringManager Instance { get; private set; }

    private List<IColoringObject> _coloringObjectList = new List<IColoringObject>();
    private Camera _camera;
    private Color _selectedColor = Color.clear;
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _camera = Camera.main;
    }

    private void Start()
    {
        _UIManager.CreateButtons(_colors);
    }

    private void OnEnable()
    {
        _UIManager.OnResetButton += ResetAllColoringObjects;
        _UIManager.OnColorSelected += SetColor;
    }

    private void OnDisable()
    {
        _UIManager.OnResetButton -= ResetAllColoringObjects;
        _UIManager.OnColorSelected -= SetColor;
    }

    private void SetColor(Color color)
    {
        _selectedColor = color;
    }
    
    public void AddColoringObject(IColoringObject coloringObject)
    {
        _coloringObjectList.Add(coloringObject);
    }
    
    private void Update()
    {
        if (_selectedColor == Color.clear)
        {
            return;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                if (hit.transform.TryGetComponent(out IColoringObject coloringObject))
                {
                    coloringObject.StartColoring(hit.textureCoord, _selectedColor);
                }
            }
        }
    }

    public void ResetAllColoringObjects()
    {
        foreach (IColoringObject coloringObject in _coloringObjectList)
        {
            coloringObject.ResetColoring();
        }
    }
}
