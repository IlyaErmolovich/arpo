using NUnit.Framework;
using Platformer.Mechanics;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTests
{
    private const string MainScenePath = "Assets/Scenes/SampleScene.unity";
    private Scene scene;
    private bool openedScene;

    [Test]
    public void EditMode_PlayerTag_IsCorrect()
    {
        // Проверяем настоящую сцену, сохраняя уже открытые сцены редактора.
        scene = SceneManager.GetSceneByPath(MainScenePath);
        if (!scene.IsValid() || !scene.isLoaded)
        {
            scene = EditorSceneManager.OpenScene(MainScenePath, OpenSceneMode.Additive);
            openedScene = true;
        }

        Assert.IsTrue(scene.IsValid(), "Не удалось открыть сцену SampleScene.");

        GameObject player = null;
        foreach (var root in scene.GetRootGameObjects())
        {
            foreach (var transform in root.GetComponentsInChildren<Transform>(true))
            {
                if (transform.name == "Player")
                    player = transform.gameObject;
            }
        }

        Assert.IsNotNull(player, "На сцене не найден объект Player.");
        Assert.AreEqual("Player", player.tag, "У игрока сброшен тег Player.");
        Assert.IsNotNull(player.GetComponent<PlayerController>(),
            "У игрока отсутствует PlayerController.");
        Assert.IsNotNull(player.GetComponent<Health>(),
            "У игрока отсутствует Health.");
    }

    [TearDown]
    public void TearDown()
    {
        if (openedScene && scene.IsValid() && scene.isLoaded)
            EditorSceneManager.CloseScene(scene, true);
        openedScene = false;
    }
}
