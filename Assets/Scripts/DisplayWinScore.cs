using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWinScore : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = "Score: " + PlayerPrefs.GetInt("Score");
    }
}
