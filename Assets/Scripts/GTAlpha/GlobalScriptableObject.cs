using UnityEngine;

namespace GTAlpha
{
    /// <summary>
    /// GameStarter 객체에 등록되어야 하는 모든 객체가 상속하는 클래스로 게임 시작시에 GameStarter에 등록된 모든 GlobalScriptableObject가 Load 함수를 호출한다. 
    /// </summary>
    public abstract class GlobalScriptableObject : ScriptableObject
    {
        public abstract void Load();
    }
}