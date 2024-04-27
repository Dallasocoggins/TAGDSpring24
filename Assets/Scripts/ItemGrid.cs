using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGrid : MonoBehaviour
{
    public Sprite[] sprites;

    public enum ItemType {
        Person,
        Rock,
        Background,
    }

    public ItemType type;

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
            image.preserveAspect = true;

            var button = newObject.AddComponent<Button>();
            // Creating a new variable like this will make sure each lambda expression for the onClick has a different value
            var index = i;
            button.onClick.AddListener(() => OnClick(index));
            var navigation = button.navigation;
            navigation.mode = Navigation.Mode.None;
            button.navigation = navigation;
        }
    }

    void OnClick(int index) {
        switch (type) {
            case ItemType.Person:
                GameManager.instance.personIndex = index;
                break;
            case ItemType.Rock:
                GameManager.instance.rockIndex = index;
                break;
            case ItemType.Background:
                GameManager.instance.backgroundIndex = index;
                break;
        }
    }
}
