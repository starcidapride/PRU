using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HPManager : Singleton<HPManager>
{
    private TMP_Text _text;
    void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = $"x {PlayerManager.Instance.Health}";
    }
}
