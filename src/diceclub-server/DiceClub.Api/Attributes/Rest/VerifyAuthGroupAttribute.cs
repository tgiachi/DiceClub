namespace DiceClub.Api.Attributes.Rest;


[AttributeUsage(AttributeTargets.Class)]
public class VerifyAuthGroupAttribute : Attribute
{
    public string GroupNames { get; set; }

    public VerifyAuthGroupAttribute(string groupNames)
    {
        GroupNames = groupNames;
    }
    
}