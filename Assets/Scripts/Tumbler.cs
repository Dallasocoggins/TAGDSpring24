using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tumbler : MonoBehaviour
{
    public int pointCost = 100;
    public ItemGrid[] itemGrids;
    public Image outputImage;

    public void OnClick() {
        if (GameManager.instance.points < pointCost) {
            return;
        }

        GameManager.instance.SubtractPoints(pointCost);

        (int gridIndex, int itemIndex) = GetRandomIndex();

        itemGrids[gridIndex].Unlock(itemIndex);
        outputImage.enabled = true;
        outputImage.sprite = itemGrids[gridIndex].sprites[itemIndex];
    }

    (int, int) GetRandomIndex() {
        int totalSprites = 0;
        foreach (ItemGrid itemGrid in itemGrids) {
            totalSprites += itemGrid.sprites.Length;
        }

        int spriteIndex = Random.Range(0, totalSprites);
        for (int i = 0; i < itemGrids.Length; i++) {
            if (itemGrids[i].sprites.Length > spriteIndex) {
                return (i, spriteIndex);
            }

            spriteIndex -= itemGrids[i].sprites.Length;
        }

        return (0, 0);
    }
}
