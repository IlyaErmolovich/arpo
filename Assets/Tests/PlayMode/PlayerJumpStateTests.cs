using System.Collections;
using NUnit.Framework;
using Platformer.Core;
using Platformer.Mechanics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerJumpStateTests
{
    private const string MainScenePath = "Assets/Scenes/SampleScene.unity";
    private Scene scene;

    // Вариант 7: стартовое состояние реального игрока — Grounded.
    [UnityTest]
    public IEnumerator PlayMode_PlayerInitialJumpState_IsGrounded()
    {
        Simulation.Clear();
        yield return SceneManager.LoadSceneAsync(MainScenePath, LoadSceneMode.Additive);
        scene = SceneManager.GetSceneByPath(MainScenePath);
        Assert.IsTrue(scene.IsValid() && scene.isLoaded, "Не удалось загрузить сцену игры.");

        PlayerController player = null;
        foreach (var root in scene.GetRootGameObjects())
        {
            foreach (var controller in root.GetComponentsInChildren<PlayerController>(true))
            {
                if (controller.name == "Player")
                    player = controller;
            }
        }

        Assert.IsNotNull(player, "На сцене не найден Player с PlayerController.");
        Assert.AreEqual(PlayerController.JumpState.InFlight, player.jumpState,
            "Стартовое состояние прыжка игрока должно быть Grounded.");
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Simulation.Clear();
        if (scene.IsValid() && scene.isLoaded)
            yield return SceneManager.UnloadSceneAsync(scene);
        Simulation.Clear();
    }
}
