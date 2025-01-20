using UnityEngine;

public class PlaneData:MonoBehaviour
{
    public static PlaneData Instance;
    public int selectedPlaneIndex = 0;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlaneModel(int index)
    {
        selectedPlaneIndex = index;
    }
}
   
