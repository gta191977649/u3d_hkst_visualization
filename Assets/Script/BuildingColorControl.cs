using UnityEngine;

public class ChangeChildColors : MonoBehaviour
{
    public GameObject[] targetObjects; // Array of target GameObjects
    public Color color1 = Color.red;  // First color
    public Color color2 = Color.blue; // Second color
    public float frequency = 1.0f;   // Speed of the color change

    void Start()
    {
        // Initial set up, if necessary.
    }

    void Update()
    {
        // Generate a value between 0 and 1 based on a sine function
        float lerpValue = Mathf.Sin(Time.time * frequency) * 0.5f + 0.5f;

        // Generate a color based on the lerpValue
        Color targetColor = Color.Lerp(color1, color2, lerpValue);

        // Loop through each target object in the array
        foreach (GameObject targetObject in targetObjects)
        {
            // Get the MeshRenderer component from the target GameObject
            MeshRenderer renderer = targetObject.GetComponent<MeshRenderer>();

            // Check if the MeshRenderer component is available
            if (renderer != null)
            {
                Material[] materials = renderer.materials; // Get all materials

                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i].SetColor("_Wireframe_Color", targetColor); // Change the "_Wireframe_Color" property of each material
                }

                renderer.materials = materials; // Re-assign the materials
            }
            else
            {
                Debug.LogWarning("No MeshRenderer found on " + targetObject.name + "!");
            }
        }
    }
}
