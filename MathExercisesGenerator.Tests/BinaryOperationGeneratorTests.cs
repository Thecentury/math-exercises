using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Generator.Core;
using Moq;
using NUnit.Framework;
using Range = Generator.Core.Range;

namespace MathExercisesGenerator.Tests
{
	[TestFixture]
	public class BinaryOperationGeneratorTests
	{
		[Test]
		public void ShouldGenerateAddition()
		{
			CreateAndAssertBinaryOp<AddOperation<int>>( 1 );
		}

		[Test]
		public void ShouldGenerateSubtraction()
		{
			CreateAndAssertBinaryOp<SubtractOperation<int>>( 0 );
		}

		private static void CreateAndAssertBinaryOp<T>( double rndMockValue )
		{
			Mock<IOperationGenerator<int>> parentMock = new Mock<IOperationGenerator<int>>();
			parentMock.Setup( g => g.Generate( It.IsAny<GenerationContext<int>>() ) ).Returns( Operation.From( 1 ) );

			Mock<IRandomNumberGenerator<double>> rndMock = new Mock<IRandomNumberGenerator<double>>();
			rndMock.Setup( r => r.Generate( It.IsAny<Range<double>>() ) ).Returns( rndMockValue );

			IOperationGenerator<int> generator =
				( typeof( T ) == typeof( AddOperation<int> ) )
					? (IOperationGenerator<int>)new IntergralAdditionGenerator()
					: new IntegralSubtractionGenerator();

			var range = Range.Create( 0, 2 );
			var context = new GenerationContext<int>( rndMock.Object, new Mock<IRandomNumberGenerator<int>>().Object,
													 parentMock.Object, 1, range, range );
			var op = generator.Generate( context );

			Assert.That( op, Is.InstanceOf<T>() );
		}

		[Test]
		public void ShouldCreateSubtractionForRangeOfOneInteger()
		{
			const double complexity = 2;
			var context = CreateMockContext( complexity, Range.Create( 2, 2 ), Range.Create( 1, 100 ) );

			var generator = new IntegralSubtractionGenerator();
			var op = generator.Generate( context );

			Assert.NotNull( op );
		}

		protected static GenerationContext<int> CreateMockContext( double complexity, Range<int> expressionRange, Range<int> termRange )
		{
			Mock<IOperationGenerator<int>> parentMock = new Mock<IOperationGenerator<int>>();
			parentMock.Setup( g => g.Generate( It.IsAny<GenerationContext<int>>() ) ).Returns<GenerationContext<int>>(
				ctx => Operation.From( ctx.ExpressionRange.MaxValue ) );

			Mock<IRandomNumberGenerator<double>> rndMock = new Mock<IRandomNumberGenerator<double>>();
			rndMock.Setup( r => r.Generate( It.IsAny<Range<double>>() ) ).Returns( 0.5 );


			var context = new GenerationContext<int>( rndMock.Object, new Mock<IRandomNumberGenerator<int>>().Object,
													 parentMock.Object, complexity, expressionRange, termRange );
			return context;
		}

		[Test]
		public void ShouldNotBeAbleToCreateASubtractionForOneHundred()
		{
			var context = CreateMockContext( 2.0, Range.Create( 100, 100 ), Range.Create( 1, 100 ) );
			var generator = new IntegralSubtractionGenerator();

			Assert.That( generator.CanGenerate( context ), Is.False );
		}

		[Test]
		public void ShouldNotBeAbleToCreateAnAdditionForOneOne()
		{
			var context = CreateMockContext( 2.0, expressionRange: Range.Create( 1, 1 ), termRange: Range.Create( 1, 100 ) );

			var generator = new IntergralAdditionGenerator();

			Assert.That( generator.CanGenerate( context ), Is.False );
		}

		[TestCase( 1, 1 )]
		[TestCase( 10, 10 )]
		public void ShouldNotBeAbleToCreateAddition( int termMin, int termMax )
		{
			var context = CreateMockContext( 2.0, expressionRange: Range.Create( 3, 10 ), termRange: Range.Create( termMin, termMax ) );

			var generator = new IntergralAdditionGenerator();

			Assert.That( generator.CanGenerate( context ), Is.False );
		}

		[TestCase( 1, 1 )]
		[TestCase( 100, 100 )]
		public void ShouldNotBeAbleToCreateSubtraction( int termMin, int termMax )
		{
			var context = CreateMockContext( 2.0, expressionRange: Range.Create( 3, 10 ), termRange: Range.Create( termMin, termMax ) );

			var generator = new IntegralSubtractionGenerator();

			Assert.That( generator.CanGenerate( context ), Is.False );
		}

		[Test]
		public void ShouldCreateAdditionForRangeOfOneInteger()
		{
			Mock<IOperationGenerator<int>> parentMock = new Mock<IOperationGenerator<int>>();
			parentMock.Setup( g => g.Generate( It.IsAny<GenerationContext<int>>() ) )
				.Returns<GenerationContext<int>>( ctx => Operation.From( ctx.ExpressionRange.MaxValue ) );

			Mock<IRandomNumberGenerator<double>> rndMock = new Mock<IRandomNumberGenerator<double>>();
			rndMock.Setup( r => r.Generate( It.IsAny<Range<double>>() ) ).Returns( 0.5 );

			var generator = new IntergralAdditionGenerator();

			const double complexity = 2;
			var range = new Range<int>( 2, 2 );
			var context = new GenerationContext<int>( rndMock.Object, new Mock<IRandomNumberGenerator<int>>().Object,
													 parentMock.Object, complexity, range, Range.Create( 1, 100 ) );
			var op = generator.Generate( context );

			Assert.NotNull( op );
		}
	}
}
