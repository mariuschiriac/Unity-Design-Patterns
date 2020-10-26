//-------------------------------------------------------------------------------------
//	TypeObjectPatternExample.cs

// Type object mode: support the flexible creation of new types by creating a class, each instance of which represents a different object type
// Thoughts: 
// 1. Use aggregation instead of inheritance. has a instead of is a. Can be understood as the composite reuse principle among the six principles of design patterns (Composite Reuse Principle)
// 2, and you can flexibly choose whether to inherit from the parent class through configuration
//-------------------------------------------------------------------------------------




using UnityEngine;
using System.Collections;

public class TypeObjectPatternExample : MonoBehaviour
{
    void Start()
    {
        //Create a category, fill with a life value of 0 to indicate inheritance from the parent category.
        Breed troll = new Breed(null, 25, "The troll hits you!");

        Breed trollArcher = new Breed(troll, 0, "The troll archer fires an arrow!");

        Breed trollWizard = new Breed(troll, 0, "The troll wizard casts a spell on you!");

        //Create monster objects by category
        Monster trollMonster = troll.NewMonster();
        trollMonster.ShowAttack();

        Monster trollArcherMonster = trollArcher.NewMonster();
        trollArcherMonster.ShowAttack();

        Monster trollWizardMonster = trollWizard.NewMonster();
        trollWizardMonster.ShowAttack();

    }

}


/// <summary>
/// Breed
/// </summary>
public class Breed
{
    private int health_;
    private string attack_;
    Breed parent_;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="parent">father</param>
    /// <param name="health">Life value, fill with 0 means inherit from the parent class</param>
    /// <param name="attack">Attack performance</param>
    public Breed(Breed parent, int health, string attack)
    {
        health_ = health;
        attack_ = attack;
        parent_ = null;

        //Copy the "agent", copy the inherited characteristics to the inside of the class when creating a type
        //Note that we no longer need the attributes in the parent class, once the construction is over, you can forget the base class
        if (parent != null)
        {
            parent_ = parent;

            //It is 0, taken from the parent layer
            if (health == 0)
            {
                health_ = parent.GetHealth();
            }
            //Is null, taken from the parent layer
            if (attack == null)
            {
                attack_ = parent.GetAttack();
            }
        }
    }

    public Monster NewMonster()
    {
        return new Monster(this);
    }

    public int GetHealth()
    {
        return health_;
    }

    public string GetAttack()
    {
        return attack_;
    }
}



/// <summary>
/// Monster “has a”Breed
/// </summary>
public class Monster
{
    private int health_;
    private Breed breed_;
    private string attack_;

    public Monster(Breed breed)
    {
        health_ = breed.GetHealth();
        breed_ = breed;
        attack_ = breed.GetAttack();
    }

    public string GetAttack()
    {
        return attack_;
    }

    public void ShowAttack()
    {
        Debug.Log(attack_);
    }
}





