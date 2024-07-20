using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

namespace Soft
{
    public class SoftBody : MonoBehaviour
    {
        [SerializeField] private SpriteShapeController ssc;
        [SerializeField] private Transform[] points;
        private List<CircleCollider2D> circleCollider2Ds = new();
        public GameObject test;
        public float tangentScale;
        public float offset;

        private void Start()
        { 
            SetPoints();
        }
        

        private void SetPoints()
        {
            var degreePerVertical = 360 / (points.Length - 1);
            for (int i = 0; i < points.Length-1; i++)
            {
                
                circleCollider2Ds.Add(points[i].GetComponent<CircleCollider2D>());
            }
            
            
        }

        private void Awake()
        {
        }

        private void FixedUpdate()
        {
            UpdateVerticies();
        }

        private void UpdateVerticies()
        {
            for (int i = 0; i < points.Length-1; i++)
            {
                Vector2 vertex = points[i].localPosition;
                Vector2 towardsCenter = (Vector2.zero - vertex).normalized;
                var radius = circleCollider2Ds[i].radius;
                try
                {
                    ssc.spline.SetPosition(i,(vertex.normalized)*(vertex.magnitude+radius+offset));
                }
                catch (Exception e)
                {
                    ssc.spline.SetPosition(i,(vertex.normalized)*(vertex.magnitude+radius+offset+1f));
                    print("too close!!");
                }
                ssc.spline.SetTangentMode(i,ShapeTangentMode.Continuous);
                //test.transform.localPosition = ssc.spline.GetPosition(1);
                Vector2 newRt = Vector2.Perpendicular(towardsCenter)*tangentScale; //右切线
                Vector2 newLt = -Vector2.Perpendicular(towardsCenter)*tangentScale; //取负方向就是左切线
                ssc.spline.SetLeftTangent(i,newLt);
                ssc.spline.SetRightTangent(i,newRt);
            }
        }
      
    }
}