namespace Generator.Core
{
	public abstract class Operation<T>
	{
		public abstract T Evaluate();

		public abstract string Text { get; }

		public abstract double Complexity { get; }

		public override string ToString()
		{
			return Text;
		}
	}
}