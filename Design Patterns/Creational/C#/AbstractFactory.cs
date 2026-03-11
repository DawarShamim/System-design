using System.Collections.Generic;

interface ISedan { void Build(); }
interface ISUV { void Build(); }
interface IHatchBack { void Build(); }

abstract class Company
{
    public abstract void Company_Name();
}
class Toyota() : Company
{
    public override Company_Name()
    {
        throw new System.NotImplementedException();
    }
}


class AbstractFactory
{
    private static readonly Dictionary<string, Func<Logger>> Company =
            new Dictionary<string, Func<Logger>>();


    private static readonly Dictionary<string, Func<Logger>> Category =
            new Dictionary<string, Func<Logger>>();


}