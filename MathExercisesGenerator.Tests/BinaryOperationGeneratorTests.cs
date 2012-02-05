using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator.Core;
using Moq;
using NUnit.Framework;

namespace MathExercisesGenerator.Tests
{
	[TestFixture]
	public class BinaryOperationGeneratorTests
	{
		[Test]
		public void ShouldGenerateAddition()
		{
			CreateAndAssertBinaryOp<AddOperation>( 1 );
		}

		[Test]
		public void ShouldGenerateSubtraction()
		{
			CreateAndAssertBinaryOp<SubtractOperation>( 0 );
		}

		private static void CreateAndAssertBinaryOp<T>( double rndMockValue )
		{
			Mock<IOperationGenerator<int>> parentMock = new Mock<IOperationGenerator<int>>();
			parentMock.Setup( g => g.Generate( It.IsAny<GenerationContext<int>>() ) ).Returns( new Number( 1 ) );

			Mock<IRandomNumberGenerator<double>> rndMock = new Mock<IRandomNumberGenerator<double>>();
			rndMock.Setup( r => r.Generate( It.IsAny<Range<double>>() ) ).Returns( rndMockValue );

			IOperationGenerator<int> generator =
				(typeof( T ) == typeof( AddOperation ))
					? (IOperationGenerator<int>)new AdditionGenerator()
					: new SubtractionGenerator();

			var context = new GenerationContext<int>( rndMock.Object, new Mock<IRandomNumberGenerator<int>>().Object,
													 parentMock.Object, 1, new Range<int>( 0, 2 ) );
			var op = generator.Generate( context );

			Assert.That( op, Is.InstanceOf<T>() );
		}
	}
}
