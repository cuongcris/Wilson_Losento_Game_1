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
        public int attackDame = 10;
        public Vector2 knockBack = Vector2.zero;
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
