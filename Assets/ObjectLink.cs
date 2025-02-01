using UnityEngine;
public class ObjectLink : MonoBehaviour
{
    [SerializeField] private GameObject objectToMove;
    private Quaternion objectRotation;
    void Update() {
        transform.rotation = objectRotation;
        // objectToMove.transform.SetPositionAndRotation(transform.position, transform.rotation);
        objectToMove.transform.localRotation = objectRotation;
    }
}