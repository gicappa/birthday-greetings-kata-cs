namespace BirthdayGreetings;

interface IVisitor<in T>
{
    void Visit(T element);
}