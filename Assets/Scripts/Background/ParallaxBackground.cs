using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Background
{
    public class ParallaxBackground : MonoBehaviour
    {
        private GameObject cam;
        [SerializeField] private float parallaxEffect; // 视差倍率

        private float xPosition; //当前背景的滚动的初始x位置
        private float length; //当前背景渲染图层的长度大小

       void Start()
       {
            cam = GameObject.Find("Main Camera");
            length = GetComponent<SpriteRenderer>().bounds.size.x;
            print(length);
            xPosition = transform.position.x;
       }

       private void Update()
       {
           float distanceToMove = cam.transform.position.x * parallaxEffect;
           float distanceMoved = cam.transform.position.x * (1 - parallaxEffect); //摄像机和背景的位置距离
           transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

           // 滚动一次，改变滚动后的初始x位置
           if (distanceMoved > xPosition + length)
               xPosition = xPosition + length; 
           else if (distanceMoved < xPosition - length)
               xPosition = xPosition - length;
       }
    }
}