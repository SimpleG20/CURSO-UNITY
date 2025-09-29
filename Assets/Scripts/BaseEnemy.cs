using UnityEngine;

public abstract class BaseEnemy : BaseCharacter
{
    private void Update()
    {
        UpdateLogic();
    }
    protected abstract void UpdateLogic();
}
