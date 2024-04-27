using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Points : MonoBehaviour
{
    TextMeshProUGUI m_TextMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        m_TextMeshPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_TextMeshPro);
        Debug.Log(GameManager.instance);
        m_TextMeshPro.text = GameManager.instance.points.ToString();
    }
}
