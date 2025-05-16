using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : Button {
    public bool _isHighlighted {
        get {
            return IsHighlighted();
        }
    }
}