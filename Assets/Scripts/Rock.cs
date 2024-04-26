using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public Sprite[] sprites;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        spriteRenderer.sprite = sprites[GameManager.instance.rockIndex];
    }
}
