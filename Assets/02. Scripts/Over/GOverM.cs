using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GOverM : MonoBehaviour
{
    [Header("재시작")]
    [SerializeField, Tooltip("메인씬")] string mainScene = "Mini";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //R키에 리스타트
        if(Input.GetKeyDown(KeyCode.R)) { RestartGame(); }

    }

    void RestartGame()
    {
        Time.timeScale = 1f; //게임시간을 정상적으로 흐르게 확인용 코드

        SceneManager.LoadScene(mainScene); //메인씬을 로드하여 게임 재시작 
        
    }
}
