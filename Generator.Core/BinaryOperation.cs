using System;

namespace Generator.Core
{
	public abstract class BinaryOperation<T> : Operation<T>
	{
		private readonly Operation<T> _operand1;
		private readonly Operation<T> _operand2;

		protected BinaryOperation( Operation<T> operand1, Operation<T> operand2 )
		{
			_operand1 = operand1;
			_operand2 = operand2;
		}

		protected abstract T EvaluateCore( T value1, T value2 );

		public sealed override T Evaluate()
		{
			T value1 = _operand1.Evaluate();
			T value2 = _operand2.Evaluate();
			T value = EvaluateCore( value1, value2 );
			return value;
		}

		public abstract string OperationText { get; }

		public sealed override string Text
		{
			get
			{
				string operation = OperationText;
				string text1 = _operand1.Text;
				string text2 = _operand2.Text;

				return String.Format( "{0} {1} {2}", text1, operation, text2 );
			}
		}

		public sealed override double Complexity
		{
			get
			{
				double complexity = _operand1.Complexity + _operand2.Complexity + OperationComplexity;
				return complexity;
			}
		}

		protected virtual double OperationComplexity
		{
			get { return 1; }
		}
	}
}