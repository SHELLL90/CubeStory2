interface IHealth
{ 
    public float MaxHealth { get; }
    public float CurrentHealth { get; }
    public void Damage(float value);
}
