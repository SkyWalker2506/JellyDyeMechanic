using System;
using UnityEngine;

public class Coloring : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Material _coloringMaterial;
    [SerializeField] private int _maxIndex = 23;
    [SerializeField] private float _coloringSpeed = 1;
    private Material _coloringMaterialInstance;
    private float _currentIndex;
    private Vector2 _paintPoint;
    private bool _doPaint;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _coloringMaterialInstance = Instantiate(_coloringMaterial);
        _renderer.material = _coloringMaterialInstance;
        _currentIndex = 0;
        _coloringMaterialInstance.SetFloat("_FlipbookIndex",_currentIndex);
    }

    void OnColoring()
    {
        _currentIndex += Time.deltaTime * _coloringSpeed;
        if (_currentIndex > _maxIndex)
        {
            _currentIndex = _maxIndex;
        }
        _coloringMaterialInstance.SetFloat("_FlipbookIndex",_currentIndex);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                _paintPoint = hit.textureCoord;
                _paintPoint.x *= 2;
                _coloringMaterialInstance.SetFloat("_TouchPositionX",_paintPoint.x);
                _coloringMaterialInstance.SetFloat("_TouchPositionY",_paintPoint.y);
                _doPaint = true;
            }
        }
        if (_doPaint && _currentIndex < _maxIndex)
        {
            OnColoring();
        }
        if (Input.GetMouseButtonUp(0))
        {
            _doPaint = false;
        }
    }

}
