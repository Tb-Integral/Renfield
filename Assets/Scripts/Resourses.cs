using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Resourses : MonoBehaviour
{

    public static Resourses Instance;

    public int fliesCount = 0;
    public int spidersCount = 0;
    public int birdsCount = 0;

    public TextMeshProUGUI FlyText;
    public TextMeshProUGUI SpiderText;
    public TextMeshProUGUI BirdText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        FlyText = GameObject.Find("TextFly").GetComponent<TextMeshProUGUI>();
        SpiderText = GameObject.Find("TextSpider").GetComponent<TextMeshProUGUI>();
        BirdText = GameObject.Find("TextBird").GetComponent<TextMeshProUGUI>();

        FlyText.text = fliesCount.ToString();
        SpiderText.text = spidersCount.ToString();
        BirdText.text = birdsCount.ToString();
    }
}
