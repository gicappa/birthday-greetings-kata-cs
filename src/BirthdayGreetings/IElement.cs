namespace BirthdayGreetings;

interface IElement<out T>
{
    void Accept(IVisitor<T> visitor);
}