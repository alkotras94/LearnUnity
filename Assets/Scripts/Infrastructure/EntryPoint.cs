using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Curtain _curtain;
        private string _sceneName = "MenuScene";

        void Start()
        {
            Curtain curtain = Instantiate(_curtain);
            curtain.Initialize();
            StartGame(curtain);
        }
    
        public async Task StartGame(Curtain curtain) => await LoadSceneAsync(_sceneName, curtain);

        public async Task LoadSceneAsync(string sceneName, Curtain curtain)
        {
            AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
        
            scene.allowSceneActivation = false;
        
            float fakeProgress = 0f; // Фейковый прогресс
            while (scene.progress < 0.9f || fakeProgress < 1f)
            {
                fakeProgress += 0.01f; // Скорость увеличения прогресса
                if (curtain.LoadProgressBar != null)
                    curtain.LoadProgressBar.value = Mathf.Min(fakeProgress, 1f);

                await Task.Yield(); // Отдаем управление в главный поток и ждем окончание кадра
                await Task.Delay(30); // Задержка для видимости прогресс бара
            }
        
            if (curtain.LoadProgressBar != null)
                curtain.LoadProgressBar.value = 1f;
        
            scene.allowSceneActivation = true;
        }
    }
}
