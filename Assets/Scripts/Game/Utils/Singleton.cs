﻿namespace Game.Utils
{
    public abstract class Singleton<T> where T : new()
    {
        private static T _instance;

        public static T Instance => _instance ??= new T();
    }
}