using UnityEngine.UI;
using UnityEngine;

public class Field : MonoBehaviour
{
    [field: SerializeField]
    public RawImage Image { get; private set; }

    [field : SerializeField]
    public Button Button { get; private set; }
}
