using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextIni : MonoBehaviour
{
    public Text LevelTitle;
    public Text DailyTitle;
    public float movementSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * movementSpeed * Time.deltaTime);
    }

    public void IniText(string levelTitle, string dailyTitle)
    {
        LevelTitle.text = levelTitle;
        DailyTitle.text = dailyTitle;
    }
}
