using UnityEngine;
using UnityEngine.Serialization;


public class ColoringObject : MonoBehaviour, IColoringObject
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Material _coloringMaterial;
    [SerializeField] private float _coloringSpeed = 1;
    private Material _coloringMaterialInstance;
    private float _fillAmount;
    private Vector2 _paintPoint; 
    private bool _painting;
    private bool _paintingFinished;

    private void Awake()
    {
        _coloringMaterialInstance = Instantiate(_coloringMaterial);
        _renderer.material = _coloringMaterialInstance;
        ResetColoring();
        Assign();
    }
    
    private void Update()
    {
        if (_painting && _fillAmount <= 1)
        {
            OnColoring();
        }
    }
    
    public void Assign()
    {
       ColoringManager.Instance.AddColoringObject(this);
    }
    
    public void ResetColoring()
    {
        _fillAmount = 0;
        _coloringMaterialInstance.SetFloat("_FillAmount", _fillAmount);
        _painting = false;
        _paintingFinished = false;
    }
    
    public void OnColoring()
    {
        _fillAmount += Time.deltaTime * .01f * _coloringSpeed;
        if (_fillAmount > 1)
        {
            _painting = false;
            _paintingFinished = true;
        }
        _coloringMaterialInstance.SetFloat("_FillAmount",_fillAmount);
    }

    public void StartColoring(Vector2 position)
    {
        if (_painting || _paintingFinished)
        {
            return;
        }
        _coloringMaterialInstance.SetFloat("_TouchPositionX",position.x-.5f);
        _coloringMaterialInstance.SetFloat("_TouchPositionY",position.y-.5f);
        _painting = true;
    }


  
}

public interface IColoringObject
{
    void Assign();
    void ResetColoring();
    void OnColoring();
    void StartColoring(Vector2 position);
}
