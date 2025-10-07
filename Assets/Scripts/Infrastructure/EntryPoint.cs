using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
        public Curtain Curtain;
        private string _sceneName = "MenuScene";

        async void Start()
        {
            Task t2 = LoadImageAsync();
            Task t1 = LoadSceneAsync(_sceneName);
            
            await Task.WhenAll(t1, t2);
            
            Debug.Log("Scene loaded");
        }

        private void Update()
        {
            //Debug.Log("Update");
        }
        
        public async Task LoadSceneAsync(string sceneName)
        {
            AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
        
            scene.allowSceneActivation = false;
        
            float fakeProgress = 0f; // Фейковый прогресс
            while (scene.progress < 0.9f || fakeProgress < 1f)
            {
                fakeProgress += 0.01f; // Скорость увеличения прогресса
                if (Curtain.LoadProgressBar != null)
                    Curtain.LoadProgressBar.value = Mathf.Min(fakeProgress, 1f);
                
                Debug.Log("Asynchronous operation");
                await Task.Yield(); // Отдаем управление в главный поток и ждем окончание кадра. Будет использоваться в реальном проекте
                //await Task.Delay(30); // Задержка для видимости прогресс бара
            }
        
            if (Curtain.LoadProgressBar != null)
                Curtain.LoadProgressBar.value = 1f;
        
            scene.allowSceneActivation = true;
        }

        public async Task LoadImageAsync()
        {
            Curtain.Initialize();
            await Task.Yield();
        }
    }
}
