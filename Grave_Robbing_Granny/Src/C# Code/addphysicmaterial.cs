using UnityEngine;

public class AssignPhysicsMaterial : MonoBehaviour
{
    public PhysicMaterial physicMaterial;

    void Start()
    {
        AssignMaterialRecursively(transform);
    }

    void AssignMaterialRecursively(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Collider collider = child.GetComponent<Collider>();
            if (collider != null)
            {
                collider.material = physicMaterial;
            }
            AssignMaterialRecursively(child); // Recursive call for deeper children
        }
    }
}
