using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGrid : MonoBehaviour
{
    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < sprites.Length; i++) {
            var sprite = sprites[i];

            var newObject = new GameObject();
            newObject.name = sprite.name;
            newObject.transform.parent = transform;

            var image = newObject.AddComponent<Image>();
            image.sprite = sprite;

            var button = newObject.AddComponent<Button>();
            button.onClick.AddListener(() => OnClick(i));
            button.onClick.AddListener(SayHi);
            var navigation = button.navigation;
            navigation.mode = Navigation.Mode.None;
            button.navigation = navigation;
        }
    }

    void OnClick(int index) {
        Debug.Log("Index " + index + " clicked");
    }

    void SayHi() {
        Debug.Log("Hi!");
    }
}
