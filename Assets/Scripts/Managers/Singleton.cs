﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeStudio.Core
{
    public class Singleton <T>: MonoBehaviour where T: MonoBehaviour
    {
        private static T m_instance;

        public static T Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = GameObject.FindObjectOfType<T>();
                    if (m_instance == null)
                    {
                        GameObject singleton = new GameObject(typeof(T).Name);
                        m_instance = singleton.AddComponent<T>();
                    }
                }
                return m_instance;
            }
        }

        private void Awake()
        {
            if (m_instance == null)
            {
                m_instance = this as T;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}


