using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace Soft
{
    public class SoftBody : MonoBehaviour
    {
        [SerializeField] private SpriteShapeController ssc;
        [SerializeField] private Transform[] points;
        private List<CircleCollider2D> circleCollider2Ds = new();
        public float offset;

        private void Start()
        { 
            SetPoints();
        }
        

        private void SetPoints()
        {
            for (int i = 0; i < points.Length; i++)
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
            for (int i = 0; i < points.Length; i++)
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
                Vector2 newRt = Vector2.Perpendicular(towardsCenter); //右切线
                Vector2 newLt = -Vector2.Perpendicular(towardsCenter); //取负方向就是左切线
                ssc.spline.SetLeftTangent(i,newLt);
                ssc.spline.SetRightTangent(i,newRt);
                Smoothen(ssc.spline,i);
            }
        }
        
        private void Smoothen(Spline softSpline, int index)
        {
            Vector3 position = softSpline.GetPosition(index);
            Vector3 positionPrev = position;
            Vector3 positionNext = position;
            if (index == 0)
            {
                positionPrev = softSpline.GetPosition(points.Length-1);
            }
            else if (index > 1) {
                positionPrev = softSpline.GetPosition(index-1);
            }
            
            if (index == points.Length - 1)
            {
                positionNext = softSpline.GetPosition(0);
            }
            else if (index - 1 < points.Length) {
                positionNext = softSpline.GetPosition(index+1);
            }
            
            Vector3 forward = gameObject.transform.forward;

            float scale = Mathf.Min((positionNext - position).magnitude, (positionPrev - position).magnitude) * 0.33f;

            Vector3 leftTangent = (positionPrev - position).normalized * scale;
            Vector3 rightTangent = (positionNext - position).normalized * scale;

            SplineUtility.CalculateTangents(position, positionPrev, positionNext, forward, scale, out rightTangent, out leftTangent);
        
            softSpline.SetLeftTangent(index, leftTangent);
            softSpline.SetRightTangent(index, rightTangent);
        }
      
    }
}