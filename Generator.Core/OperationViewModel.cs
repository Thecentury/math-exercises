using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generator.Core
{
	public abstract class OperationViewModel : BindingObject
	{
		public abstract int Value { get; }
	}

	public sealed class BracketOperationViewModel : OperationViewModel
	{
		private readonly OperationViewModel _inner;

		public BracketOperationViewModel( OperationViewModel inner )
		{
			_inner = inner;
		}

		public OperationViewModel Inner
		{
			get { return _inner; }
		}

		public override int Value
		{
			get { return _inner.Value; }
		}
	}

	public sealed class NumberOperationViewModel : OperationViewModel
	{
		private readonly Number _number;

		public NumberOperationViewModel( Number number )
		{
			_number = number;
		}

		public Number Number
		{
			get { return _number; }
		}

		public override int Value
		{
			get { return _number.Evaluate(); }
		}
	}

	public sealed class BinaryOperationViewModel : OperationViewModel
	{
		private readonly string _operationString;
		private readonly OperationViewModel _left;
		private readonly OperationViewModel _right;
		private readonly BinaryOperation<int> _operation;

		public BinaryOperationViewModel( string operationString, OperationViewModel left, OperationViewModel right, BinaryOperation<int> operation )
		{
			_operationString = operationString;
			_left = left;
			_right = right;
			_operation = operation;
		}

		public OperationViewModel Right
		{
			get { return _right; }
		}

		public OperationViewModel Left
		{
			get { return _left; }
		}

		public string Operation
		{
			get { return _operationString; }
		}

		public override int Value
		{
			get { return _operation.Evaluate(); }
		}
	}
}
