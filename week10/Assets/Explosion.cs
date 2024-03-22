using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
 
    void OnCollisionEnter(Collision col)
    {
        //如果碰撞物是上方落下的小球，进行爆炸处理
        if (col.transform.name == "Sphere")
        {
            //定义爆炸半径
            float radius = 3.0f;
            //定义爆炸位置为炸弹位置
            Vector3 explosionPos = transform.position;
            //这个方法用来反回球型半径之内（包括半径）的所有碰撞体collider[]
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
 
            //遍历返回的碰撞体，如果是刚体，则给刚体添加力
            foreach (Collider hit in colliders)
            {
                if (hit.GetComponent<Rigidbody>())
                {
                    hit.GetComponent<Rigidbody>().AddExplosionForce(600, explosionPos, radius, 1.0f);
                }
                //销毁炸弹和小球
                // Destroy(col.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
