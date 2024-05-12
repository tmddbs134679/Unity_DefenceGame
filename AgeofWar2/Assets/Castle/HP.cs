using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HP : MonoBehaviour
{

      [SerializeField] public Slider slider;
    int maxHP = 100;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float damage)
    {
        slider.value = slider.value - (damage / maxHP);

        if(slider.value <= 0)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
        }
    }
}
