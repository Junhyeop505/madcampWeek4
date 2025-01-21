using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ChoosePlane(int index)
    {
        Debug.Log("choose plane: "+index);
        if (PlaneData.Instance != null)
        {
            PlaneData.Instance.SetPlaneModel(index);
            Debug.Log("plane model set");
        }
        else
        {
            Debug.LogError("Plane is null");
        }
    }
}
