using System;
using JetBrains.Annotations;

namespace Generator.Core
{
	public abstract class BinaryOperation<T> : Operation<T>
	{
		public Operation<T> Left { get; set; }
		public Operation<T> Right { get; set; }

		protected BinaryOperation() { }

		protected BinaryOperation( [NotNull] Operation<T> left, [NotNull] Operation<T> right )
		{
			if ( left == null )
			{
				throw new ArgumentNullException( "left" );
			}
			if ( right == null )
			{
				throw new ArgumentNullException( "right" );
			}

			Left = left;
			Right = right;
		}

		protected abstract T EvaluateCore( T value1, T value2 );

		public sealed override T Evaluate()
		{
			T value1 = Left.Evaluate();
			T value2 = Right.Evaluate();
			T value = EvaluateCore( value1, value2 );
			return value;
		}

		public abstract string OperationText { get; }

		public sealed override string Text
		{
			get
			{
				string operation = OperationText;
				string text1 = Left.Text;
				string text2 = Right.Text;

				return String.Format( "{0} {1} {2}", text1, operation, text2 );
			}
		}

		public abstract BinaryOperation<T> CloneCore();
	}
}