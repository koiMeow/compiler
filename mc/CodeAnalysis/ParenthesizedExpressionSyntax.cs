using System.Collections.Generic;

namespace Compiler.CodeAnalysis
{
    sealed class ParenthesizedExpressionSyntax : ExpressionSyntax
    {
        public ParenthesizedExpressionSyntax(SyntaxToken openParenthesizedToken, ExpressionSyntax expression, SyntaxToken closeParenthesizedToken)
        {
            OpenParenthesizedToken = openParenthesizedToken;
            Expression = expression;
            CloseParenthesizedToken = closeParenthesizedToken;
        }

        public SyntaxToken OpenParenthesizedToken { get; }
        public ExpressionSyntax Expression { get; }
        public SyntaxToken CloseParenthesizedToken { get; }

        public override SyntaxKind Kind => SyntaxKind.ParenthesizedExpression;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return OpenParenthesizedToken;
            yield return Expression;
            yield return CloseParenthesizedToken;
        }
    }
}