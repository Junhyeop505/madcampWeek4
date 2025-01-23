using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TMP_Text gradeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float distance=PlaneData.Instance.planeTravelDistance;
        string grade=GetGrade(distance);
        gradeText.text = $"Distance: {distance:F2} meters\nGrade: {grade}";
    }

     private string GetGrade(float distance)
    {
        if (distance > 97)
            return "A+";
        else if(distance>94)
            return "A";
        else if(distance>90)
            return "A-";
        else if(distance>85)
            return "B+";
        else if (distance > 80)
            return "B";
        else if(distance>75)
            return "B-";
        else if(distance>70)
            return "C+";
        else if(distance>63)
            return "C";
        else if(distance>55)
            return "C-";
        else if(distance>50)
            return "D+";
        else if(distance>43)
            return "D";
        else if(distance>37)
            return "D-";
        else return "F";
    }
}
