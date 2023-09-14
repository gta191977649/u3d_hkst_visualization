using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyAllMesh : MonoBehaviour
{
    public GameObject root; // The root GameObject to start the search from, can be set in the Inspector.
    public Material materialToApply; // The material to apply, settable from the Inspector.

    // Start is called before the first frame update
    void Start()
    {
        if (root == null) // If the root object is not set, start from this object.
        {
            root = this.gameObject;
        }

        ApplyScriptAndMaterialToChildren(root.transform);
    }

    // Recursive function to loop through all child objects and their descendants
    void ApplyScriptAndMaterialToChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            // Check if the child object has a MeshRenderer component
            MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
               
                // Replace all the materials
                if (materialToApply != null)
                {
                    Material[] newMaterials = new Material[meshRenderer.materials.Length];
                    for (int i = 0; i < newMaterials.Length; i++)
                    {
                        //newMaterials[i] = materialToApply;
                        Material newMat = Instantiate(materialToApply);
                        newMaterials[i] = newMat;
                    }
                    meshRenderer.materials = newMaterials;
                }
                else
                {
                    Debug.LogWarning("No material specified to apply.");
                }
            }

            // Recursively call this function for any child objects
            ApplyScriptAndMaterialToChildren(child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
