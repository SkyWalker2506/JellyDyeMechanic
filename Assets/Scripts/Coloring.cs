using System;
using UnityEngine;

public class Coloring : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Material _coloringMaterial;
    [SerializeField] private float _coloringSpeed = 1;
    private Material _coloringMaterialInstance;
    private float _coloringRate;
    private Vector2 _paintPoint;
    private bool _doPaint;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _coloringMaterialInstance = Instantiate(_coloringMaterial);
        _renderer.material = _coloringMaterialInstance;
        _coloringRate = 0;
        _coloringMaterialInstance.SetFloat("_FlipbookIndex",_coloringRate);
    }

    void OnColoring()
    {
        _coloringRate += Time.deltaTime * _coloringSpeed;
        if (_coloringRate > 1)
        {
            _coloringRate = 1;
        }
        _coloringMaterialInstance.SetFloat("_FlipbookIndex",_coloringRate);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                _paintPoint = hit.textureCoord;
                _coloringMaterialInstance.SetFloat("_TouchPositionX",_paintPoint.x-.5f);
                _coloringMaterialInstance.SetFloat("_TouchPositionY",_paintPoint.y-.5f);
                _doPaint = true;
            }
        }
        if (_doPaint && _coloringRate <= 1)
        {
            OnColoring();
        }
        if (Input.GetMouseButtonUp(0))
        {
            _doPaint = false;
        }
    }

}
