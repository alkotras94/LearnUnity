using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    /*2. Реализовать класс ObjectPool<T>, который будет реализовывать паттерн "ObjectPool". Этот класс должен обладать следующей функциональностью
    2.1. Получать объекты типа T и хранить их.
    2.2. Метод получения объекта типа T (пул достаёт из хранилища).

    3.1. Добавить возможность инициализировать пул с заданным количеством объектов сразу. Сигнатура будет следующей - Init(T prefab, int count). 
    Создавать объекты через Instantiate
    3.2. Сделать так - если не хватает объектов в хранилище и вызывается метод получения, то создавать объект через Instantiate
    3.3. Сделать так, чтобы тип T обязательно реализовывал интерфейс IPoolObject. В интерфейсе должно быть объявлено два метода - Init и DeInit.
     Вызывать их при извлечении объекта из пула и возвращении в пул соответственно.
    */
    
    public class ObjectPool<T> where T : MonoBehaviour, IPoolObject
    {
        private readonly Queue<T> _pool = new Queue<T>();
        private T _prefab;
        private Transform _container;
        
        // Инициализация пула с заданным количеством объектов.
        public void Init(T prefab, int count, Transform container = null)
        {
            _prefab = prefab;
            _container = container;

            for (int i = 0; i < count; i++)
            {
                T obj = Object.Instantiate(_prefab, _container);
                obj.gameObject.SetActive(false);
                _pool.Enqueue(obj);
            }
        }

        // Получить объект из пула. Если пул пуст — создаётся новый.
        public T Get()
        {
            T obj;

            if (_pool.Count > 0)
            {
                obj = _pool.Dequeue();
            }
            else
            {
                obj = Object.Instantiate(_prefab, _container);
            }

            obj.gameObject.SetActive(true);
            obj.Init(); // вызвать пользовательскую инициализацию

            return obj;
        }
        
        // Вернуть объект обратно в пул.
        public void ReturnToPool(T obj)
        {
            obj.DeInit();
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public interface IPoolObject
    {
        public void Init();
        public void DeInit();
    }
}