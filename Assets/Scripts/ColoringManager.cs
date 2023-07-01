using System.Collections.Generic;
using UnityEngine;

public class ColoringManager : MonoBehaviour
{
    public static ColoringManager Instance { get; private set; }

    private List<IColoringObject> _coloringObjectList = new List<IColoringObject>();
    private Camera _camera;

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

    public void AddColoringObject(IColoringObject coloringObject)
    {
        _coloringObjectList.Add(coloringObject);
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                if (hit.transform.TryGetComponent(out IColoringObject coloringObject))
                {
                    coloringObject.StartColoring(hit.textureCoord);
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
