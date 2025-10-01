using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace HeroPlayer
{
    public class HeroAnimations : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer; 
        [SerializeField] private float maxDelay = 0.5f; // допустимый промежуток между кликами
        [SerializeField] private float _startAlpfa = 0f;
        [SerializeField] private float _finishAlpha = 1f;
        [SerializeField] private int _durationMs = 50000;
        [SerializeField] private int stepMs = 5000;
        
        private int clickCount = 0;
        private float clickTimer = 0f;
        private CancellationTokenSource cts;



        async void Start()
        {
            cts = new CancellationTokenSource();
            await StartAnimations(cts.Token, _startAlpfa, _finishAlpha, _durationMs, stepMs);
            Debug.Log("Ассинхронная операция закончилась");
        }

        void OnMouseDown()
        {
            clickCount++;
            clickTimer = maxDelay;

            if (clickCount == 3)
            {
                cts.Cancel();
                Debug.Log("Объект кликнут 3 раза!");
                clickCount = 0;
            }
        }

        void Update()
        {
            if (clickTimer > 0)
            {
                clickTimer -= Time.deltaTime;
                if (clickTimer <= 0)
                {
                    clickCount = 0;
                }
            }
        }

        private async Task StartAnimations(CancellationToken token, float startAlpfa, float finishAlpha, int durationMs, int stepMs)
        {
            Color color = _spriteRenderer.color;
            color.a = startAlpfa;
            _spriteRenderer.color = color;

            try
            {
                float alpha = startAlpfa;
                int elapsed = 0;

                while (elapsed < durationMs)
                {
                    //token.ThrowIfCancellationRequested();

                    elapsed += stepMs;
                    float t = Mathf.Clamp01((float)elapsed / durationMs);
                    alpha = Mathf.Lerp(startAlpfa, finishAlpha, t);

                    color.a = alpha;
                    _spriteRenderer.color = color;

                    Debug.Log("Идет операция");

                    await Task.Delay(stepMs, token);
                }
            }
            catch (TaskCanceledException)
            {
                Debug.Log("Анимация прервана пользователем.");
            }
            finally
            {
                color.a = finishAlpha;
                _spriteRenderer.color = color;
            }
        }

        
    }
}