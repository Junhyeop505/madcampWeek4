using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    public Vector3 spawnPosition = new Vector3(0, 10, -70);
    public Quaternion spawnRotation = Quaternion.identity;

    void Start()
{
    if (PlaneData.Instance == null)
    {
        Debug.LogError("PlaneData instance is null! Ensure PlaneData exists.");
        return;
    }

    if (PlaneData.Instance.planeModels.Count == 0)
    {
        Debug.LogError("No planes in PlaneData.planeModels. Ensure planes are created before transitioning to this scene.");
        return;
    }

    int selectedModel = PlaneData.Instance.selectedPlaneIndex;
    if (selectedModel < 0 || selectedModel >= PlaneData.Instance.planeModels.Count)
    {
        Debug.LogError($"Invalid selectedPlaneIndex: {selectedModel}. Plane models count: {PlaneData.Instance.planeModels.Count}");
        return;
    }

    GameObject selectedPlanePrefab = PlaneData.Instance.GetSelectPlane();
        if (selectedPlanePrefab != null)
        {
            GameObject spawnedPlane = Instantiate(selectedPlanePrefab, spawnPosition, spawnRotation);
            Debug.Log($"Spawned plane: {spawnedPlane.name}");

            // Assign the spawned plane to the camera's target
            CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.target = spawnedPlane.transform;
                Debug.Log("Camera target updated to the spawned plane.");
            }
            else
            {
                Debug.LogError("CameraFollow script is missing on the main camera.");
            }
        }
        else
        {
            Debug.LogError($"Prefab at index {selectedModel} is null. Check if the plane was properly added or destroyed.");
        }
    }
}
