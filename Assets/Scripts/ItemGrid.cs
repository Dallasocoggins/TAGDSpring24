using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ItemGrid : MonoBehaviour {
    public Sprite[] sprites;
    public Sprite outlineSprite;
    public Sprite lockedSprite;

    public enum ItemType {
        Person,
        Rock,
        Background,
    }

    public ItemType type;

    ItemType startingType = ItemType.Rock;

    GameObject[] outlines;
    Image[] images;

    int itemIndex {
        get {
            switch (type) {
                case ItemType.Person:
                    return GameManager.instance.personIndex;
                case ItemType.Rock:
                    return GameManager.instance.rockIndex;
                case ItemType.Background:
                    return GameManager.instance.backgroundIndex;
                default:
                    return -1;
            }
        }
        set {
            switch (type) {
                case ItemType.Person:
                    GameManager.instance.personIndex = value;
                    break;
                case ItemType.Rock:
                    GameManager.instance.rockIndex = value;
                    break;
                case ItemType.Background:
                    GameManager.instance.backgroundIndex = value;
                    break;
            }
        }
    }

    bool[] unlockedArray {
        get {
            switch (type) {
                case ItemType.Person:
                    return GameManager.instance.unlockedPeople;
                case ItemType.Rock:
                    return GameManager.instance.unlockedRocks;
                case ItemType.Background:
                    return GameManager.instance.unlockedBackgrounds;
                default:
                    return null;
            }
        }
    }


    void Start()
    {
        outlines = new GameObject[sprites.Length];
        images = new Image[sprites.Length];

        for (int i = 0; i < sprites.Length; i++) {
            var sprite = unlockedArray[i] ? sprites[i] : lockedSprite;

            var newObject = new GameObject();
            newObject.name = sprites[i].name;
            newObject.transform.parent = transform;

            var image = newObject.AddComponent<Image>();
            image.sprite = sprite;
            image.preserveAspect = true;
            images[i] = image;

            var button = newObject.AddComponent<Button>();
            // Creating a new variable like this will make sure each lambda expression for the onClick has a different value
            var index = i;
            button.onClick.AddListener(() => OnClick(index));
            var navigation = button.navigation;
            navigation.mode = Navigation.Mode.None;
            button.navigation = navigation;

            var outlineObject = new GameObject("Outline", typeof(RectTransform));
            outlineObject.transform.parent = newObject.transform;
            RectTransform rect = outlineObject.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = new Vector2(-5, -5);
            rect.offsetMax = new Vector2(5, 5);

            var outlineImage = outlineObject.AddComponent<Image>();
            outlineImage.sprite = outlineSprite;

            outlines[i] = outlineObject;
            outlineObject.SetActive(false);
        }

        outlines[itemIndex].SetActive(true);

        gameObject.SetActive(type == startingType);
    }

    void OnClick(int index) {
        if (!unlockedArray[index]) {
            Unlock(index);
            return;
        }

        itemIndex = index;
        foreach (var outline in outlines) {
            outline.SetActive(false);
        }
        outlines[index].SetActive(true);
    }

    public void Unlock(int index) {
        images[index].sprite = sprites[index];
        unlockedArray[index] = true;
    }
}
