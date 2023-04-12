using UnityEngine;
using UnityEngine.UI;

public interface IViewMap
{
    public delegate void ChoiseField(string name);
    public event ChoiseField ChoiseEvent;

    public void show(FieldType[,] map, int size);
    public void UpdateField(FieldType field, int position);
}

public class MapView : MonoBehaviour, IViewMap
{
    [SerializeField] private Field _prefab;
    [SerializeField] private Transform _content;
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private Sprite[] _fieldIcons;

    public event IViewMap.ChoiseField ChoiseEvent;

    private Field[] _fields;

    public void show(FieldType[,] map, int size)
    {
        _fields = new Field[size * size];
        _grid.constraintCount = size;

        for (int i = 0; i < size * size; i++)
        {
            Field field = GameObject.Instantiate(_prefab, _content);
            field.name = $"0_{i}";
            field.Image.texture = _fieldIcons[0].texture;
            field.Button.onClick.AddListener(() => ChoiseEvent?.Invoke(field.name));
            _fields[i] = field;
        }

    }

    public void UpdateField(FieldType field, int position)
    {
        Field fieldTmp = _fields[position];
        int fieldType = (int)field;
        fieldTmp.name = $"{fieldType}_{position}";
        fieldTmp.Image.texture = _fieldIcons[fieldType].texture;
    }

}