using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public static Scene_Manager Instance = null;

    //진행할 씬 번호
    public int sceneNumber = 2;
    //로딩 슬라이더 바
    public Slider loadingBar;
    //로딩 진행 텍스트
    public Text loadingtext;

    // Start is called before the first frame update
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "LoadingScene")
        {
            StartCoroutine(TransitionNextScene(sceneNumber));
        }
    }

    // Update is called once per frame

    //비동기 씬 로드 코루틴
    IEnumerator TransitionNextScene(int num)
    {
        //지정된 씬을 비동기 형식으로 로드한다.
        AsyncOperation ao = SceneManager.LoadSceneAsync(num);

        //로드되는 씬의 모습이 화면에 보이지 않게 한다.
        ao.allowSceneActivation = false;

        //로딩이 완료될 때까지 반복해서 씬의 요소들을 로드하고 진행 과정을 화면에 표시한다.
        while(!ao.isDone) //완료되지 않았다면
        {
            //로딩 진행률을 슬라이더 바와 텍스트료 표시한다.
            loadingBar.value = ao.progress;
            loadingtext.text = (ao.progress * 100f).ToString() + "%";

            //씬 로드 진행률이 90퍼가 넘어가면
            if(ao.progress >= 0.9f)
            {
                //씬 화면에 보이게
                ao.allowSceneActivation = true;
            }
            //다음 프레임이 될 때까지 대기            
            yield return null;
        }
        
    }
    public void MainToLoading()
    {
        SceneManager.LoadScene(1);        
    }
}
