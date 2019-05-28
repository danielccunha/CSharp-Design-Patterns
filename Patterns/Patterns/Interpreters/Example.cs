using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patterns.Interpreters
{
    public interface IElement
    {
        int Value { get; }
    }

    public class Integer : IElement
    {
        public Integer(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }

    public class BinaryOperation : IElement
    {
        public enum Type
        {
            Addition, Substraction
        }

        public Type MyType { get; set; }
        public IElement Left { get; set; }
        public IElement Right { get; set; }
        public int Value
        {
            get
            {
                switch (MyType)
                {
                    case Type.Addition:
                        return Left.Value + Right.Value;
                    case Type.Substraction:
                        return Left.Value - Right.Value;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

    public class Token
    {
        public enum Type
        {
            Integer, Plus, Minus, Lparen, Rparen
        }

        public Type MyType { get; }
        public string Text { get; }

        public Token(Type myType, string text)
        {
            MyType = myType;
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public override string ToString() => $"`{Text}`";
    }

    public static class Example
    {
        public static void Start()
        {
            var input = "(13+4)-(12+1)";
            var tokens = Lex(input);
            var parsed = Parse(tokens);

            Console.WriteLine($"{input} = {parsed.Value}");
        }

        public static List<Token> Lex(string input)
        {
            var result = new List<Token>();

            for (int index = 0; index < input.Length; ++index)
            {
                switch (input[index])
                {
                    case '+':
                        result.Add(new Token(Token.Type.Plus, "+"));
                        break;
                    case '-':
                        result.Add(new Token(Token.Type.Minus, "-"));
                        break;
                    case '(':
                        result.Add(new Token(Token.Type.Lparen, "("));
                        break;
                    case ')':
                        result.Add(new Token(Token.Type.Rparen, ")"));
                        break;
                    default:
                        var sb = new StringBuilder(input[index].ToString());

                        for (int jIndex = index + 1; jIndex < input.Length; ++jIndex)
                        {
                            if (char.IsDigit(input[jIndex]))
                            {
                                sb.Append(input[jIndex]);
                                ++index;
                            }
                            else
                            {
                                result.Add(new Token(Token.Type.Integer, sb.ToString()));
                                break;
                            }
                        }
                        break;
                }
            }

            return result;
        }

        public static IElement Parse(IReadOnlyList<Token> tokens)
        {
            var result = new BinaryOperation();
            var haveLHS = false;

            for (int index = 0; index < tokens.Count; index++)
            {
                var token = tokens[index];

                switch (token.MyType)
                {
                    case Token.Type.Integer:
                        var integer = new Integer(int.Parse(token.Text));
                        if (!haveLHS)
                        {
                            result.Left = integer;
                            haveLHS = true;
                        }
                        else
                            result.Right = integer;
                        break;
                    case Token.Type.Plus:
                        result.MyType = BinaryOperation.Type.Addition;
                        break;
                    case Token.Type.Minus:
                        result.MyType = BinaryOperation.Type.Substraction;
                        break;
                    case Token.Type.Lparen:
                        int jIndex = index;
                        for (; jIndex < tokens.Count; ++jIndex)
                            if (tokens[jIndex].MyType == Token.Type.Rparen)
                                break;

                        var subExpression = tokens.Skip(index + 1).Take(jIndex - index - 1).ToList();
                        var element = Parse(subExpression);

                        if (!haveLHS)
                        {
                            result.Left = element;
                            haveLHS = true;
                        }
                        else
                            result.Right = element;
                        index = jIndex;
                        break;
                }
            }

            return result;
        }
    }
}
