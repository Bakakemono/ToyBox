using UnityEngine;

public class Test : MonoBehaviour
{
    Node _behaviorTree;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _behaviorTree = new Sequence();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
