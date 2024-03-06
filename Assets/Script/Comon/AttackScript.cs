using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script
{
    public class attackScript : MonoBehaviour
    {
        private int attackDame;

        public NextLevelTriiger levelTriiger;
        public Enemy enemyAttribute;

        public Vector2 knockBack = Vector2.zero;

        private void Update()
        {
            //nếu qua màn thì offset(hệ số theo level) sẽ thay đổi --> dẫn đến chỉ số của quái sẽ thay đổi theo
            if (levelTriiger != null && enemyAttribute != null)
            {
                attackDame = enemyAttribute.Damage + levelTriiger.offset;
            }
            else
            {
                attackDame = 20;
                //dame cua player
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //see it if can be hit
            Damageable damageable = collision.GetComponent<Damageable>();

            if (damageable != null)
            {
                //đảo ngược hướng văng theo chiều đánh
                Vector2 directionKnockback = transform.parent.localScale.x > 0 ? knockBack : new Vector2(-knockBack.x, knockBack.y);
                //hit target
              
                bool getDame = damageable.Hit(attackDame, directionKnockback);
                
            }
        }         
    }
}
