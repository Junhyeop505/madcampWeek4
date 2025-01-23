using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    public Vector3 spawnPosition = new Vector3(6, 11, -42);
    public Quaternion spawnRotation = Quaternion.identity;

    void Start()
    {
        GameObject planeSpawnerPrefab = Resources.Load<GameObject>("PlaneSpawner");
        if (PlaneData.Instance == null)
        {
            Debug.LogError("PlaneData instance is null! Ensure PlaneData exists.");
            return;
        }

        GameObject selectedPlanePrefab = PlaneData.Instance.GetSelectPlane();
        if (selectedPlanePrefab != null)
        {
            GameObject spawnedPlane = Instantiate(selectedPlanePrefab, spawnPosition, spawnRotation);
            CameraFollow cameraFollow=Camera.main.GetComponent<CameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.target = spawnedPlane.transform;
            }
            Debug.Log("Spawned plane: " + spawnedPlane.name);
        }
        else
        {
            Debug.LogError("No plane selected, unable to spawn.");
        }
    }
}
