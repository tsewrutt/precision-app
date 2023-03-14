using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Settings_setup : MonoBehaviour
{
    public Button def_b;
    public Button pro_b;
    public Button deu_b;
    public Button tri_b;

    // Start is called before the first frame update
    void Start()
    {
        def_b.onClick.AddListener(defaultSettings);
        pro_b.onClick.AddListener(proSettings);
        deu_b.onClick.AddListener(deuSettings);
        tri_b.onClick.AddListener(triSettings);
    }
// Update is called once per frame
    void defaultSettings()
    {
        SceneManager.LoadScene("mainScene");
    }

    void proSettings()
    {
        // settings requirement for pro
        SceneManager.LoadScene("proScene");
    }

    void deuSettings()
    {
        // settings requirement for deu
        SceneManager.LoadScene("deuScene");
    }

    void triSettings()
    {
        // settings requirement for tri
        SceneManager.LoadScene("triScene");
    }
}
