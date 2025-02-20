using UnityEngine;

public abstract class SoldierBase
{
    public string Name { get; private set; }
    public int Health { get; private set; }
    public int Attack { get; private set; }
    public int Bonus { get; private set; }

    protected SoldierBase(string name, int health, int attack, int bonus)
    {
        Name = name;
        Health = health;
        Attack = attack;
        Bonus = bonus;
    }

    public virtual void DisplayInfo()
    {
        Debug.Log($"Name: {Name}, Health: {Health}, Attack: {Attack}, Bonus: {Bonus}");
    }
}

public class KapikuluPiyadesi : SoldierBase
{
    public KapikuluPiyadesi() : base("Kap�kulu Piyadesi", 100, 20, 10) { }
}

public class GulamMizraklisi : SoldierBase
{
    public GulamMizraklisi() : base("Gul�m M�zrakl�s�", 100, 20, 10) { }
}

public class SipahiSuvarisi : SoldierBase
{
    public SipahiSuvarisi() : base("Sipahi S�varisi", 100, 20, 10) { }
}
