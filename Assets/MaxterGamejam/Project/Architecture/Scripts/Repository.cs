namespace com.LOK1game.recode.Architecture
{
	public abstract class Repository
	{
        public abstract void Initialize();
        public abstract void Save();

        public virtual void OnCreate() { }
        public virtual void OnStart() { }
	}
}