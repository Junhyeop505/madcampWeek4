using UnityEngine;
using TMPro;

public class Gameovercloud : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMP_Text gradeText;
    public ParticleSystem cloudParticles;
    void Start()
    {
        float distance=PlaneData.Instance.planeTravelDistance;
        if (distance > 90) {
            {
                SetCloudParticleGradientColor();
                AdjustCloudParticleSizeAndSpread();
            } 
        }
        
    }

    private void SetCloudParticleGradientColor()
    {
        if (cloudParticles != null)
        {
            var main = cloudParticles.main;

            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[]
                {
                    new GradientColorKey(Color.yellow, 0.0f),
                    new GradientColorKey(Color.red, 0.6f),
                    new GradientColorKey(Color.blue, 1.0f)
                },
                new GradientAlphaKey[]
                {
                    new GradientAlphaKey(1.0f, 0.0f),
                    new GradientAlphaKey(1.0f, 1.0f),
                    new GradientAlphaKey(1.0f,0.8f),
                    new GradientAlphaKey(0.0f,1.0f)
                }
            );
            main.startColor = new ParticleSystem.MinMaxGradient(gradient);
        }
    }
    private void AdjustCloudParticleSizeAndSpread()
    {
        if(cloudParticles != null)
        {
            var main=cloudParticles.main;
            var shape=cloudParticles.shape;

            // main.startSize = new ParticleSystem.MinMaxCurve(0.5f, 1.0f);
            main.startSize3D=true;
            main.startSizeX=new ParticleSystem.MinMaxCurve(0.5f,0.8f);
            main.startSizeY=new ParticleSystem.MinMaxCurve(0.3f,0.5f);
            main.startSizeZ=new ParticleSystem.MinMaxCurve(0.5f,0.8f);
            shape.shapeType=ParticleSystemShapeType.Sphere;
            shape.radius=5.0f;
            shape.scale=new Vector3(2.0f,2.0f,2.0f);

            // shape.radius = 5.0f;
            // shape.angle = 45.0f;
        }
    }
}
