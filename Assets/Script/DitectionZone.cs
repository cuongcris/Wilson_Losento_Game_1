using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;

namespace Assets.Script
{
    public class DitectionZone : MonoBehaviour
    {
        public UnityEvent noColliderEvent;
        public List<Collider2D> detectionCol = new List<Collider2D>();

        Collider2D col;
        private void Awake()
        {
            col = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            detectionCol.Add(collision);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            detectionCol.Remove(collision);
            if (detectionCol.Count <= 0)
            {
                noColliderEvent.Invoke();
            }
        }
    }
}
