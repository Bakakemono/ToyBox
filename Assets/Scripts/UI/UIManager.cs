using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField] CustomButton _button;

    [SerializeField] GameObject _highlightedObject;
    [SerializeField] GameObject _notHighlighted;

    private void Update() {
        if(_button._isHighlighted) {
            _highlightedObject.SetActive(true);
            _notHighlighted.SetActive(false);
        }
        else {
            _highlightedObject.SetActive(false);
            _notHighlighted.SetActive(true);
        }
    }
}