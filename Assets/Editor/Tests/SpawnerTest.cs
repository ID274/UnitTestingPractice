using NUnit.Framework;
using UnityEngine;

public class SpawnerTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void CheckIfHeightNotAboveMax()
    {
        //Assemble
        GameObject spawnerObject = new GameObject();
        spawnerObject.AddComponent<Spawner>();
        Spawner spawnerScript = spawnerObject.GetComponent<Spawner>();
        //Act
        spawnerScript.height = 100;
        spawnerScript.GetGridSize();
        //Assert
        Assert.LessOrEqual(spawnerScript.height, 10);
    }

    [Test]
    public void CheckIfHeightNotBelowMin()
    {
        //Assemble
        GameObject spawnerObject = new GameObject();
        spawnerObject.AddComponent<Spawner>();
        Spawner spawnerScript = spawnerObject.GetComponent<Spawner>();
        //Act
        spawnerScript.height = -10;
        spawnerScript.GetGridSize();
        //Assert
        Assert.GreaterOrEqual(spawnerScript.height, 1);
    }

    [Test]
    public void CheckIfLengthNotAboveMax()
    {
        //Assemble
        GameObject spawnerObject = new GameObject();
        spawnerObject.AddComponent<Spawner>();
        Spawner spawnerScript = spawnerObject.GetComponent<Spawner>();
        //Act
        spawnerScript.length = 100;
        spawnerScript.GetGridSize();
        //Assert
        Assert.LessOrEqual(spawnerScript.length, 10);
    }

    [Test]
    public void CheckIfLengthNotBelowMin()
    {
        //Assemble
        GameObject spawnerObject = new GameObject();
        spawnerObject.AddComponent<Spawner>();
        Spawner spawnerScript = spawnerObject.GetComponent<Spawner>();
        //Act
        spawnerScript.length = -10;
        spawnerScript.GetGridSize();
        //Assert
        Assert.GreaterOrEqual(spawnerScript.length, 1);
    }

    [Test]
    public void CheckIfMinesLowerThanArea()
    {
        //Assemble
        GameObject spawnerObject = new GameObject();
        spawnerObject.AddComponent<Spawner>();
        Spawner spawnerScript = spawnerObject.GetComponent<Spawner>();
        //Act
        spawnerScript.length = 10;
        spawnerScript.height = 10;
        spawnerScript.mines = 100;
        spawnerScript.GenerateMines();
        //Assert
        Assert.LessOrEqual(spawnerScript.mines, spawnerScript.length * spawnerScript.height / 2);
    }

}
