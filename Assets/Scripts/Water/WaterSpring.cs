using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

namespace Water
{
    public class WaterSpring : MonoBehaviour
    {
        public float velocity = 0;
        public float force = 0;
        public float height = 0; //current height
        private float target_height = 0;
        private int waveIndex;
        private SpriteShapeController spriteShapeController;
        private float resistance=40f;

        public void Init(SpriteShapeController ssc)
        {
            var index = transform.GetSiblingIndex();
            waveIndex = index + 1; // due to include the corner ,so we should plus 1
            spriteShapeController = ssc;
            velocity = 0;
            height = transform.localPosition.y;
            target_height = transform.localPosition.y;
        }

        // WavePrefab move 
        public void WaveSpringUpdate(float springStiffness,float dampening)
        {
            height = transform.localPosition.y; // Real-time update the height
            var x = height - target_height; // extension
            var loss = -dampening * velocity;
            
            force = -springStiffness * x + loss; // force  = - k x (hook's law)
            velocity += force;
            var y = transform.localPosition.y;
            transform.localPosition = new Vector3(transform.localPosition.x, y + velocity, transform.localPosition.z);
        }

        // spline wave point move with WavePrefab
        public void WavePointUpdate()
        {
            if (spriteShapeController != null)
            {
                Spline waterSpline = spriteShapeController.spline;
                Vector3 wavePosition = waterSpline.GetPosition(waveIndex);
                waterSpline.SetPosition(waveIndex,new Vector3(wavePosition.x,transform.localPosition.y,wavePosition.y)); // Spline's Point do Motion with wavePrefab
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {

            var speed = other.GetComponent<Rigidbody2D>();
            if (speed != null)
                velocity += speed.velocity.y/resistance;
        }
    }
}