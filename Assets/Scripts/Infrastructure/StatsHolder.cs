using System;
using UnityEngine;
using Random = UnityEngine.Random;


// Сделать дженерик класс Range<T> для использования его с любым типом
// Заменить пары полей min max на одно поле Range, например damageRange
// Класс Range должен отображаться в инспекторе

namespace Infrastructure
{
    public class StatsHolder : MonoBehaviour
    {
        [SerializeField] private FloatRange damageRange =  new FloatRange(); 
        [SerializeField] private FloatRange healthRange =  new FloatRange(); 
        [SerializeField] private IntRange levelRange =  new IntRange(); 

        private void Start()
        {
            print($"{Random.Range(damageRange.Min, damageRange.Max)}");
            print($"{Random.Range(healthRange.Min, healthRange.Max)}");
            print($"{Random.Range(levelRange.Min, levelRange.Max)}");
        }
    }

    [Serializable]
    public class Range<T> where T : struct
    {
        [SerializeField] private T min;
        [SerializeField] private T max;

        public T Min => min;
        public T Max => max;

    }

    [Serializable]
    public class FloatRange : Range<float>
    {
        
    }
    
    [Serializable]
    public class IntRange : Range<int>
    {
        
    }
}