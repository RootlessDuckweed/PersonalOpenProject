using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace Water
{
    public class WaterShapeController : MonoBehaviour
    {
        private const int CornersCount = 2;
        [SerializeField] private SpriteShapeController ssc;
        [SerializeField] private int wavesCount = 6;
        
        [SerializeField] private float springStiffness = 0.1f; //spring scale
        [SerializeField] private List<WaterSpring> springs = new();
        [SerializeField] private float dampening=0.03f;
        public float spread = 0.006f; //Spread scale
        [SerializeField] private GameObject wavePointPref;
        [SerializeField] private GameObject wavePoints;
        private void Start()
        {
            SetWaves();
            Splash(Convert.ToInt32(Mathf.Floor(springs.Count/2)),0.1f);
        }

        private void FixedUpdate()
        {
            foreach (var waterSpring in springs)
            {
                waterSpring.WaveSpringUpdate(springStiffness,dampening);
                waterSpring.WavePointUpdate();
            }
            UpdateSprings();
        }

        private void SetWaves()
        {
            Spline waterSpline = ssc.spline;
            int waterPointCount = waterSpline.GetPointCount();
            // delete point except corner
            for (int i = CornersCount; i < waterPointCount-CornersCount; i++)
            {
                waterSpline.RemovePointAt(CornersCount); 
                
            }

            // heed the detail that  0 index is the bottom-left corner,so in spline's points it is clockwise to count
            Vector3 waterTopLeftCorner = waterSpline.GetPosition(1); // get top-left corner
            Vector3 waterTopRightCorner = waterSpline.GetPosition(2); // get top-right corner
            float waterWidth = waterTopRightCorner.x - waterTopLeftCorner.x; //get water width
            float spacingPerWave = waterWidth / (wavesCount + 1); // get space between per wave (need to plus 1)
            for (int i = wavesCount; i >0; i--)
            {
                int index = CornersCount; // start from index to insert points
                float xPosition = waterTopLeftCorner.x + (spacingPerWave * i);
                Vector3 wavePoint = new Vector3(xPosition, waterTopLeftCorner.y, waterTopLeftCorner.z);
                waterSpline.InsertPointAt(index,wavePoint);
                waterSpline.SetHeight(index,0.1f);
                waterSpline.SetCorner(index,false);
                waterSpline.SetTangentMode(index,ShapeTangentMode.Continuous);
            }
            
            for (int i = 0; i <=wavesCount+1; i++)
            {
                int index = i + 1; // include the corner
                Smoothen(waterSpline, index); // make the point look  smooth
                GameObject wavePoint = Instantiate(wavePointPref, wavePoints.transform, false); // tag
                wavePoint.transform.localPosition = waterSpline.GetPosition(index); // set the prefab in the wavePoint
                WaterSpring waterSpring = wavePoint.GetComponent<WaterSpring>();
                waterSpring.Init(ssc);
                springs.Add(waterSpring);
            }
        }
        
        // use Bessel's curve to smoothen a point's left and right tangent
        private void Smoothen(Spline waterSpline, int index)
        {
            Vector3 position = waterSpline.GetPosition(index);
            Vector3 positionPrev = position;
            Vector3 positionNext = position;
            if (index > 1) {
                positionPrev = waterSpline.GetPosition(index-1);
            }
            if (index - 1 <= wavesCount) {
                positionNext = waterSpline.GetPosition(index+1);
            }

            Vector3 forward = gameObject.transform.forward;

            float scale = Mathf.Min((positionNext - position).magnitude, (positionPrev - position).magnitude) * 0.33f;

            Vector3 leftTangent = (positionPrev - position).normalized * scale;
            Vector3 rightTangent = (positionNext - position).normalized * scale;

            SplineUtility.CalculateTangents(position, positionPrev, positionNext, forward, scale, out rightTangent, out leftTangent);
        
            waterSpline.SetLeftTangent(index, leftTangent);
            waterSpline.SetRightTangent(index, rightTangent);
        }

        private void UpdateSprings()
        {
            int count = springs.Count;
            float[] left_deltas = new float[count];
            float[] right_deltas = new float[count];
            for (int i = 0; i < count; i++)
            {
                if (i > 0)
                {
                    left_deltas[i] = spread * (springs[i].height - springs[i - 1].height);
                    springs[i - 1].velocity += left_deltas[i];
                }

                if (i < springs.Count - 1)
                {
                    right_deltas[i] = spread * (springs[i].height - springs[i + 1].height);
                    springs[i + 1].velocity += left_deltas[i];
                }
            }
        }

        

        private void Splash(int index, float speed)
        {
            if (index >= 0 && index < springs.Count)
            {
                springs[index].velocity += speed;
            }
        }
    }
}