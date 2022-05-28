using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Nebby.CSharpUtils;

namespace MTT2
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class RandomSpriteAssigner : MonoBehaviour
    {
        public Sprite[] sprites = Array.Empty<Sprite>();
        public UnityEvent onSpriteAssigned;

        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            if(sprites.TryGetRandomElement(out Sprite sprite))
            {
                spriteRenderer.sprite = sprite;
                onSpriteAssigned.Invoke();
            }
        }
    }
}
