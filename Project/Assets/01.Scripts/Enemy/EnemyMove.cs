using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed;

    public float RandSpeed()
   {
       return speed =  (float)Random.Range(0.5f,0.8f);
   }
   void Update()
   {
       transform.Translate(Vector2.right * -1 * speed * Time.deltaTime);
   }

}
