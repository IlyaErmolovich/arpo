using System.Collections;
using NUnit.Framework;
using Platformer.Core;
using Platformer.Mechanics;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerHealthTests
{
    private GameObject playerObj;

    [UnityTest]
    public IEnumerator PlayMode_PlayerTakesDamage_HealthDecreases()
    {
        Simulation.Clear();
        playerObj = new GameObject("TestPlayer");
        playerObj.SetActive(false);
        Health health = playerObj.AddComponent<Health>();
        health.maxHP = 3;
        playerObj.SetActive(true);

        Assert.IsTrue(health.IsAlive, "Персонаж должен быть жив на старте.");

        health.Decrement();
        Assert.IsTrue(health.IsAlive, "Персонаж не должен погибнуть после одного урона.");

        health.Decrement();
        health.Decrement();
        Assert.IsFalse(health.IsAlive, "После трёх ударов здоровье должно закончиться.");

        // Событие смерти не должно попасть в следующий тест.
        Simulation.Clear();
        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Simulation.Clear();
        if (playerObj != null)
            Object.Destroy(playerObj);
        yield return null;
    }
}
