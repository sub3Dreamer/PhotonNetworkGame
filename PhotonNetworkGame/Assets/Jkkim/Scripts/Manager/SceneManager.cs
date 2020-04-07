using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScene = UnityEngine.SceneManagement;

namespace PUNGame
{
    public class SceneManager : MonoBehaviour
    {
        public enum Scene
        {
            UnDefined = -1,
            Login = 0,
            Stage = 1,
        }

        public static Scene GetActiveScene()
        {
            return (Scene)Enum.Parse(typeof(Scene), UnityScene.SceneManager.GetActiveScene().name);
        }

        public static void LoadScene(Scene sceneName)
        {
            UnityScene.SceneManager.LoadScene(sceneName.ToString());
        }
    }
}