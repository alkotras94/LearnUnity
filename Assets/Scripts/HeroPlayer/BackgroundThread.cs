using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace HeroPlayer
{
    public class BackgroundThread : MonoBehaviour
    {
        private async void Start()
        {
            int result = await Task.Run(() => Calculate(5,5));
            Debug.Log("Поток Unity " + Thread.CurrentThread.ManagedThreadId);
            Debug.Log("Результат " + result);
            await Task.Run(() => Debug.Log("Фоновый поток " + Thread.CurrentThread.ManagedThreadId));
        }
        
        private int Calculate(int a, int b)
        {
            return a * b;
        }
        
    }
}